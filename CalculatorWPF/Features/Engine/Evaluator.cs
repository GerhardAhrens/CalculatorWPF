namespace System.Windows.Calculator
{
    public class Evaluator
    {
        private readonly CalculatorEngine _engine;
        private readonly FunctionRegistry _functionRegistry;

        public Evaluator(CalculatorEngine engine, FunctionRegistry functionRegistry)
        {
            _engine = engine;
            _functionRegistry = functionRegistry;
        }

        public CalculatorValue Evaluate(ExpressionNode node)
        {
            return node switch
            {
                NumberExpression n => EvaluateNumber(n),
                VariableExpression v => EvaluateVariable(v),
                UnaryExpression u => EvaluateUnary(u),
                BinaryExpression b => EvaluateBinary(b),
                FunctionExpression f => EvaluateFunction(f),
                StringExpression s => EvaluateString(s),
                _ => throw new EvaluationException("Unbekannter Ausdruck.")
            };
        }

        #region Number

        private CalculatorValue EvaluateNumber(NumberExpression expression)
        {
            return expression.Value;
        }

        #endregion

        #region Unary

        private CalculatorValue EvaluateUnary(UnaryExpression unary)
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

        private CalculatorValue EvaluateBinary(BinaryExpression binary)
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

                case BinaryOperator.Power:
                    return Math.Pow(left, right);

                default:
                    throw new EvaluationException($"Unbekannter Operator '{binary.Operator}'.");
            }
        }

        #endregion

        #region String
        private CalculatorValue EvaluateString(StringExpression expression)
        {
            return expression.Value;
        }
        #endregion String

        #region Function
        private CalculatorValue EvaluateFunction(FunctionExpression function)
        {
            if (!_functionRegistry.TryGetFunction(function.Name, out var calculatorFunction))
            {
                throw new EvaluationException($"Unbekannte Funktion '{function.Name}'.");
            }

            if (calculatorFunction.ParameterCount != function.Parameters.Count)
            {
                throw new EvaluationException($"Funktion '{function.Name}' erwartet {calculatorFunction.ParameterCount} Parameter.");
            }

            CalculatorValue[] values = new CalculatorValue[function.Parameters.Count];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Evaluate(function.Parameters[i]);
            }

            return calculatorFunction.Execute(values);
        }
        #endregion Function

        private CalculatorValue EvaluateVariable(VariableExpression variable)
        {
            if (_engine.TryGetValue(variable.Name, out double value))
            {
                return value;
            }

            throw new EvaluationException($"Unbekannte Variable '{variable.Name}'.");
        }
    }
}
