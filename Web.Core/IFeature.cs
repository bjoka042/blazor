using Microsoft.Extensions.DependencyInjection;
using System;

namespace Web.Core
{
    public interface IFeature
    {
        string Name { get; }

        void Register(IServiceCollection serviceProvider);
    }
}
