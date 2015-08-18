using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse
{
    public class SourceText
    {
        public string Identifier { get; }
        public IEnumerable<char> Buffer { get; }
        public Syntax.Node ParseTree { get; set; }

        public SourceText(string identifier, IEnumerable<char> buffer)
        {
            Identifier = identifier;
            Buffer = buffer;
        }
    }
}
