using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    public class AnalyzeSourceFile : CompilerStep<AnalysisUnit>
    {
        ParseUnit ParseUnit { get; }
        ApteridAnalyzer Analyzer { get; set; }

        public AnalyzeSourceFile(CompilerContext context, AnalysisUnit analysisUnit, ParseUnit parseUnit)
            : base(context, analysisUnit)
        {
            ParseUnit = parseUnit;
        }

        public override async Task RunAsync(CancellationToken cancel)
        {
            if (Analyzer == null)
                Analyzer = new ApteridAnalyzer(Context, ParseUnit.SourceFile, Unit);

            await Analyzer.Analyze(cancel);
        }
    }
}
