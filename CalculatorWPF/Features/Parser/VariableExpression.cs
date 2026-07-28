namespace System.Windows.Calculator
{
    public class VariableExpression : ExpressionNode
    {
        public string Name { get; }

        public VariableExpression(string name)
        {
            Name = name;
        }
    }
}
