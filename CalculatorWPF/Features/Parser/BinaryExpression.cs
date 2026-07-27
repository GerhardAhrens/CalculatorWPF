namespace System.Windows.Calculator
{
    public class BinaryExpression : ExpressionNode
    {
        public BinaryExpression(ExpressionNode left, BinaryOperator op, ExpressionNode right)
        {
            Left = left;
            Operator = op;
            Right = right;
        }

        public ExpressionNode Left { get; }

        public ExpressionNode Right { get; }

        public BinaryOperator Operator { get; }
    }
}
