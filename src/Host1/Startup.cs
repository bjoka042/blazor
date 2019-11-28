using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using McMaster.NETCore.Plugins;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Linq;
using PluginBase;
using Host1.Utility;

namespace Host1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddRazorPages();
            services.AddServerSideBlazor();

            var additionalAssemblies = new List<Assembly>();

            foreach (var dir in Directory.GetDirectories(Path.Combine(AppContext.BaseDirectory, "Plugins")))
            {
                var pluginFile = Path.Combine(dir, Path.GetFileName(dir) + ".dll");
                Console.WriteLine($"Loading plugin {pluginFile}");

                mvcBuilder.AddPluginFromAssemblyFile(pluginFile);
            }

            var assemblies = AssemblyLoadContext.All
                .SelectMany(x => x.Assemblies)
                .Where(x => x.ExportedTypes.Any(d => !d.IsInterface && typeof(IFeature).IsAssignableFrom(d)))
                .ToList();

            var featureTypes = assemblies.SelectMany(x => x.ExportedTypes).Where(x => !x.IsInterface && typeof(IFeature).IsAssignableFrom(x));
            foreach (var featureType in featureTypes)
            {
                var feature = Activator.CreateInstance(featureType) as IFeature;
                if (feature == null)
                    throw new ArgumentNullException(nameof(feature), $"Failed to instantiate {nameof(IFeature)} from type {featureType.FullName}.");

                services.AddSingleton(feature);
                feature.Register(services);

                if (!additionalAssemblies.Contains(featureType.Assembly))
                    additionalAssemblies.Add(featureType.Assembly);
            }

            additionalAssemblies.ForEach(x => Console.WriteLine($"AdditionalAssemblies: {x.FullName}"));
            services.AddSingleton(typeof(IAdditionalAssembliesProvider), new AdditionalAssembliesProvider(additionalAssemblies));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute(); // Behöv om man ska använda Controllers/Routes
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
