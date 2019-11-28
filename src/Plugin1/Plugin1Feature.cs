using Plugin1.Providers;
using Microsoft.Extensions.DependencyInjection;
using PluginBase;

namespace Plugin1
{
    public class Plugin1Feature : IFeature
    {
        public string Name => "Plugin1";

        public void Register(IServiceCollection services)
        {
            services.AddTransient<ITextProvider, TextProvider>();
        }
    }
}
