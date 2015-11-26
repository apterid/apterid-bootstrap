// Copyright (C) 2015 The Apterid Developers - See LICENSE

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

        public override Action GetStepAction(CancellationToken cancel)
        {
            return () =>
            {
                try
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
                        lock (Unit)
                        {
                            if (Unit.AnalysisUnit == null)
                            {
                                Unit.AnalysisUnit = new AnalysisUnit
                                {
                                    ParseUnits = Unit.ParseUnits
                                };
                            }
                        }
                    }

                    if (Unit.GenerationUnit == null)
                    {
                        lock (Unit)
                        {
                            if (Unit.GenerationUnit == null)
                            {
                                Unit.GenerationUnit = new GenerationUnit
                                {
                                    Mode = Unit.Mode,
                                    ParseUnits = Unit.ParseUnits,
                                    AnalysisUnit = Unit.AnalysisUnit,
                                };
                            }
                        }
                    }

                    // tasks
                    var parseAndAnalyzeActions = new List<Tuple<Action, Action>>();

                    foreach (var parseUnit in Unit.ParseUnits)
                    {
                        Action parse = null;
                        Action analyze = null;

                        if ((Unit.Mode & CompileOutputMode.Parse) != 0)
                        {
                            parse = new ParseSourceFile(Context, parseUnit).GetStepAction(Context.CancelSource.Token);
                        }

                        if ((Unit.Mode & CompileOutputMode.Analyze) != 0)
                        {
                            analyze = new AnalyzeSourceFile(Context, Unit.AnalysisUnit, parseUnit).GetStepAction(Context.CancelSource.Token);
                        }

                        parseAndAnalyzeActions.Add(Tuple.Create(parse, analyze));
                    }

                    var tasks = parseAndAnalyzeActions
                        .Select(pa =>
                        {
                            if (pa.Item1 != null && pa.Item2 != null)
                            {
                                var parse = Task.Factory.StartNew(pa.Item1, TaskCreationOptions.AttachedToParent);
                                return parse.ContinueWith(t => pa.Item2());
                            }
                            else if (pa.Item1 != null)
                            {
                                return Task.Factory.StartNew(pa.Item1, TaskCreationOptions.AttachedToParent);
                            }
                            else if (pa.Item2 != null)
                            {
                                return Task.Factory.StartNew(pa.Item2, TaskCreationOptions.AttachedToParent);
                            }
                            else
                            {
                                return null;
                            }
                        })
                        .Where(t => t != null);

                    Task.WhenAll(tasks).Wait();

                    if ((Unit.Mode & CompileOutputMode.Generate) != 0 && !Unit.Errors.Any())
                    {
                        var generate = new GenerateAssembly(Context, Unit).GetStepAction(Context.CancelSource.Token);
                        var task = Task.Factory.StartNew(generate, TaskCreationOptions.AttachedToParent);
                        task.Wait();
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Unit.AddError(new CompilerError { Exception = e });
                }
            };
        }
    }
}
