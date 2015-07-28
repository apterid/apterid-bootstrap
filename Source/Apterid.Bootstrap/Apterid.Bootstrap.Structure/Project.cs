using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Structure
{
    public class Project
    {
        public Solution Solution { get; set; }
        public IList<SourceFile> SourceFiles { get; set; }
        public IList<Module> Modules { get; set; }
    }
}
