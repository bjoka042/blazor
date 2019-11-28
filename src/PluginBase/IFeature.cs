using Microsoft.Extensions.DependencyInjection;
using System;

namespace PluginBase
{
    public interface IFeature
    {
        string Name { get; }

        void Register(IServiceCollection serviceProvider);
    }
}
