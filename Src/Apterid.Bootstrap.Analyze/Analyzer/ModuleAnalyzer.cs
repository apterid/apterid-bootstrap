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
    class ModuleAnalyzer : Analyzer
    {
        protected BindingAnalyzer BindingAnalyzer { get; }

        public ModuleAnalyzer(ApteridAnalyzer parent)
            : base(parent)
        {
            BindingAnalyzer = new BindingAnalyzer(parent);
        }

        public Task<Module> AnalyzeModule(Parse.Syntax.Module moduleNode, Module module, CancellationToken cancel)
        {
            var bindings = new List<Tuple<Parse.Syntax.Binding, Binding>>();

            // collect module bindings
            var triviaNodes = new List<Parse.Syntax.Node>();

            Binding curBinding = null;
            Parse.Syntax.Binding bindingNode = null;

            foreach (var node in moduleNode.Body)
            {
                if (cancel.IsCancellationRequested)
                    throw new OperationCanceledException(cancel);

                if ((bindingNode = node as Parse.Syntax.Binding) != null)
                {
                    var bindingName = new QualifiedName(module, bindingNode.Name.Text);

                    lock (module.Bindings)
                    {
                        if (module.Bindings.TryGetValue(bindingName, out curBinding))
                        {
                            Unit.AddError(new AnalyzerError(node, string.Format(ErrorMessages.E_0012_Analyzer_DuplicateBinding, bindingName.Name)));
                        }
                        else
                        {
                            curBinding = new Binding(bindingNode)
                            {
                                IsPublic = (bindingNode.Flags & Parse.Syntax.Flags.IsPublic) != 0,
                                Parent = module,
                                Name = bindingName
                            };

                            bindings.Add(Tuple.Create(bindingNode, curBinding));
                            module.Bindings.Add(curBinding.Name, curBinding);
                        }

                        foreach (var tn in triviaNodes)
                            curBinding.PreTrivia.Add(tn);
                        triviaNodes.Clear();
                    }
                }
                else if (node is Parse.Syntax.Space)
                {
                    triviaNodes.Add(node);
                }
                else
                {
                    Unit.AddError(new AnalyzerError(node, string.Format(ErrorMessages.E_0013_Analyzer_InvalidScopeItem, ApteridError.Truncate(node.Text))));
                }
            }

            if (curBinding != null)
            {
                foreach (var tn in triviaNodes)
                    curBinding.PostTrivia.Add(tn);
            }

            // analyze
            var tasks = bindings.Select(bb => Task.Factory.StartNew(() => BindingAnalyzer.AnalyzeBinding(module, bb.Item1, bb.Item2, cancel), TaskCreationOptions.AttachedToParent));
            return Task.WhenAll(tasks).ContinueWith(t => module);
        }
    }
}
