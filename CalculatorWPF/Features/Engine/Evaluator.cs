namespace System.Windows.Calculator
{
    public class Evaluator
    {
        public double Evaluate(ExpressionNode node)
        {
            switch (node)
            {
                case NumberExpression number:
                    return EvaluateNumber(number);

                case UnaryExpression unary:
                    return EvaluateUnary(unary);

                case BinaryExpression binary:
                    return EvaluateBinary(binary);

                default:
                    throw new EvaluationException($"Unbekannter Knotentyp '{node.GetType().Name}'.");
            }
        }

        #region Number

        private double EvaluateNumber(NumberExpression number)
        {
            return number.Value;
        }

        #endregion

        #region Unary

        private double EvaluateUnary(UnaryExpression unary)
        {
            double value = Evaluate(unary.Operand);

            switch (unary.Operator)
            {
                case TokenType.Plus:
                    return value;

                case TokenType.Minus:
                    return -value;

                default:
                    throw new EvaluationException($"Unbekannter unärer Operator '{unary.Operator}'.");
            }
        }

        #endregion

        #region Binary

        private double EvaluateBinary(BinaryExpression binary)
        {
            double left = Evaluate(binary.Left);
            double right = Evaluate(binary.Right);

            switch (binary.Operator)
            {
                case BinaryOperator.Add:
                    return left + right;

                case BinaryOperator.Subtract:
                    return left - right;

                case BinaryOperator.Multiply:
                    return left * right;

                case BinaryOperator.Divide:

                    if (right == 0)
                        throw new EvaluationException("Division durch Null.");

                    return left / right;

                default:
                    throw new EvaluationException($"Unbekannter Operator '{binary.Operator}'.");
            }
        }

        #endregion
    }
}
