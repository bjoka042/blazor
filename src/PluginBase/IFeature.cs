using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace PluginBase
{
    public interface IFeature
    {
        string Name { get; }

        void Register(IServiceCollection serviceProvider, ILogger logger);
    }
}
