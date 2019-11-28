using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Host1.Utility
{
    public static class PluginDirectory
    {
        public static IEnumerable<string> GetAssemblyPaths(string path)
        {
            return Directory
                .EnumerateFiles(path, "*.dll", SearchOption.AllDirectories)
                .Where(d => !d.Contains($"{Path.DirectorySeparatorChar}refs{Path.DirectorySeparatorChar}"));
        }
    }
}