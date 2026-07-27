namespace System.Windows.Calculator
{
    public interface ICalculatorFunction
    {
        string Name { get; }

        int ParameterCount { get; }

        double Execute(params double[] parameters);
    }
}
