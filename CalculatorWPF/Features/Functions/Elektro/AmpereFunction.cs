namespace System.Windows.Calculator
{
    [Formula("Elektro", "Ampere", "(Number;Number)", "Ampere(;)", "Ampere(1150;230)", "Die Funktion berechet aus Leistung (Watt) und Spannung (V) die Stromstärke (A); Ampere(1150;230) → 5A")]

    public sealed class AmpereFunction : ICalculatorFunction
    {
        public string Name => "Ampere";

        public int ParameterCount => 2;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Ampere erwartet genau zwei Parameter.");
            }

            if (!parameters[0].IsNumber)
            {
                throw new EvaluationException("Ampere erwartet einen numerischen Parameter.");
            }

            if (!parameters[1].IsNumber)
            {
                throw new EvaluationException("Ampere erwartet einen numerischen Parameter.");
            }

            double watt = parameters[0].AsNumber();
            double volt = parameters[1].AsNumber();

            if (watt < 0)
            {
                throw new EvaluationException("Die Leistung darf nicht negativ sein.");
            }

            if (volt < 0)
            {
                throw new EvaluationException("Die Spannung darf nicht negativ sein.");
            }

            if (watt == 0)
            {
                throw new EvaluationException("Die Leistung darf nicht null sein.");
            }

            return watt / volt;
        }
    }
}
