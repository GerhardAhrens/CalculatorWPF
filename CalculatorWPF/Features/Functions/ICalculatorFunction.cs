namespace System.Windows.Calculator
{
    public interface ICalculatorFunction
    {
        string Name { get; }

        int ParameterCount { get; }

        // TODO: Parameter anpassen
        /*
        int MinParameterCount { get; }
        int MaxParameterCount { get; }
        */
        CalculatorValue Execute(params CalculatorValue[] parameters);
    }
}
