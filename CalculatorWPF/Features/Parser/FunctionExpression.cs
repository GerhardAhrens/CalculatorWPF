namespace System.Windows.Calculator
{
    using System.Collections.Generic;

    public class FunctionExpression : ExpressionNode
    {
        public FunctionExpression(string name, List<ExpressionNode> parameters)
        {
            this.Name = name;
            this.Parameters = parameters;
        }

        public string Name { get; }

        public List<ExpressionNode> Parameters { get; }
    }
}
