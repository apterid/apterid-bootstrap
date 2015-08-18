using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Module : Scope
    {
        public IList<Type> Types { get; set; }
        public IList<Binding> Bindings { get; set; }
    }
}
