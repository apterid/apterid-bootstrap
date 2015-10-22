using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse
{
    public class ApteridError
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public ParserSourceFile SourceFile { get; set; }
        public Syntax.Node ErrorNode { get; set; }
        public int ErrorIndex { get; set; }
    }
}
