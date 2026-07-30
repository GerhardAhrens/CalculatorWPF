namespace System.Windows.Calculator
{
    public class BruttoFunction : ICalculatorFunction
    {
        public string Name => "brutto";

        public int ParameterCount => 1;

        public double Execute(params double[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("Brutto erwartet genau einen Parameter.");
            }

            return parameters[0] * 1.19;
        }
    }
}
