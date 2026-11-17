namespace System.Windows.Calculator
{
    [Formula("Geometrie", "ArToqm", "(Number)", "ArToqm()", "ArToqm(5)", "ArToqm(5) → 5000")]

    public sealed class ArToqmFunction : ICalculatorFunction
    {
        public string Name => "ArToqm";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("ArToqm erwartet genau einen Parameter.");
            }

            if (!parameters[0].IsNumber)
            {
                throw new EvaluationException("ArToqm erwartet einen numerischen Parameter.");
            }

            double ar = parameters[0].AsNumber();

            if (ar < 0)
            {
                throw new EvaluationException("Die Fläche (Ar) darf nicht negativ sein.");
            }

            return ar * 100;
        }
    }
}
