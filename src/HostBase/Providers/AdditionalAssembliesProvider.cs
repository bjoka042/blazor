using System.Collections.Generic;
using System.Reflection;

namespace HostBase.Providers
{
    public class AdditionalAssembliesProvider : IAdditionalAssembliesProvider
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public AdditionalAssembliesProvider(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<Assembly> GetAdditionalAssemblies()
        {
            return _assemblies;
        }
    }
}
