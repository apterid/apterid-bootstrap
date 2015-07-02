using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse.Syntax
{
    public abstract class Node
    {
        public SourceText Source { get; set; }
        public int Index { get; set; }
        public int Next { get; set; }
    }
}
