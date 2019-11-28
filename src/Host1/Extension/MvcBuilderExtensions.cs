//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Runtime.Loader;
//using Microsoft.Extensions.DependencyInjection;
//using PluginBase;

//namespace Website1.Extensions
//{
//    public static class MvcBuilderExtensions
//    {
//        public static IMvcBuilder AddPlugins(this IMvcBuilder source, string path)
//        {
//            var additionalAssemblies = new List<Assembly>();

//            foreach (var dir in Directory.GetDirectories(Path.Combine(AppContext.BaseDirectory, "Plugins")))
//            {
//                var pluginFile = Path.Combine(dir, Path.GetFileName(dir) + ".dll");
//                Console.WriteLine($"Loading plugin {pluginFile}");

//                source.AddPluginFromAssemblyFile(pluginFile);
//            }

//            var assemblies = AssemblyLoadContext.All
//                .SelectMany(x => x.Assemblies)
//                .Where(x => x.ExportedTypes.Any(d => !d.IsInterface && typeof(IFeature).IsAssignableFrom(d)))
//                .ToList();

//            var featureTypes = assemblies.SelectMany(x => x.ExportedTypes).Where(x => !x.IsInterface && typeof(IFeature).IsAssignableFrom(x));
//            foreach (var featureType in featureTypes)
//            {
//                var feature = Activator.CreateInstance(featureType) as IFeature;
//                if (feature == null)
//                    throw new ArgumentNullException(nameof(feature), $"Failed to instantiate {nameof(IFeature)} from type {featureType.FullName}.");

//                services.AddSingleton(feature);
//                feature.Register(services);

//                if (!additionalAssemblies.Contains(featureType.Assembly))
//                    additionalAssemblies.Add(featureType.Assembly);
//            }

//            additionalAssemblies.ForEach(x => Console.WriteLine($"AdditionalAssemblies: {x.FullName}"));
//            services.AddSingleton(typeof(IAdditionalAssembliesProvider), new AdditionalAssembliesProvider(additionalAssemblies));

//            return source;
//        }
//    }
//}