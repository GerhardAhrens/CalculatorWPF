namespace System.Windows.Calculator
{
    public class NumberExpression : ExpressionNode
    {
        public NumberExpression(double value)
        {
            Value = value;
        }
        public double Value { get; }

    }
}
