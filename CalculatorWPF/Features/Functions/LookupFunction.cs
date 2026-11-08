namespace System.Windows.Calculator
{
    [Formula("Funktion", "Lookup", "(String; Key; String)", "Lookup(\"\"; ; \"\")", "Lookup(\"Artikel\";2001;\"B\") → Kugelschreiber oder Lookup(\"Artikel\";2001;\"C\") → 1,99", "Sucht nach einem Wert in einer Tabelle und gibt den entsprechenden Eintrag zurück")]
    public sealed class LookupFunction : ICalculatorFunction
    {
        public string Name => "Lookup";

        // Variable Anzahl Parameter
        public int ParameterCount => -1;

        public ILookupProvider Provider { get; set; }

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (Provider == null)
            {
                throw new EvaluationException("Es wurde kein LookupProvider zugewiesen.");
            }

            if (parameters.Length < 3)
            {
                throw new EvaluationException("Lookup erwartet mindestens drei Parameter.");
            }

            if (parameters[0].IsString == false)
                throw new EvaluationException("Der erste Parameter von Lookup muss eine Zeichenkette sein.");

            if (parameters[^1].IsString == false)
            {
                throw new EvaluationException("Der letzte Parameter von Lookup muss eine Zeichenkette sein.");
            }

            string source = parameters[0].AsString();
            string field = parameters[^1].AsString();

            CalculatorValue[] keys = parameters
                .Skip(1)
                .Take(parameters.Length - 2)
                .ToArray();

            return Provider.Lookup(source, keys, field);
        }
    }
}
