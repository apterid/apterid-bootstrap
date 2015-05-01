using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compiler.Context
{
    /// <summary>
    /// Holds information about all solutions that are referenced in our context.
    /// </summary>
    class Workspace
    {
        public ICollection<Solution> Solutions { get; set; }
    }
}
