using Plugin1.Providers;
using Microsoft.Extensions.DependencyInjection;
using PluginBase;
using Microsoft.Extensions.Logging;

namespace Plugin1
{
    public class Plugin1Feature : IFeature
    {
        public string Name => "Plugin1";

        public void Register(IServiceCollection services, ILogger logger)
        {
            logger.LogInformation($"Register {nameof(ITextProvider)}");
            services.AddTransient<ITextProvider, TextProvider>();
        }
    }
}
