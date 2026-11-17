namespace System.Windows.Calculator
{
    [Formula("Geometrie", "QmToar", "(Number)", "QmToar()", "QmToar(5)", "QmToar(5) → 0,05")]

    public sealed class QmToarFunction : ICalculatorFunction
    {
        public string Name => "QmToar";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("QmToar erwartet genau einen Parameter.");
            }

            if (!parameters[0].IsNumber)
            {
                throw new EvaluationException("QmToar erwartet einen numerischen Parameter.");
            }

            double qm = parameters[0].AsNumber();

            if (qm < 0)
            {
                throw new EvaluationException("Die Fläche (Qm) darf nicht negativ sein.");
            }

            return qm / 100;
        }
    }
}
