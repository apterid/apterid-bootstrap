using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Common
{
    public enum OutputMode
    {
        Library,
        Executable,
    }

    public class Context
    {
        public bool ForceRecompile { get; set; }
        public bool AbortOnError { get; set; }

        public IDictionary<string, SourceFile> Sources { get; set; }
        public IDictionary<string, Reference> References { get; set; }
    }
}
