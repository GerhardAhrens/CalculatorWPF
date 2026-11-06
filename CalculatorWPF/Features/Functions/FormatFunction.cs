namespace System.Windows.Calculator
{
    [Formula("Funktion", "Format", "(Value; String)", "Format(;)", "Format(1234.567;\"N2\") → 1.234,57 | Format(Heute();\"dd.MM.yyyy\") → 31.07.2026 | Format(Lookup(\"Artikel\";2001;\"C\");\"0.00 €\")","Formatiert einen Wert gemäß dem angegebenen Format")]

    public sealed class FormatFunction : ICalculatorFunction
    {
        public string Name => "Format";

        public int ParameterCount => 2;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new EvaluationException("Format erwartet zwei Parameter.");
            }

            CalculatorValue value = parameters[0];
            string format = parameters[1].AsString();

            return CalculatorValue.From(value.ToDisplayString(format));
        }
    }
}
