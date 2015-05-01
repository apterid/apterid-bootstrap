using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Compiler.Context
{
    /// <summary>
    /// Contains information about projects referenced in a solution that is in a workspace.
    /// </summary>
    class Solution
    {
        public ICollection<Project> Projects { get; set; }
    }
}
