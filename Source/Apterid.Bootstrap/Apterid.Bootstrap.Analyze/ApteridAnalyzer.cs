using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Apterid.Bootstrap.Common;
using Apterid.Bootstrap.Parse;
using Verophyle.CSLogic;

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
                Unit.AddError(new AnalyzerError
                {
                    Node = SourceFile.ParseTree,
                    Message = string.Format(ErrorMessages.E_0010_Analyzer_ParseTreeIsNotSource, SourceFile.Name)
                });
                return;
            }

            var modules = AnalyzeSource(sourceNode).ToList();
            if (Unit.Errors.Any())
                return;

            ResolveTypes(modules);
        }

        #region Semantic Analysis

        IEnumerable<Module> AnalyzeSource(Parse.Syntax.Source sourceNode)
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
                    yield return module;
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
                    yield break;
                }
            }

            if (module != null)
            {
                foreach (var tn in triviaNodes)
                    module.PostTrivia.Add(tn);
            }
        }

        void AnalyzeModule(Module module, Parse.Syntax.Module moduleNode)
        {
            var triviaNodes = new List<Parse.Syntax.Node>();

            Binding binding = null;
            Parse.Syntax.Binding bindingNode = null;

            foreach (var node in moduleNode.Body)
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
            if (bindingNode.Body == null || !bindingNode.Body.Any())
            {
                Unit.AddError(new AnalyzerError
                {
                    Node = bindingNode,
                    Message = string.Format(ErrorMessages.E_0014_Analyzer_EmptyBinding, bindingNode.Name.Text),
                });
                return;
            }

            var triviaNodes = new List<Parse.Syntax.Node>();
            Expression expression = null;

            Parse.Syntax.Literal literalNode;
            Parse.Syntax.Literal<BigInteger> bigIntLiteral;

            foreach (var node in bindingNode.Body)
            {
                if (Cancel.IsCancellationRequested)
                    throw new OperationCanceledException(Cancel);

                if ((literalNode = node as Parse.Syntax.Literal) != null)
                {
                    if ((bigIntLiteral = literalNode as Parse.Syntax.Literal<BigInteger>) != null)
                    {
                        expression = new Expressions.IntegerLiteral(bigIntLiteral.Value);
                    }
                    else
                    {
                        throw new NotImplementedException(string.Format("Literals of type {0} not implemented yet.", literalNode.ValueType.Name));
                    }
                }
                else if (node is Parse.Syntax.Space)
                {
                    triviaNodes.Add(node);
                }
            }

            if (expression == null)
            {
                Unit.AddError(new AnalyzerError
                {
                    Node = bindingNode,
                    Message = string.Format(ErrorMessages.E_0014_Analyzer_EmptyBinding, bindingNode.Name.Text),
                });
                return;
            }

            foreach (var tn in triviaNodes)
                expression.PostTrivia.Add(tn);

            binding.Expression = expression;
        }

        #endregion

        #region Type Resolution

        struct TypeResolveRec
        {
            public Goal<Type> Constraint { get; set; }
            public IEnumerable<Tuple<Expression, Var>> ExpTypeVars { get; set; }
        }

        bool ResolveTypes(IEnumerable<Module> modules)
        {
            var trr = new TypeResolveRec
            {
                Constraint = s => new[] { s }, // true
                ExpTypeVars = Enumerable.Empty<Tuple<Expression, Var>>(),
            };

            trr = modules.Aggregate(trr, (mtr, m) => m.Bindings.Values.Aggregate(mtr, (btr, b) => ResolveExpressionType(btr, b.Expression)));
            var varTypes = Goal.Eval(trr.Constraint).FirstOrDefault();

            foreach (var expVar in trr.ExpTypeVars)
            {
                var e = expVar.Item1;
                var v = expVar.Item2;

                if (!varTypes.Binds(v))
                {
                    Unit.AddError(new AnalyzerError
                    {
                        Node = e.SyntaxNode,
                        Message = string.Format(ErrorMessages.E_0015_Analyzer_UnableToInferType, ApteridError.Truncate(e.SyntaxNode.Text)),
                    });
                    return false;
                }

                e.ResolvedType = varTypes[v];
            }

            return true;
        }

        TypeResolveRec ResolveExpressionType(TypeResolveRec tvr, Expression e)
        {
            tvr = e.Children.Aggregate(tvr, (tvrc, c) => ResolveExpressionType(tvrc, c));
            var v = Var.NewVar();
            return new TypeResolveRec
            {
                Constraint = Goal.Conj(tvr.Constraint, e.ResolveType(v)),
                ExpTypeVars = tvr.ExpTypeVars.Concat(new[] { Tuple.Create(e, v) }),
            };
        }

        #endregion
    }

    public class AnalyzerError : ApteridError
    {
        public Parse.Syntax.Node Node { get; set; }
    }
}
