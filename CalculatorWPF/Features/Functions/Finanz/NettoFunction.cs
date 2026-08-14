namespace System.Windows.Calculator
{
    public class NettoFunction : ICalculatorFunction
    {
        public string Name => "netto";

        public int ParameterCount => 2;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Netto erwartet genau zwei Parameter.");
            }

            if (parameters[0].IsNumber == false || parameters[1].IsNumber == false)
            {
                throw new EvaluationException("Beide Parameter müssen numerisch sein.");
            }

            double brutto = parameters[0];
            double mwst = parameters[1];

            return brutto / (1.0 + mwst / 100.0);
        }
    }
}
