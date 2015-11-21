using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Module : Scope
    {
        public Scope Parent { get; internal set; }

        public IList<Type> Types { get; } = new List<Type>();
        public IList<Binding> Bindings { get; } = new List<Binding>();
    }
}
