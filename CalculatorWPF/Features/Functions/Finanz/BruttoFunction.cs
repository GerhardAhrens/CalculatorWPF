namespace System.Windows.Calculator
{
    [Formula("Finanz", "Brutto", "(Number, Number)", "Brutto()", "Brutto(100;mwst) → 119", "Berechnet den Bruttobetrag aus Netto-Betrag und Mehrwertsteuer")]
    public class BruttoFunction : ICalculatorFunction
    {
        public string Name => "brutto";

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

            double betrag = parameters[0];
            double mwst = parameters[1];

            return betrag * ((mwst / 100) + 1);
        }
    }
}
