namespace System.Windows.Calculator
{
    using System.Collections.Generic;
    using System.Windows.Documents;

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
            this._functionRegistry.Register(new HeuteFunction());
            this._functionRegistry.Register(new DateDiffFunction());
            this._functionRegistry.Register(new DateFunction());

            var lookupFunction = new LookupFunction
            {
                Provider = new MyLookupProvider()
            };

            this._functionRegistry.Register(lookupFunction);

            _evaluator = new Evaluator(this, _functionRegistry);

            _valueRegistry.Register("MwSt", () => 19);
            _valueRegistry.Register("PI", () => Math.PI);
            _valueRegistry.Register("E", () => Math.E);
            _valueRegistry.Register("M", () => MemoryValue);
        }

        public ILookupProvider LookupProvider { get; set; }

        public bool TryGetValue(string name, out double value)
        {
            return _valueRegistry.TryGetValue(name, out value);
        }

        public CalculatorValue Evaluate(string expression)
        {
            List<Token> tokens = _tokenizer.Tokenize(expression);

            ExpressionNode tree = _parser.Parse(tokens);

            return _evaluator.Evaluate(tree);
        }
    }
}
