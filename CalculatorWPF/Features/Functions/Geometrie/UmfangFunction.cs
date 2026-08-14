namespace System.Windows.Calculator
{

    public sealed class UmfangFunction : ICalculatorFunction
    {
        public string Name => "Umfang";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("Umfang erwartet genau einen Parameter.");
            }

            if (!parameters[0].IsNumber)
            {
                throw new EvaluationException("Umfang erwartet einen numerischen Parameter.");
            }

            double radius = parameters[0].AsNumber();

            if (radius < 0)
            {
                throw new EvaluationException("Der Radius darf nicht negativ sein.");
            }

            return 2.0 * Math.PI * radius;
        }
    }
}
