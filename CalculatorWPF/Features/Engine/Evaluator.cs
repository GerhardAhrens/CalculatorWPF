namespace System.Windows.Calculator
{
    public class Evaluator
    {
        private readonly CalculatorEngine _engine;
        private readonly FunctionRegistry _functionRegistry;
        private readonly VariableRegistry _variableRegistry;

        public Evaluator(CalculatorEngine engine, FunctionRegistry functionRegistry)
        {
            _engine = engine;
            _functionRegistry = functionRegistry;
        }

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

                case FunctionExpression function:
                    return EvaluateFunction(function);

                case VariableExpression variable:
                    return EvaluateVariable(variable);

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

                case BinaryOperator.Power:
                    return Math.Pow(left, right);

                default:
                    throw new EvaluationException($"Unbekannter Operator '{binary.Operator}'.");
            }
        }

        #endregion

        private double EvaluateFunction(FunctionExpression function)
        {
            if (!_functionRegistry.TryGetFunction(function.Name, out var calculatorFunction))
            {
                throw new EvaluationException($"Unbekannte Funktion '{function.Name}'.");
            }

            if (calculatorFunction.ParameterCount != function.Parameters.Count)
            {
                throw new EvaluationException($"Funktion '{function.Name}' erwartet {calculatorFunction.ParameterCount} Parameter.");
            }

            double[] values = new double[function.Parameters.Count];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Evaluate(function.Parameters[i]);
            }

            return calculatorFunction.Execute(values);
        }

        private double EvaluateVariable(VariableExpression variable)
        {
            if (variable.Name.Equals("M", StringComparison.OrdinalIgnoreCase))
                return _engine.MemoryValue;

            throw new EvaluationException($"Unbekannte Variable '{variable.Name}'.");
        }
    }
}
