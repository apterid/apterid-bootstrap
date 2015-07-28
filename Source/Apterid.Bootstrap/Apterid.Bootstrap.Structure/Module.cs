using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Structure
{
    /// <summary>
    /// Stores information about a module.
    /// </summary>
    public class Module
    {
        public Project Project { get; set; }
        public IList<Function> Functions { get; set; }
    }
}
