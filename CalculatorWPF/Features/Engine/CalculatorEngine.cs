namespace System.Windows.Calculator
{
    using System.Collections.Generic;

    public class CalculatorEngine
    {
        private readonly Tokenizer _tokenizer = new();
        private readonly Parser _parser = new();
        private readonly FunctionRegistry _registry;
        private readonly Evaluator _evaluator;

        public CalculatorEngine()
        {
            this._registry = new FunctionRegistry();
            this._registry.Register(new SqrtFunction());
            this._evaluator = new Evaluator(_registry);
        }

        public double Evaluate(string expression)
        {
            List<Token> tokens = this._tokenizer.Tokenize(expression);

            ExpressionNode tree = this._parser.Parse(tokens);

            return this._evaluator.Evaluate(tree);
        }
    }
}
