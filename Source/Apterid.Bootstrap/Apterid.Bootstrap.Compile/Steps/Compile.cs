using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;

namespace Apterid.Bootstrap.Compile.Steps
{
    class Compile : CompilerStep
    {
        public Compile(CompilerContext context, CompilationUnit compileUnit)
            : base(context, compileUnit)
        {
            Unit.AnalyzeUnit = new AnalysisUnit();

            // TODO: make dummy modules from references

            var parseAndAnalyze = new CompilerStep(context, compileUnit)
            {
                SubSteps = Unit.SourceFiles
                    .Select(sourceFile =>
                    {
                        var parseStep = new ParseSourceFile(Context, Unit, sourceFile);

                        var analyzeStep = new AnalyzeSourceFile(Context, Unit, sourceFile);
                        parseStep.Continuation = analyzeStep;

                        return parseStep;
                    })
                    .OfType<CompilerStep>()
                    .ToList()
            };

            var generate = new Generate(context, compileUnit);

            parseAndAnalyze.Continuation = generate;

            SubSteps = new List<CompilerStep>
            {
                parseAndAnalyze
            };
        }
    }
}
