using System;
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
        public AnalyzeUnit Unit { get; private set; }
        public ParserSourceFile SourceFile { get; }
        protected CancellationToken Cancel { get; }

        public bool NeedsRerun { get; private set; }

        public ApteridAnalyzer(Context context, ParserSourceFile sourceFile, AnalyzeUnit analyzeUnit, CancellationToken cancel)
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
                Unit.AddError(new AnalyzeError { Node = SourceFile.ParseTree, Message = string.Format(ErrorMessages.EA_0010_Analyzer_ParseTreeIsNotSource, SourceFile.Name) });
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
                    module = new Module
                    {
                        Name = new QualifiedName
                        {
                            Qualifiers = moduleNode.Qualifiers.Select(id => id.Text),
                            Name = moduleNode.Name.Text
                        },
                    };

                    foreach (var tn in triviaNodes)
                        module.PreTrivia.Add(tn);
                    triviaNodes.Clear();

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
                    Unit.AddError(new AnalyzeError
                    {
                        Node = node,
                        Message = string.Format(ErrorMessages.E_0011_Analyzer_InvalidToplevelItem, ApteridError.Truncate(node.Text)),
                    });
                    return;
                }
            }

            if (module != null && triviaNodes.Count > 0)
            {
                foreach (var tn in triviaNodes)
                    module.PostTrivia.Add(tn);
            }

            // add module to the analyze unit
            if (module != null)
                Unit.Modules[module.Name] = module;

            // don't complain about no module; can be an empty source file
        }

        void AnalyzeModule(Module module, Parse.Syntax.Module moduleNode)
        {
            // get types and bindings
            var triviaNodes = new List<Parse.Syntax.Node>();

            IList<Type> types = new List<Type>();
            IList<Binding> bindings = new List<Binding>();

            Parse.Syntax.Binding bindingNode = null;

            foreach (var node in moduleNode.Children)
            {
                if (Cancel.IsCancellationRequested)
                    throw new OperationCanceledException(Cancel);

                if ((bindingNode = node as Parse.Syntax.Binding) != null)
                {
                    var binding = new Binding
                    {
                        Name = new QualifiedName(module, bindingNode.Name.Text)
                    };
                }
                else
                {
                    triviaNodes.Add(node);
                }
            }
        }
    }

    public class AnalyzeError : ApteridError
    {
        public Parse.Syntax.Node Node { get; set; }
    }
}
