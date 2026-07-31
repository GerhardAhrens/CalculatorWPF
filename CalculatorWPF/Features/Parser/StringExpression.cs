namespace System.Windows.Calculator
{
    public sealed class StringExpression : ExpressionNode
    {
        public StringExpression(string value)
        {
            this.Value = value;
        }

        public string Value { get; }
    }
}
