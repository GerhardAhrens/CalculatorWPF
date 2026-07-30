namespace System.Windows.Calculator
{
    public class RabattFunction : ICalculatorFunction
    {
        public string Name => "rabatt";

        public int ParameterCount => 2;

        public double Execute(params double[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Rabatt erwartet genau zwei Parameter.");
            }

            double betrag = parameters[0];
            double rabatt = parameters[1];

            return betrag * (1.0 - rabatt / 100.0);
        }
    }
}
