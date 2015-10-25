using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    public class AnalyzeSourceFileStep : CompilerStep
    {
        public ParserSourceFile SourceFile { get; }

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
            Context.Analyzer.Analyze(CompileUnit.AnalyzeUnit, SourceFile);
            return Context.Errors.Any() ? Failed() : Succeeded();
        }
    }
}
