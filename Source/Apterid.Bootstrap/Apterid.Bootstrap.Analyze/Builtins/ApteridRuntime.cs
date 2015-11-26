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

        private ApteridRuntime()
        {
            Name = new QualifiedName { Name = "Apterid" };
        }

        public class ApteridModule : Type
        {
            public static ApteridModule Instance { get; } = new ApteridModule();

            private ApteridModule()
            {
                Name = new QualifiedName(ApteridRuntime.Instance, "Module");
            }
        }
    }
}
