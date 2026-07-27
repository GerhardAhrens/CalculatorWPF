namespace System.Windows.Calculator
{
    using System.Collections.Generic;

    public class CalculatorEngine
    {
        private readonly FunctionRegistry _registry = new();
        private readonly Tokenizer _tokenizer = new();
        private readonly Parser _parser = new();
        private readonly Evaluator _evaluator = new();

        public CalculatorEngine()
        {
            this._registry.Register(new SqrtFunction());
        }

        public double Evaluate(string expression)
        {
            List<Token> tokens = this._tokenizer.Tokenize(expression);

            ExpressionNode tree = this._parser.Parse(tokens);

            return this._evaluator.Evaluate(tree);
        }
    }
}
