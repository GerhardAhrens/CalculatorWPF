namespace System.Windows.Calculator
{
    public sealed class Ps2KwFunction : ICalculatorFunction
    {
        public string Name => "PS2KW";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("PS2KW erwartet genau einen Parameter.");
            }

            if (parameters[0].IsNumber == false)
            {
                throw new EvaluationException("PS2KW erwartet einen numerischen Parameter.");
            }

            double ps = parameters[0].AsNumber();

            // 1 PS = 0,73549875 kW
            return ps * 0.73549875;
        }
    }
}
