namespace System.Windows.Calculator
{
    using System.Collections.Generic;

    public class CalculatorEngine
    {
        private readonly Tokenizer _tokenizer = new();
        private readonly Parser _parser = new();
        private readonly Evaluator _evaluator = new();

        public double Evaluate(string expression)
        {
            List<Token> tokens = _tokenizer.Tokenize(expression);

            ExpressionNode tree = _parser.Parse(tokens);

            return _evaluator.Evaluate(tree);
        }
    }
}
