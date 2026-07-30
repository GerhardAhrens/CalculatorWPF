namespace System.Windows.Calculator
{
    public class NettoFunction : ICalculatorFunction
    {
        public string Name => "netto";

        public int ParameterCount => 2;

        public double Execute(params double[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Netto erwartet genau zwei Parameter.");
            }

            double brutto = parameters[0];
            double mwst = parameters[1];

            return brutto / (1.0 + mwst / 100.0);
        }
    }
}
