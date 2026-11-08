namespace System.Windows.Calculator
{
    [Formula("Datum", "TagImJahr", "(DateTime)", "TagImJahr(Date(;;))", "TagImJahr(Date(2026; 11; 8))  → Sonntag", "Gibt den Wochentag zu einem bestimmten Datum zurück")]
    public class TagImJahr : ICalculatorFunction
    {
        public string Name => "TagImJahr";

        public int ParameterCount => 1;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new ArgumentException("TagImJahr erwartet genau einen Parameter.");
            }

            if (parameters[0].IsDateTime == false)
            {
                throw new EvaluationException("Der Parameter muß ein Datum sein.");
            }

            DateTime datum = parameters[0];

            return datum.ToString("dddd", new System.Globalization.CultureInfo("de-DE"));
        }
    }
}
