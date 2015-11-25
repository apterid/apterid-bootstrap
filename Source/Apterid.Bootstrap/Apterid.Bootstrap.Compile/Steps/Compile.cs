using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Generate;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Compile.Steps
{
    class Compile : CompilerStep<CompilationUnit>
    {
        public Compile(CompilerContext context, CompilationUnit compilationUnit)
            : base(context, compilationUnit)
        {
        }

        public override async Task RunAsync(CancellationToken cancel)
        {
            // set up compilation units
            Unit.ParseUnits = Unit.SourceFiles
                .Where(sf => Context.ForceRecompile || !sf.Exists || sf.LastWriteTimeUtc > Unit.OutputFileInfo.LastWriteTimeUtc)
                .Select(sf => new ParseUnit { SourceFile = sf })
                .ToList();

            // TODO: make dummy modules from references
            Unit.AnalysisUnit = new AnalysisUnit
            {
                ParseUnits = Unit.ParseUnits
            };

            Unit.GenerationUnit = new GenerationUnit
            {
                Mode = Unit.Mode,
                OutputFileInfo = Unit.OutputFileInfo,

                ParseUnits = Unit.ParseUnits,
                AnalysisUnit = Unit.AnalysisUnit,
            };

            // tasks
            var parseAndAnalyzeTasks = Unit.ParseUnits
                .Select(async parseUnit =>
                {
                    await new ParseSourceFile(Context, parseUnit).RunAsync(Context.CancelSource.Token);
                    await new AnalyzeSourceFile(Context, Unit.AnalysisUnit, parseUnit).RunAsync(Context.CancelSource.Token);
                })
                .ToArray();

            await Task.WhenAll(parseAndAnalyzeTasks);
            await new GenerateAssembly(Context, Unit.GenerationUnit).RunAsync(Context.CancelSource.Token);
        }
    }
}
