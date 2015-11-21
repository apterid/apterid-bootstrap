﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;

namespace Apterid.Bootstrap.Analyze
{
    public class ApteridAnalyzer
    {
        public Context Context { get; }
        public AnalysisUnit Unit { get; private set; }
        public ParserSourceFile SourceFile { get; }
        protected CancellationToken Cancel { get; }

        public bool NeedsRerun { get; private set; }

        public ApteridAnalyzer(Context context, ParserSourceFile sourceFile, AnalysisUnit analyzeUnit, CancellationToken cancel)
        {
            Context = context;
            Unit = analyzeUnit;
            SourceFile = sourceFile;
            Cancel = cancel;
        }

        public void Analyze()
        {
            var sourceNode = SourceFile.ParseTree as Parse.Syntax.Source;
            if (sourceNode == null)
            {
                Unit.AddError(new AnalyzerError { Node = SourceFile.ParseTree, Message = string.Format(ErrorMessages.E_0010_Analyzer_ParseTreeIsNotSource, SourceFile.Name) });
                return;
            }

            AnalyzeSource(sourceNode);
        }

        void AnalyzeSource(Parse.Syntax.Source sourceNode)
        {
            // look for top-level modules
            var triviaNodes = new List<Parse.Syntax.Node>();

            Module module = null;
            Parse.Syntax.Module moduleNode = null;

            foreach (var node in sourceNode.Children)
            {
                if (Cancel.IsCancellationRequested)
                    throw new OperationCanceledException(Cancel);

                if ((moduleNode = node as Parse.Syntax.Module) != null)
                {
                    // look for existing module
                    var moduleName = new QualifiedName
                    {
                        Qualifiers = moduleNode.Qualifiers.Select(id => id.Text),
                        Name = moduleNode.Name.Text
                    };

                    lock (Unit.Modules)
                    {
                        if (!Unit.Modules.TryGetValue(moduleName, out module))
                        {
                            module = new Module { Name = moduleName };
                            Unit.Modules.Add(module.Name, module);
                        }

                        foreach (var tn in triviaNodes)
                            module.PreTrivia.Add(tn);
                        triviaNodes.Clear();
                    }

                    AnalyzeModule(module, moduleNode);
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
                    Unit.AddError(new AnalyzerError
                    {
                        Node = node,
                        Message = string.Format(ErrorMessages.E_0011_Analyzer_InvalidToplevelItem, ApteridError.Truncate(node.Text)),
                    });
                    return;
                }
            }

            if (module != null)
            {
                foreach (var tn in triviaNodes)
                    module.PostTrivia.Add(tn);
            }

            // don't complain about no module; can be an empty source file
        }

        void AnalyzeModule(Module module, Parse.Syntax.Module moduleNode)
        {
            var triviaNodes = new List<Parse.Syntax.Node>();

            Binding binding = null;
            Parse.Syntax.Binding bindingNode = null;

            foreach (var node in moduleNode.Children)
            {
                if (Cancel.IsCancellationRequested)
                    throw new OperationCanceledException(Cancel);

                if ((bindingNode = node as Parse.Syntax.Binding) != null)
                {
                    var bindingName = new QualifiedName(module, bindingNode.Name.Text);

                    lock (module.Bindings)
                    {
                        if (module.Bindings.TryGetValue(bindingName, out binding))
                        {
                            Unit.AddError(new AnalyzerError
                            {
                                Node = node,
                                Message = string.Format(ErrorMessages.E_0012_Analyzer_DuplicateBinding, bindingName.Name),
                            });
                            return;
                        }
                        else
                        {
                            binding = new Binding { Parent = module, Name = bindingName };
                            module.Bindings.Add(binding.Name, binding);
                        }

                        foreach (var tn in triviaNodes)
                            binding.PreTrivia.Add(tn);
                        triviaNodes.Clear();
                    }

                    AnalyzeBinding(module, binding, bindingNode);
                }
                else if (node is Parse.Syntax.Space)
                {
                    triviaNodes.Add(node);
                }
                else
                {
                    Unit.AddError(new AnalyzerError
                    {
                        Node = node,
                        Message = string.Format(ErrorMessages.E_0013_Analyzer_InvalidScopeItem, ApteridError.Truncate(node.Text)),
                    });
                    return;
                }
            }

            if (binding != null)
            {
                foreach (var tn in triviaNodes)
                    binding.PostTrivia.Add(tn);
            }
        }

        void AnalyzeBinding(Module module, Binding binding, Parse.Syntax.Binding bindingNode)
        {

        }
    }

    public class AnalyzerError : ApteridError
    {
        public Parse.Syntax.Node Node { get; set; }
    }
}
