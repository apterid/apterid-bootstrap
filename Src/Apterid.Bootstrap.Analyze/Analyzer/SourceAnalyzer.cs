using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze.Abstract;
using Apterid.Bootstrap.Common;

namespace Apterid.Bootstrap.Analyze.Analyzer
{
    class SourceAnalyzer : Analyzer
    {
        ModuleAnalyzer ModuleAnalyzer { get; }

        public SourceAnalyzer(ApteridAnalyzer parent)
            : base(parent)
        {
            ModuleAnalyzer = new ModuleAnalyzer(parent);
        }

        public Task<Module[]> AnalyzeSource(Parse.Syntax.Source sourceNode, CancellationToken cancel)
        {
            var nodesAndModules = new List<Tuple<Parse.Syntax.Module, Module>>();

            // collect top-level modules
            var triviaNodes = new List<Parse.Syntax.Node>();
            Module curModule = null;
            Parse.Syntax.Module moduleNode = null;

            foreach (var node in sourceNode.Children)
            {
                if (cancel.IsCancellationRequested)
                    throw new OperationCanceledException(cancel);

                if ((moduleNode = node as Parse.Syntax.Module) != null)
                {
                    // look for existing module
                    var moduleName = new QualifiedName
                    {
                        Qualifiers = moduleNode.Name.Qualifiers.Select(id => id.Text).ToArray(),
                        Name = moduleNode.Name.Text
                    };

                    lock (Unit.Modules)
                    {
                        if (!Unit.Modules.TryGetValue(moduleName, out curModule))
                        {
                            curModule = new Module(moduleNode)
                            {
                                IsPublic = (moduleNode.Flags & Parse.Syntax.Flags.IsPublic) != 0,
                                Name = moduleName
                            };

                            Unit.Modules.Add(curModule.Name, curModule);
                        }

                        foreach (var tn in triviaNodes)
                            curModule.PreTrivia.Add(tn);
                        triviaNodes.Clear();
                    }

                    nodesAndModules.Add(Tuple.Create(moduleNode, curModule));
                }
                else if (node is Parse.Syntax.Directive)
                {
                    throw new NotImplementedException();
                }
                else if (node is Parse.Syntax.Space)
                {
                    triviaNodes.Add(node);
                }
                else
                {
                    Unit.AddError(new AnalyzerError(node, string.Format(ErrorMessages.E_0011_Analyzer_InvalidToplevelItem, ApteridError.Truncate(node.Text))));
                }
            }

            if (curModule != null)
            {
                foreach (var tn in triviaNodes)
                    curModule.PostTrivia.Add(tn);
            }

            // analyze
            var tasks = nodesAndModules.Select(mm => ModuleAnalyzer.AnalyzeModule(mm.Item1, mm.Item2, cancel));
            return Task.WhenAll(tasks);
        }
    }
}
