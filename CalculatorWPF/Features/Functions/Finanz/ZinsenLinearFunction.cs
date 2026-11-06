namespace System.Windows.Calculator
{
    [Formula("Finanz", "Zinsen", "(Number; Number; Number)", "Zinsen(;;)", "Zinsen(1000; 5; 12)  → 50", "Gibt die linearen Zinsen für einen bestimmten Betrag(Euro), Zinssatz(Prozent) und Zeitraum(Monate) zurück")]
    public class ZinsenLinearFunction : ICalculatorFunction
    {
        public string Name => "zinsen";

        public int ParameterCount => 3;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 3)
            {
                throw new ArgumentException("Zinsen erwartet genau drei Parameter.");
            }

            if (parameters[0].IsNumber == false || parameters[1].IsNumber == false || parameters[2].IsNumber == false)
            {
                throw new EvaluationException("Alle Parameter müssen numerisch sein.");
            }

            double betrag = parameters[0];
            double zins = parameters[1];
            double monate = parameters[2];

            return betrag * (zins/100) * (monate/12);
        }
    }
}
