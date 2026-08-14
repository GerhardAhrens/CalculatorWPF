namespace System.Windows.Calculator
{
    public class HeuteFunction : ICalculatorFunction
    {
        public string Name => "heute";

        public int ParameterCount => 0;

        public CalculatorValue Execute(params CalculatorValue[] parameters)
        {
            if (parameters.Length != ParameterCount)
            {
                throw new ArgumentException("Heute erwartet keine Parameter.");
            }

            return DateTime.Today;
        }
    }
}
