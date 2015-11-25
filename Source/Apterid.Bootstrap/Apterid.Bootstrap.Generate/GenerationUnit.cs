using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Generate
{
    public class GenerationUnit : Unit
    {
        public OutputMode Mode { get; set; }
        public FileInfo OutputFileInfo { get; set; }

        public IList<ParseUnit> ParseUnits { get; set; }
        public AnalysisUnit AnalysisUnit { get; set; }

        public override IEnumerable<Unit> Children
        {
            get
            {
                if (AnalysisUnit != null)
                    return new[] { AnalysisUnit };
                else if (ParseUnits != null)
                    return ParseUnits;
                else
                    return Enumerable.Empty<Unit>();
            }
        }
    }
}
