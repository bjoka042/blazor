using System.Collections.Generic;
using System.Reflection;

namespace Host1.Utility
{
    public interface IAdditionalAssembliesProvider
    {
        IEnumerable<Assembly> Get();
    }
}
