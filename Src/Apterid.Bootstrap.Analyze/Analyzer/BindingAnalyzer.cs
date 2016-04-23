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

namespace Apterid.Bootstrap.Analyze.Analyzer
{
    class BindingAnalyzer : Analyzer
    {
        public BindingAnalyzer(ApteridAnalyzer parent)
            : base(parent)
        {
        }

        public void AnalyzeBinding(Module module, Parse.Syntax.Binding bindingNode, Binding binding, CancellationToken cancel)
        {
            if (bindingNode.Body == null || !bindingNode.Body.Any())
            {
                Unit.AddError(new AnalyzerError(bindingNode, string.Format(ErrorMessages.E_0014_Analyzer_EmptyBinding, bindingNode.Name.Text)));
                return;
            }

            var triviaNodes = new List<Parse.Syntax.Node>();
            Expression expression = null;

            Parse.Syntax.FunctionLiteral functionLiteralNode;
            Parse.Syntax.Literal literalNode;

            foreach (var node in bindingNode.Body)
            {
                if (cancel.IsCancellationRequested)
                    throw new OperationCanceledException(cancel);

                if ((functionLiteralNode = node as Parse.Syntax.FunctionLiteral) != null)
                {

                }
                else if ((literalNode = node as Parse.Syntax.Literal) != null)
                {
                    expression = AnalyzeLiteral(module, literalNode, cancel);
                }
                else if (node is Parse.Syntax.Space)
                {
                    triviaNodes.Add(node);
                }
            }

            if (expression == null)
            {
                Unit.AddError(new AnalyzerError(bindingNode, string.Format(ErrorMessages.E_0014_Analyzer_EmptyBinding, bindingNode.Name.Text)));
                return;
            }

            foreach (var tn in triviaNodes)
                expression.PostTrivia.Add(tn);

            binding.Expression = expression;
        }



        Expression AnalyzeLiteral(Module module, Parse.Syntax.Literal literalNode, CancellationToken cancel)
        {
            if (literalNode.ValueType == typeof(BigInteger))
            {
                return new IntegerLiteral((BigInteger)literalNode.Value, literalNode);
            }
            else if (literalNode.ValueType == typeof(bool))
            {
                return new Literal<bool>((bool)literalNode.Value, literalNode);
            }
            else
            {
                throw new NotImplementedException(string.Format("Literals of type {0} not implemented yet.", literalNode.ValueType.Name));
            }
        }
    }
}
