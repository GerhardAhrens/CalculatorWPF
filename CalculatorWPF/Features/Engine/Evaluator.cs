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
            CalculatorValue left = Evaluate(binary.Left);
            CalculatorValue right = Evaluate(binary.Right);

            switch (binary.Operator)
            {
                case BinaryOperator.Add:
                    return Add(left, right);

                case BinaryOperator.Subtract:
                    return Subtract(left, right);

                case BinaryOperator.Multiply:
                    return Multiply(left, right);

                case BinaryOperator.Divide:
                    return Divide(left, right);

                case BinaryOperator.Power:
                    return Power(left, right);

                case BinaryOperator.Equal:
                    return Equal(left, right);

                case BinaryOperator.NotEqual:
                    return CalculatorValue.From(!Equal(left, right).AsBoolean());

                case BinaryOperator.Less:
                    return Less(left, right);

                case BinaryOperator.LessOrEqual:
                    return LessOrEqual(left, right);

                case BinaryOperator.Greater:
                    return Greater(left, right);

                case BinaryOperator.GreaterOrEqual:
                    return GreaterOrEqual(left, right);
                default:
                    throw new EvaluationException($"Unbekannter Operator '{binary.Operator}'.");
            }
        }

        private CalculatorValue Add(CalculatorValue left, CalculatorValue right)
        {
            // Zahl + Zahl
            if (left.IsNumber && right.IsNumber)
                return left.AsNumber() + right.AsNumber();

            // Datum + Tage
            if (left.IsDateTime && right.IsNumber)
                return left.AsDateTime().AddDays(right.AsNumber());

            // Tage + Datum
            if (left.IsNumber && right.IsDateTime)
                return right.AsDateTime().AddDays(left.AsNumber());

            // String + String (optional)
            if (left.IsString && right.IsString)
                return left.AsString() + right.AsString();

            throw new EvaluationException("Operator '+' kann mit diesen Datentypen nicht verwendet werden.");
        }

        private CalculatorValue Subtract(CalculatorValue left, CalculatorValue right)
        {
            // Zahl - Zahl
            if (left.IsNumber && right.IsNumber)
                return left.AsNumber() - right.AsNumber();

            // Datum - Tage
            if (left.IsDateTime && right.IsNumber)
                return left.AsDateTime().AddDays(-right.AsNumber());

            // Datum - Datum = Tage
            if (left.IsDateTime && right.IsDateTime)
                return (left.AsDateTime() - right.AsDateTime()).TotalDays;

            throw new EvaluationException("Operator '-' kann mit diesen Datentypen nicht verwendet werden.");
        }

        private CalculatorValue Multiply(CalculatorValue left, CalculatorValue right)
        {
            if (left.IsNumber && right.IsNumber)
                return left.AsNumber() * right.AsNumber();

            throw new EvaluationException("Operator '*' kann mit diesen Datentypen nicht verwendet werden.");
        }

        private CalculatorValue Divide(CalculatorValue left, CalculatorValue right)
        {
            if (!left.IsNumber || !right.IsNumber)
                throw new EvaluationException("Operator '/' kann nur mit Zahlen verwendet werden.");

            if (right.AsNumber() == 0)
                throw new EvaluationException("Division durch Null.");

            return left.AsNumber() / right.AsNumber();
        }

        private CalculatorValue Power(CalculatorValue left, CalculatorValue right)
        {
            if (!left.IsNumber || !right.IsNumber)
                throw new EvaluationException("Operator '^' kann nur mit Zahlen verwendet werden.");

            return Math.Pow(left.AsNumber(), right.AsNumber());
        }

        private CalculatorValue Equal(CalculatorValue left, CalculatorValue right)
        {
            if (left.Type != right.Type)
                return CalculatorValue.From(false);

            return left.Type switch
            {
                CalculatorValueType.Number => CalculatorValue.From(left.AsNumber() == right.AsNumber()),

                CalculatorValueType.String => CalculatorValue.From(left.AsString() == right.AsString()),

                CalculatorValueType.Boolean => CalculatorValue.From(left.AsBoolean() == right.AsBoolean()),

                CalculatorValueType.DateTime => CalculatorValue.From(left.AsDateTime() == right.AsDateTime()),

                CalculatorValueType.Null => CalculatorValue.From(true),

                _ => CalculatorValue.From(false)
            };
        }

        private CalculatorValue Less(CalculatorValue left, CalculatorValue right)
        {
            if (left.Type != right.Type)
            {
                throw new EvaluationException("Vergleich zwischen unterschiedlichen Datentypen.");
            }

            return left.Type switch
            {
                CalculatorValueType.Number => CalculatorValue.From(left.AsNumber() < right.AsNumber()),

                CalculatorValueType.DateTime => CalculatorValue.From(left.AsDateTime() < right.AsDateTime()),

                _ => throw new EvaluationException($"Operator '<' wird für Typ '{left.Type}' nicht unterstützt.")
            };
        }

        private CalculatorValue Greater(CalculatorValue left, CalculatorValue right)
        {
            if (left.Type != right.Type)
            {
                throw new EvaluationException("Vergleich zwischen unterschiedlichen Datentypen.");
            }

            return left.Type switch
            {
                CalculatorValueType.Number => CalculatorValue.From(left.AsNumber() > right.AsNumber()),

                CalculatorValueType.DateTime => CalculatorValue.From(left.AsDateTime() > right.AsDateTime()),

                _ => throw new EvaluationException($"Operator '>' wird für Typ '{left.Type}' nicht unterstützt.")
            };
        }

        private CalculatorValue LessOrEqual(CalculatorValue left, CalculatorValue right)
        {
            return CalculatorValue.From(Less(left, right).AsBoolean() || Equal(left, right).AsBoolean());
        }

        private CalculatorValue GreaterOrEqual(CalculatorValue left, CalculatorValue right)
        {
            return CalculatorValue.From(Greater(left, right).AsBoolean() || Equal(left, right).AsBoolean());
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
            if (_functionRegistry.TryGetFunction(function.Name, out var calculatorFunction) == false)
            {
                throw new EvaluationException($"Unbekannte Funktion '{function.Name}'.");
            }

            // TODO: Parameter anpassen
            /* Erweiterung der Parameter
            if (function.Parameters.Count < calculatorFunction.MinParameterCount || function.Parameters.Count > calculatorFunction.MaxParameterCount)
            {
            }
            */

            if (calculatorFunction.ParameterCount >= 0 && function.Parameters.Count != calculatorFunction.ParameterCount)
            {
                throw new EvaluationException($"Funktion {function.Name}() erwartet {calculatorFunction.ParameterCount} Parameter.");
            }

            if (function.Name.Equals("If", StringComparison.OrdinalIgnoreCase))
            {
                return EvaluateIf(function);
            }

            CalculatorValue[] values = new CalculatorValue[function.Parameters.Count];

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Evaluate(function.Parameters[i]);
            }

            return calculatorFunction.Execute(values);
        }

        private CalculatorValue EvaluateIf(FunctionExpression function)
        {
            if (function.Parameters.Count != 3)
            {
                throw new EvaluationException("If erwartet drei Parameter.");
            }

            CalculatorValue condition = Evaluate(function.Parameters[0]);

            if (condition.IsBoolean == false)
            {
                throw new EvaluationException("Die Bedingung muss Boolean sein.");
            }

            if (condition.AsBoolean())
            {
                return Evaluate(function.Parameters[1]);
            }

            return Evaluate(function.Parameters[2]);
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
