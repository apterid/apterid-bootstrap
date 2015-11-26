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
        public ParsedSourceFile SourceFile { get; }

        public ApteridAnalyzer(Context context, ParsedSourceFile sourceFile, AnalysisUnit analyzeUnit)
        {
            Context = context;
            Unit = analyzeUnit;
            SourceFile = sourceFile;
        }

        public void Analyze(CancellationToken cancel)
        {
            var sourceNode = SourceFile.ParseTree as Parse.Syntax.Source;
            if (sourceNode == null)
            {
                Unit.AddError(new AnalyzerError
                {
                    ErrorNode = SourceFile.ParseTree,
                    Message = string.Format(ErrorMessages.E_0010_Analyzer_ParseTreeIsNotSource, SourceFile.Name)
                });
                return;
            }

            var modules = AnalyzeSource(sourceNode, cancel).Result;
            if (Unit.Errors.Any() && Context.AbortOnError)
                return;

            ResolveTypes(modules, cancel);
        }

        #region Semantic Analysis

        Task<Module[]> AnalyzeSource(Parse.Syntax.Source sourceNode, CancellationToken cancel)
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
                        Qualifiers = moduleNode.Qualifiers.Select(id => id.Text),
                        Name = moduleNode.Name.Text
                    };

                    lock (Unit.Modules)
                    {
                        if (!Unit.Modules.TryGetValue(moduleName, out curModule))
                        {
                            curModule = new Module { Name = moduleName };
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
                    Unit.AddError(new AnalyzerError
                    {
                        ErrorNode = node,
                        Message = string.Format(ErrorMessages.E_0011_Analyzer_InvalidToplevelItem, ApteridError.Truncate(node.Text)),
                    });                    
                }
            }

            if (curModule != null)
            {
                foreach (var tn in triviaNodes)
                    curModule.PostTrivia.Add(tn);
            }

            // analyze
            var tasks = nodesAndModules.Select(mm => AnalyzeModule(mm.Item1, mm.Item2, cancel));
            return Task.WhenAll(tasks);
        }

        Task<Module> AnalyzeModule(Parse.Syntax.Module moduleNode, Module module, CancellationToken cancel)
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
                            Unit.AddError(new AnalyzerError
                            {
                                ErrorNode = node,
                                Message = string.Format(ErrorMessages.E_0012_Analyzer_DuplicateBinding, bindingName.Name),
                            });
                        }
                        else
                        {
                            curBinding = new Binding { Parent = module, Name = bindingName, SyntaxNode = bindingNode };
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
                    Unit.AddError(new AnalyzerError
                    {
                        ErrorNode = node,
                        Message = string.Format(ErrorMessages.E_0013_Analyzer_InvalidScopeItem, ApteridError.Truncate(node.Text)),
                    });
                }
            }

            if (curBinding != null)
            {
                foreach (var tn in triviaNodes)
                    curBinding.PostTrivia.Add(tn);
            }

            // analyze
            var tasks = bindings.Select(bb => Task.Factory.StartNew(() => AnalyzeBinding(module, bb.Item1, bb.Item2, cancel), TaskCreationOptions.AttachedToParent));
            return Task.WhenAll(tasks).ContinueWith(t => module);
        }

        void AnalyzeBinding(Module module, Parse.Syntax.Binding bindingNode, Binding binding, CancellationToken cancel)
        {
            if (bindingNode.Body == null || !bindingNode.Body.Any())
            {
                Unit.AddError(new AnalyzerError
                {
                    ErrorNode = bindingNode,
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
                if (cancel.IsCancellationRequested)
                    throw new OperationCanceledException(cancel);

                if ((literalNode = node as Parse.Syntax.Literal) != null)
                {
                    if ((bigIntLiteral = literalNode as Parse.Syntax.Literal<BigInteger>) != null)
                    {
                        expression = new Expressions.IntegerLiteral(bigIntLiteral.Value) { SyntaxNode = bigIntLiteral };
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
                    ErrorNode = bindingNode,
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

        void ResolveTypes(IEnumerable<Module> modules, CancellationToken cancel)
        {
            var trr = new TypeResolveRec
            {
                Constraint = s => new[] { s }, // true
                ExpTypeVars = Enumerable.Empty<Tuple<Expression, Var>>(),
            };

            trr = modules.Aggregate(trr, (mtr, m) =>
            {
                if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                return m.Bindings.Values.Aggregate(mtr, (btr, b) =>
                {
                    if (cancel.IsCancellationRequested) throw new OperationCanceledException(cancel);

                    return ResolveExpressionType(btr, b.Expression);
                });
            });

            try
            {
                var varTypes = Goal.Eval(trr.Constraint).First();

                foreach (var expVar in trr.ExpTypeVars)
                {
                    var e = expVar.Item1;
                    var v = expVar.Item2;

                    if (varTypes.Binds(v))
                    {
                        e.ResolvedType = varTypes[v];
                    }
                    else
                    {
                        Unit.AddError(new AnalyzerError
                        {
                            ErrorNode = e.SyntaxNode,
                            Message = string.Format(ErrorMessages.E_0015_Analyzer_UnableToInferType, ApteridError.Truncate(e.SyntaxNode.Text)),
                        });
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                Unit.AddError(new AnalyzerError { Exception = e });
            }
        }

        TypeResolveRec ResolveExpressionType(TypeResolveRec tvr, Expression e)
        {
            tvr = e.Children.Aggregate(tvr, (tvrc, c) => ResolveExpressionType(tvrc, c));
            var v = Var.NewVar();
            var result = new TypeResolveRec
            {
                Constraint = Goal.Conj(tvr.Constraint, e.ResolveType(v)),
                ExpTypeVars = tvr.ExpTypeVars.Concat(new[] { Tuple.Create(e, v) }),
            };
            return result;
        }

        #endregion
    }

    public class AnalyzerError : NodeError
    {
    }
}
