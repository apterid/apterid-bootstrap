using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronMeta.Matcher;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Token : Node
    {
    }

    public class Keyword : Token
    {
    }

    public class Identifier : Token
    {
    }

    public class QualifiedIdentifier : Identifier
    {
        IList<Identifier> qualifiers;

        public Identifier Identifier { get; set; }

        public IEnumerable<Identifier> Qualifiers
        {
            get
            {
                return qualifiers ?? (qualifiers = new List<Identifier>());
            }
            set
            {
                qualifiers = value.ToList();
            }
        }

        public override string Text
        {
            get { return Identifier.Text; }
        }
    }
}
