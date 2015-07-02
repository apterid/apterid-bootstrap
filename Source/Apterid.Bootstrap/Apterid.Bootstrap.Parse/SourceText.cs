using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apterid.Bootstrap.Parse
{
    public class SourceText
    {
        public string Identifier { get; set; }
        public IEnumerable<string> Buffer { get; set; }
    }
}
