namespace System.Windows.Calculator
{
    public class PercentExpression : ExpressionNode
    {
        public ExpressionNode Operand { get; }

        public PercentExpression(ExpressionNode operand)
        {
            Operand = operand;
        }
    }
}
