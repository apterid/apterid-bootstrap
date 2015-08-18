using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile
{
    public class BuildError
    {
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public SourceText SourceText { get; set; }
        public int ErrorIndex { get; set; }
    }
}
