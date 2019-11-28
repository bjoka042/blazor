using System.Collections.Generic;
using System.Reflection;

namespace HostBase.Providers
{
    public interface IAdditionalAssembliesProvider
    {
        IEnumerable<Assembly> GetAdditionalAssemblies();
    }
}
