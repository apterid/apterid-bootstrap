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
        public IList<Reference> References { get; set; }
    }
}
