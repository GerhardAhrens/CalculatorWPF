namespace System.Windows.Calculator
{
    using System.Collections.Generic;

    public class CalculatorEngine
    {
        public double MemoryValue { get; set; }

        private readonly Tokenizer _tokenizer = new();
        private readonly Parser _parser = new();
        private readonly ValueRegistry _valueRegistry = new();
        private readonly FunctionRegistry _functionRegistry;
        private readonly Evaluator _evaluator;

        public CalculatorEngine()
        {
            _functionRegistry = new FunctionRegistry();
            this._functionRegistry.Register(new SqrtFunction());
            this._functionRegistry.Register(new BruttoFunction());
            this._functionRegistry.Register(new NettoFunction());
            this._functionRegistry.Register(new RabattFunction());
            _evaluator = new Evaluator(this, _functionRegistry);

            _valueRegistry.Register("MwSt", () => 19);
            _valueRegistry.Register("PI", () => Math.PI);
            _valueRegistry.Register("E", () => Math.E);
            _valueRegistry.Register("M", () => MemoryValue);
        }

        public bool TryGetValue(string name, out double value)
        {
            return _valueRegistry.TryGetValue(name, out value);
        }

        public double Evaluate(string expression)
        {
            List<Token> tokens = this._tokenizer.Tokenize(expression);

            ExpressionNode tree = this._parser.Parse(tokens);

            return this._evaluator.Evaluate(tree);
        }
    }
}
