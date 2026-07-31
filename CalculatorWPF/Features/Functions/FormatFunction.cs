namespace System.Windows.Calculator
{
    public sealed class FormatFunction : ICalculatorFunction
    {
        public string Name => "Format";

        public int ParameterCount => 2;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new EvaluationException("Format erwartet zwei Parameter.");
            }

            CalculatorValue value = parameters[0];
            string format = parameters[1].AsString();

            return CalculatorValue.From(value.ToDisplayString(format));
        }
    }
}
