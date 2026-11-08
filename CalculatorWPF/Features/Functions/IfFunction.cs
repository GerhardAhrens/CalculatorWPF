namespace System.Windows.Calculator
{
    [Formula("Funktion", "If", "(Boolean; Value; Value)", "If(; ; )", "If(1>0;\"Ja\";\"Nein\")", "Prüft eine Bedingung und gibt einen von zwei Werten zurück")]

    public sealed class IfFunction : ICalculatorFunction
    {
        public string Name => "If";

        public int ParameterCount => 3;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (!parameters[0].IsBoolean == true)
            {
                throw new EvaluationException("Der erste Parameter von If muss Boolean sein.");
            }

            return parameters[0].AsBoolean() ? parameters[1] : parameters[2];
        }
    }
}
