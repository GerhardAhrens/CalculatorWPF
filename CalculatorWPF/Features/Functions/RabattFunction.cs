namespace System.Windows.Calculator
{
    public class RabattFunction : ICalculatorFunction
    {
        public string Name => "rabatt";

        public int ParameterCount => 2;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new ArgumentException("Rabatt erwartet genau zwei Parameter.");
            }

            if (parameters[0].IsNumber == false || parameters[1].IsNumber == false)
            {
                throw new EvaluationException("Beide Parameter müssen numerisch sein.");
            }

            double betrag = parameters[0];
            double rabatt = parameters[1];

            return betrag * (1.0 - rabatt / 100.0);
        }
    }
}
