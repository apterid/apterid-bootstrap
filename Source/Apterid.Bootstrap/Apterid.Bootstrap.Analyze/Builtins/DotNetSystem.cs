using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze.Builtins
{
    /// <summary>
    /// Proxy for System.
    /// </summary>
    internal class DotNetSystem : Scope
    {
        public static DotNetSystem Instance = new DotNetSystem();

        public DotNetSystem()
        {
            Name = new QualifiedName { Name = "System" };
        }

        public class Numerics : Scope
        {
            public static Numerics Instance = new Numerics();

            public Numerics()
            {
                Name = new QualifiedName(DotNetSystem.Instance, "Numerics");
            }
        }
    }
}
