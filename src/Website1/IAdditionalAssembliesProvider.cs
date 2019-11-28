using System.Collections.Generic;
using System.Reflection;

namespace Website1
{
    public interface IAdditionalAssembliesProvider
    {
        IEnumerable<Assembly> Get();
    }
}
