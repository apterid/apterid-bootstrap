using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;

namespace Apterid.Bootstrap.Compile.Steps
{
    class CompileStep : CompilerStep
    {
        public CompileStep(CompileContext context, CompileUnit compileUnit)
            : base(context, compileUnit)
        {
            CompileUnit.AnalyzeUnit = new AnalyzeUnit();

            var parseAndAnalyze = new CompileStep(context, compileUnit)
            {
                SubSteps = CompileUnit.SourceFiles
                    .Select(sourceFile =>
                    {
                        var parseStep = new ParseSourceFileStep(Context, CompileUnit, sourceFile);

                        var analyzeStep = new AnalyzeSourceFileStep(Context, CompileUnit, sourceFile);
                        parseStep.Continuation = analyzeStep;

                        return parseStep;
                    })
                    .OfType<CompilerStep>()
                    .ToList()
            };

            var generate = new GenerateStep(context, compileUnit);

            parseAndAnalyze.Continuation = generate;

            SubSteps = new List<CompilerStep>
            {
                parseAndAnalyze
            };
        }
    }
}
