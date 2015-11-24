using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verophyle.CSLogic;

namespace Apterid.Bootstrap.Analyze
{
    public abstract class Expression : Scope
    {
        public Type ResolvedType { get; internal set; }

        public abstract Goal<Type> ResolveType(Var type);

        public IList<Expression> Children { get; } = new List<Expression>();
    }
}
