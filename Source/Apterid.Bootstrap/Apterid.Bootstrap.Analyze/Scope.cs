using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Analyze
{
    public class Scope
    {
        public QualifiedName Name { get; internal set; }

        public IList<Parse.Syntax.Node> PreTrivia { get; } = new List<Parse.Syntax.Node>();
        public IList<Parse.Syntax.Node> PostTrivia { get; } = new List<Parse.Syntax.Node>();
    }
}
