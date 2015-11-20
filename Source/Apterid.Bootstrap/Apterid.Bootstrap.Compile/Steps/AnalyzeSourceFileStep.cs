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
    public class AnalyzeSourceFileStep : CompileStep
    {
        public ParserSourceFile SourceFile { get; }
        internal ApteridAnalyzer Analyzer { get; set; }

        public AnalyzeSourceFileStep(
            CompileContext context, 
            CompileUnit compileUnit, 
            ParserSourceFile sourceFile)
            : base(context, compileUnit)
        {
            SourceFile = sourceFile;
        }

        public override StepResult Run()
        {
            // TODO: make dummy modules from references

            try
            {
                if (Analyzer == null)
                    Analyzer = new ApteridAnalyzer(Context, SourceFile, CompileUnit.AnalyzeUnit, Context.CancelSource.Token);

                Analyzer.Analyze();

                if (Analyzer.NeedsRerun)
                    this.Continuation = new AnalyzeSourceFileStep(Context, CompileUnit, SourceFile) { Analyzer = Analyzer };
            }
            catch (OperationCanceledException)
            {
                return Canceled();
            }
            catch (ApteridException e)
            {
                CompileUnit.AddError(new AnalyzeError { Exception = e });
            }

            return Context.Errors.Any() ? Failed() : Succeeded();
        }
    }
}
