using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Binding : Scope
    {
        public Annotation Annotation { get; set; }
        public Expression Value { get; set; }
    }
}
