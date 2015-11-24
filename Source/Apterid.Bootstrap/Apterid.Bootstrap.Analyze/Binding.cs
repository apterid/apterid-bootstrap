using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Binding : Scope
    {
        public bool IsPublic { get; internal set; }
        public Annotation Annotation { get; internal set; }
        public Expression Expression { get; internal set; }
    }
}
