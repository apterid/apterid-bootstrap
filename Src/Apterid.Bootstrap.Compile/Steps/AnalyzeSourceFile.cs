// Copyright (C) 2015 The Apterid Developers - See LICENSE

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

        public override Action GetStepAction(CancellationToken cancel)
        {
            return () =>
            {
                if (Analyzer == null)
                {
                    lock (this)
                    {
                        if (Analyzer == null)
                            Analyzer = new ApteridAnalyzer(Context, Unit, ParseUnit.SourceFile);
                    }
                }

                try
                {
                    Analyzer.Analyze(cancel);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Unit.AddError(new AnalyzerError { Exception = e });
                }
            };
        }
    }
}
