namespace System.Windows.Calculator
{
    public sealed class ConstantDefinition : FunctionDefinition
    {
        public double Value { get; }

        public ConstantDefinition(string name, double value) : base(name)
        {
            Value = value;
        }
    }
}
