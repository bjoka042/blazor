using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using HostBase.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PluginBase;

namespace HostBase.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddPlugins(this IMvcBuilder source, string pluginPath, ILogger logger)
        {
            try
            {
                logger?.LogInformation($"Loading plugins from path: {pluginPath}");

                var directories = Directory.GetDirectories(pluginPath);

                if (directories.Any() == false)
                {
                    logger?.LogWarning($"No plugins found in directory: {pluginPath}");
                }

                foreach (var dir in directories)
                {
                    var pluginName = Path.GetFileName(dir);
                    var pluginFile = Path.Combine(dir, pluginName + ".dll");

                    logger?.LogInformation($"Found plugin: {pluginName}, trying to load: {pluginFile}");

                    try
                    {
                        source.AddPluginFromAssemblyFile(pluginFile);
                    }
                    catch (Exception ex)
                    {
                        logger?.LogError(ex, $"Failed to add plugin: {pluginFile}");
                    }
                }
                var assemblies = AssemblyLoadContext.All
                    .SelectMany(x => x.Assemblies)
                    .Where(x => x.ExportedTypes.Any(d => !d.IsInterface && typeof(IFeature).IsAssignableFrom(d)))
                    .ToList();

                if (logger?.IsEnabled(LogLevel.Information) ?? false)
                {
                    logger?.LogInformation($"Looking for implementations of {nameof(IFeature)} in loaded assemblies");
                    assemblies.ForEach(x =>
                    {
                        logger?.LogInformation($"Found {x.GetName()} in assembly: {x.FullName}");
                    });

                    if (assemblies.Any() == false)
                    {
                        logger?.LogWarning($"No implementation of {nameof(IFeature)} was found");
                    }
                }

                var featureTypes = assemblies.SelectMany(x => x.ExportedTypes).Where(x => !x.IsInterface && typeof(IFeature).IsAssignableFrom(x));

                foreach (var featureType in featureTypes)
                {
                    logger?.LogInformation($"Creating instance of {featureType.Name}");

                    try
                    {
                        var feature = Activator.CreateInstance(featureType) as IFeature;
                        if (feature == null)
                            throw new ArgumentNullException(nameof(feature), $"Failed to instantiate type: {featureType.FullName}.");

                        source.Services.AddSingleton(feature);

                        logger?.LogInformation($"Register services from feature {feature.Name}");

                        feature.Register(source.Services, logger);
                    }
                    catch (Exception ex)
                    {
                        logger?.LogError(ex, $"Failed register feature {featureType.FullName}");
                    }
                }

                if (assemblies.Any())
                {
                    assemblies.ForEach(x => logger?.LogInformation($"Adding assebly: {x.FullName} to {nameof(AdditionalAssembliesProvider)}"));
                }
                else
                {
                    logger?.LogInformation($"There were no asseblies to add to {nameof(AdditionalAssembliesProvider)}");
                }

                source.Services.AddSingleton(typeof(IAdditionalAssembliesProvider), new AdditionalAssembliesProvider(assemblies));
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, $"Failed to add plugins");
            }

            return source;
        }
    }
}