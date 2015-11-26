using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Parse
{
    public class NodeError : ApteridError
    {
        public ParsedSourceFile SourceFile { get; set; }
        public Syntax.Node ErrorNode { get; set; }
        public int ErrorIndex { get; set; }
    }
}
