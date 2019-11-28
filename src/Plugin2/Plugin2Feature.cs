using Microsoft.Extensions.DependencyInjection;
using PluginBase;

namespace Plugin2
{
    public class Plugin1Feature : IFeature
    {
        public string Name => "Plugin2";

        public void Register(IServiceCollection services)
        {
        }
    }
}
