using System.Collections.Generic;
using System.Reflection;

namespace Website1
{
    public class AdditionalAssembliesProvider : IAdditionalAssembliesProvider
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public AdditionalAssembliesProvider(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }
        public IEnumerable<Assembly> Get()
        {
            return _assemblies;
        }
    }
}
