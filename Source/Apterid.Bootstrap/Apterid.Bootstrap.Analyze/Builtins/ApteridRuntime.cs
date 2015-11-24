using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze.Builtins
{
    /// <summary>
    /// Proxy for Apterid.
    /// </summary>
    internal class ApteridRuntime : Scope
    {
        public static ApteridRuntime Instance { get; } = new ApteridRuntime();

        public ApteridRuntime()
        {
            Name = new QualifiedName { Name = "Apterid" };
        }
    }
}
