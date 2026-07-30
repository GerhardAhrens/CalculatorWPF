namespace System.Windows.Calculator
{
    public class BruttoFunction : ICalculatorFunction
    {
        public string Name => "brutto";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("Brutto erwartet genau einen Parameter.");
            }

            if (parameters[0].IsNumber == false)
            {
                throw new EvaluationException("Erster Parameter muss numerisch sein.");
            }

            return parameters[0] * 1.19;
        }
    }
}
