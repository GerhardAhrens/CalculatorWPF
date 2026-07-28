namespace System.Windows.Calculator
{
    using System.Collections.Generic;

    public class CalculatorEngine
    {
        public double MemoryValue { get; set; }

        private readonly Tokenizer _tokenizer = new();
        private readonly Parser _parser = new();
        private readonly FunctionRegistry _functionRegistry;
        private readonly VariableRegistry _variableRegistry;
        private readonly Evaluator _evaluator;

        public CalculatorEngine()
        {
            _functionRegistry = new FunctionRegistry();
            _variableRegistry = new VariableRegistry();
            this._functionRegistry.Register(new SqrtFunction());
            _evaluator = new Evaluator(_functionRegistry, _variableRegistry);
        }

        public double Evaluate(string expression)
        {
            List<Token> tokens = this._tokenizer.Tokenize(expression);

            ExpressionNode tree = this._parser.Parse(tokens);

            return this._evaluator.Evaluate(tree);
        }
    }
}
