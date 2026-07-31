namespace System.Windows.Calculator
{
    public interface ILookupProvider
    {
        CalculatorValue Lookup(string source, IReadOnlyList<CalculatorValue> keys, string field);
    }
}
