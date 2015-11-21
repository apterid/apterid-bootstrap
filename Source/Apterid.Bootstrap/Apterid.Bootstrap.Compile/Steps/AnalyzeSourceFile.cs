using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    public class AnalyzeSourceFile : CompilerStep
    {
        public ParserSourceFile SourceFile { get; }
        internal ApteridAnalyzer Analyzer { get; set; }

        public AnalyzeSourceFile(
            CompilerContext context, 
            CompilationUnit compileUnit, 
            ParserSourceFile sourceFile)
            : base(context, compileUnit)
        {
            SourceFile = sourceFile;
        }

        public override StepResult Run()
        {
            try
            {
                if (Analyzer == null)
                    Analyzer = new ApteridAnalyzer(Context, SourceFile, Unit.AnalyzeUnit, Context.CancelSource.Token);

                Analyzer.Analyze();

                if (Analyzer.NeedsRerun)
                    this.Continuation = new AnalyzeSourceFile(Context, Unit, SourceFile) { Analyzer = Analyzer };
            }
            catch (OperationCanceledException)
            {
                return Canceled();
            }
            catch (ApteridException e)
            {
                Unit.AddError(new AnalyzerError { Exception = e });
            }

            return Context.Errors.Any() ? Failed() : Succeeded();
        }
    }
}
