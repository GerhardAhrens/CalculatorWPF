namespace System.Windows.Calculator
{
    public sealed class IfFunction : ICalculatorFunction
    {
        public string Name => "If";

        public int ParameterCount => 3;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (!parameters[0].IsBoolean == true)
            {
                throw new EvaluationException("Der erste Parameter von If muss Boolean sein.");
            }

            return parameters[0].AsBoolean() ? parameters[1] : parameters[2];
        }
    }
}
