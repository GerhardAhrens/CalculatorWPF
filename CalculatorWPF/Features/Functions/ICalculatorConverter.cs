namespace System.Windows.Calculator
{
    public interface ICalculatorConverter : ICalculatorFunction
    {
        CalculatorValue ExecuteBack(params CalculatorValue[] parameters);
    }
}
