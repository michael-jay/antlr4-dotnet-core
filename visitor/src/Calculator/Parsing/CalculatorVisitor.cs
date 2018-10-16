using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

using OperandContext = CalculatorParser.OperandContext;
using ExpressionContext = CalculatorParser.ExpressionContext;

namespace Calculator.Parsing
{
    internal class CalculatorVisitor : CalculatorBaseVisitor<int>
    {
        #region Member Variables
        private readonly Dictionary<string,Func<int,int,int>> _funcMap =
            new Dictionary<string, Func<int,int,int>>
            {
                {"+", (a, b) => a + b},
                {"-", (a, b) => a - b},
                {"*", (a, b) => a * b},
                {"/", (a, b) => a / b}
            };
        #endregion

        #region Base Class Overrides
        public override int VisitExpression([NotNull]ExpressionContext context)
        {
            return HandleGroup(context.operand(), context.OPERATOR());
        }

        public override int VisitOperand([NotNull]OperandContext context)
        {
            ITerminalNode digit = context.DIGIT();

            return digit != null
                ? int.Parse(digit.GetText())
                : HandleGroup(context.operand(), context.OPERATOR());
        }
        #endregion

        #region Utility Methods
        private int HandleGroup(OperandContext[] operandCtxs, ITerminalNode[] operatorNodes)
        {
            List<int> operands = operandCtxs.Select(Visit).ToList();
            Queue<string> operators = new Queue<string>(operatorNodes.Select(o => o.GetText()));

            return operands.Aggregate((a, c) => _funcMap[operators.Dequeue()](a, c));
        }
        #endregion
    }
}
