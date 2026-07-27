namespace System.Windows.Calculator
{
    public class UnaryExpression : ExpressionNode
    {
        public UnaryExpression(TokenType op, ExpressionNode operand)
        {
            Operator = op;
            Operand = operand;
        }

        public TokenType Operator { get; }

        public ExpressionNode Operand { get; }
    }
}
