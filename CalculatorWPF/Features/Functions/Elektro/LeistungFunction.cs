namespace System.Windows.Calculator
{
    [Formula("Elektro", "Watt", "(Number;Number)", "Watt(;)", "Watt(230;5)", "Die Funktion berechet aus Spannung (V) und Stromstärke (A) die Leistung (Watt); Watt(230;5) → 1150W")]

    public sealed class LeistungFunction : ICalculatorFunction
    {
        public string Name => "Watt";

        public int ParameterCount => 2;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Watt erwartet genau zwei Parameter.");
            }

            if (!parameters[0].IsNumber)
            {
                throw new EvaluationException("Volt erwartet einen numerischen Parameter.");
            }

            if (!parameters[1].IsNumber)
            {
                throw new EvaluationException("Ampere erwartet einen numerischen Parameter.");
            }

            double volt = parameters[1].AsNumber();
            double ampere = parameters[0].AsNumber();

            if (volt < 0)
            {
                throw new EvaluationException("Die Spannung darf nicht negativ sein.");
            }

            if (ampere < 0)
            {
                throw new EvaluationException("Die Stromstärke darf nicht negativ sein.");
            }

            return volt * ampere;
        }
    }
}
