using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    /// <summary>
    /// Stores information about a module.
    /// </summary>
    public class Module
    {
        public Assembly Project { get; set; }
        public IList<Function> Functions { get; set; }
    }
}
