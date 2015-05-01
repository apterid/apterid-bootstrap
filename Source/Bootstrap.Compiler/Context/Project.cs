using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compiler.Context
{
    /// <summary>
    /// Holds information about compilation for a project (external references, project ASTs).
    /// </summary>
    class Project
    {
        public ICollection<Assembly> References { get; set; }
        
        public Assembly Output { get; set; }
    }
}
