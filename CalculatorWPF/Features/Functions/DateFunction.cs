namespace System.Windows.Calculator
{
    public class DateFunction : ICalculatorFunction
    {
        public string Name => "date";

        public int ParameterCount => 3;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != ParameterCount)
            {
                throw new ArgumentException("Date erwartet 3 Parameter.");
            }

            if (parameters[0].IsNumber == false)
            {
                throw new EvaluationException("Erster Parameter muss nummerisch sein.");
            }

            if (parameters[1].IsNumber == false)
            {
                throw new EvaluationException("Zweiter Parameter muss nummerisch sein.");
            }

            if (parameters[2].IsNumber == false)
            {
                throw new EvaluationException("Dritter Parameter muss nummerisch sein.");
            }

            int year = (int)parameters[0].AsNumber();
            int month = (int)parameters[1].AsNumber();
            int day = (int)parameters[2].AsNumber();

            return new DateTime(year, month, day);
        }
    }
}
