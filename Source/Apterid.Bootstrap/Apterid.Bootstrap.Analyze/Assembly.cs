using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Analyze
{
    public class Assembly
    {
        public IList<SourceFile> Sources { get; set; }
        public IList<Module> Modules { get; set; }
    }
}
