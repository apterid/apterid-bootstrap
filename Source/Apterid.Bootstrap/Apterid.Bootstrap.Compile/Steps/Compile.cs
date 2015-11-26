using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze;
using Apterid.Bootstrap.Common;
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
            if (Unit.ParseUnits == null)
            {
                Unit.ParseUnits = Unit.SourceFiles
                    .Where(sf => (Unit.Mode & CompileOutputMode.SaveToFile) == 0
                                 || Context.ForceRecompile
                                 || !sf.Exists
                                 || Unit.OutputFileInfo == null
                                 || sf.LastWriteTimeUtc > Unit.OutputFileInfo.LastWriteTimeUtc)
                    .Select(sf => new ParseUnit { SourceFile = sf })
                    .ToList();
            }

            // TODO: make dummy modules from references

            if (Unit.AnalysisUnit == null)
            {
                Unit.AnalysisUnit = new AnalysisUnit
                {
                    ParseUnits = Unit.ParseUnits
                };
            }

            if (Unit.GenerationUnit == null)
            {
                Unit.GenerationUnit = new GenerationUnit
                {
                    Mode = Unit.Mode,
                    ParseUnits = Unit.ParseUnits,
                    AnalysisUnit = Unit.AnalysisUnit,
                };
            }

            // tasks
            var tasksToWait = new List<Task>();

            foreach (var parseUnit in Unit.ParseUnits)
            {
                Task parse = null;
                Task analyze = null;

                if ((Unit.Mode & CompileOutputMode.Parse) != 0)
                {
                    parse = new ParseSourceFile(Context, parseUnit).RunAsync(Context.CancelSource.Token);
                    tasksToWait.Add(parse);
                    parse.Start();
                }

                if ((Unit.Mode & CompileOutputMode.Analyze) != 0)
                {
                    analyze = new AnalyzeSourceFile(Context, Unit.AnalysisUnit, parseUnit).RunAsync(Context.CancelSource.Token);

                    if (parse == null)
                    {
                        tasksToWait.Add(parse.ContinueWith(t => analyze));
                    }
                    else
                    {
                        tasksToWait.Add(analyze);
                        analyze.Start();
                    }
                }
            }

            await Task.WhenAll(tasksToWait);

            if ((Unit.Mode & CompileOutputMode.Generate) != 0)
            {
                await new GenerateAssembly(Context, Unit).RunAsync(Context.CancelSource.Token);
            }
        }
    }
}
