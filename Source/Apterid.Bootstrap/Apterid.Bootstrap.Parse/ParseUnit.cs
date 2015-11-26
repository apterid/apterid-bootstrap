using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Parse
{
    public class ParseUnit : Unit
    {
        public ParsedSourceFile SourceFile { get; set; }
        public ApteridParser Parser { get; set; }

        public override IEnumerable<Unit> Children => Enumerable.Empty<Unit>();
    }
}
