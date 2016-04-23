// Copyright (C) 2016 The Apterid Developers - See LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Apterid.Bootstrap.Analyze.Abstract;
using Apterid.Bootstrap.Analyze.Abstract.Expressions;
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

        Analyzer.SourceAnalyzer SourceAnalyzer { get; }
        TypeResolver TypeResolver { get; }

        public ApteridAnalyzer(Context context, AnalysisUnit analyzeUnit, ParsedSourceFile sourceFile)
        {
            Context = context;
            Unit = analyzeUnit;
            SourceFile = sourceFile;

            SourceAnalyzer = new Analyzer.SourceAnalyzer(this);

            var rtr = new ReferenceTypeResolver(context.References.Values);
            TypeResolver = new ScopeTypeResolver(rtr);
        }

        public void Analyze(CancellationToken cancel)
        {
            var sourceNode = SourceFile.ParseTree as Parse.Syntax.Source;
            if (sourceNode == null)
            {
                Unit.AddError(new AnalyzerError(SourceFile.ParseTree, string.Format(ErrorMessages.E_0010_Analyzer_ParseTreeIsNotSource, SourceFile.Name)));
                return;
            }

            var modules = SourceAnalyzer.AnalyzeSource(sourceNode, cancel).Result;
            if (Unit.Errors.Any() && Context.AbortOnError)
                return;

            ResolveTypes(modules, cancel);
        }

        #region Semantic Analysis

        #endregion

        #region Type Resolution

        struct TypeResolveRec
        {
            public Goal<AType> Constraint { get; set; }
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
                        Unit.AddError(new AnalyzerError(e.SyntaxNode, string.Format(ErrorMessages.E_0015_Analyzer_UnableToInferType, ApteridError.Truncate(e.SyntaxNode.Text))));
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
            tvr = e.Children.OfType<Expression>().Aggregate(tvr, (tvrc, c) => ResolveExpressionType(tvrc, c));
            var v = Var.NewVar();
            var result = new TypeResolveRec
            {
                Constraint = Goal.Conj(tvr.Constraint, e.ResolveType(Unit, TypeResolver, v)),
                ExpTypeVars = tvr.ExpTypeVars.Concat(new[] { Tuple.Create(e, v) }),
            };
            return result;
        }

        #endregion
    }

    public class AnalyzerError : NodeError
    {
        public AnalyzerError()
        {
        }

        public AnalyzerError(Parse.Syntax.Node node, string message)
        {
            ErrorNode = node;
            Message = message;
        }
    }
}
