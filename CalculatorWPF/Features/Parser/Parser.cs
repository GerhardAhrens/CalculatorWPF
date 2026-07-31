namespace System.Windows.Calculator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class Parser
    {
        private List<Token> _tokens = new();
        private int _position;

        #region Public

        public ExpressionNode Parse(List<Token> tokens)
        {
            _tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            _position = 0;

            ExpressionNode expression = ParseExpression();

            if (Current.Type != TokenType.End)
            {
                throw new ParserException($"Unerwartetes Token '{Current.Text}'.");
            }

            return expression;
        }

        #endregion

        #region Expression

        private ExpressionNode ParseExpression()
        {
            return ParseComparison();
        }

        #endregion

        #region + -

        private ExpressionNode ParseAddition()
        {
            ExpressionNode left = ParseMultiplication();

            while (Current.Type == TokenType.Plus ||
                   Current.Type == TokenType.Minus)
            {
                TokenType op = Current.Type;

                Next();

                ExpressionNode right = ParseMultiplication();

                left = new BinaryExpression(left, op == TokenType.Plus ? BinaryOperator.Add : BinaryOperator.Subtract, right);
            }

            return left;
        }

        #endregion

        #region

        private ExpressionNode ParseMultiplication()
        {
            ExpressionNode left = ParseUnary();

            while (Current.Type == TokenType.Multiply || Current.Type == TokenType.Divide)
            {
                TokenType op = Current.Type;

                Next();

                ExpressionNode right = ParseUnary();

                left = new BinaryExpression(left, op == TokenType.Multiply ? BinaryOperator.Multiply : BinaryOperator.Divide, right);
            }

            return left;
        }

        #endregion

        #region Primary

        private ExpressionNode ParsePrimary()
        {
            if (Current.Type == TokenType.Number)
            {
                double value = double.Parse(Current.Text, CultureInfo.InvariantCulture);

                Next();

                return new NumberExpression(value);
            }

            if (Current.Type == TokenType.LeftParenthesis)
            {
                Next();

                ExpressionNode expression = ParseExpression();

                Expect(TokenType.RightParenthesis);

                return expression;
            }

            if (Current.Type == TokenType.Identifier)
            {
                string identifier = Current.Text;

                Next();

                if (Current.Type == TokenType.LeftParenthesis)
                {
                    return ParseFunction(identifier);
                }

                return new VariableExpression(identifier);
            }

            if (Current.Type == TokenType.String)
            {
                string value = Current.Text;
                Next();
                return new StringExpression(value);
            }

            throw new ParserException($"Unerwartetes Token '{Current.Text}'.");
        }

        #endregion

        #region ParseComparison
        private ExpressionNode ParseComparison()
        {
            ExpressionNode left = ParseAddition();

            while (Current.Type == TokenType.Equal ||
                   Current.Type == TokenType.NotEqual ||
                   Current.Type == TokenType.Less ||
                   Current.Type == TokenType.LessOrEqual ||
                   Current.Type == TokenType.Greater ||
                   Current.Type == TokenType.GreaterOrEqual)
            {
                TokenType op = Current.Type;

                Next();

                ExpressionNode right = ParseAddition();

                BinaryOperator binaryOperator = op switch
                {
                    TokenType.Equal => BinaryOperator.Equal,
                    TokenType.NotEqual => BinaryOperator.NotEqual,
                    TokenType.Less => BinaryOperator.Less,
                    TokenType.LessOrEqual => BinaryOperator.LessOrEqual,
                    TokenType.Greater => BinaryOperator.Greater,
                    TokenType.GreaterOrEqual => BinaryOperator.GreaterOrEqual,
                    _ => throw new InvalidOperationException()
                };

                left = new BinaryExpression(left, binaryOperator, right);
            }

            return left;
        }
        #endregion ParseComparison

        #region Unary

        private ExpressionNode ParseUnary()
        {
            if (Current.Type == TokenType.Plus)
            {
                Next();

                return new UnaryExpression(TokenType.Plus, ParseUnary());
            }

            if (Current.Type == TokenType.Minus)
            {
                Next();

                return new UnaryExpression(TokenType.Minus, ParseUnary());
            }

            return ParsePower();
        }

        #endregion

        private ExpressionNode ParsePower()
        {
            ExpressionNode left = ParsePercent();

            if (Current.Type == TokenType.Power)
            {
                Next();

                ExpressionNode right = ParseUnary();

                return new BinaryExpression(left, BinaryOperator.Power, right);
            }

            return left;
        }

        private ExpressionNode ParsePercent()
        {
            ExpressionNode node = ParsePrimary();

            while (Current.Type == TokenType.Percent)
            {
                Next();

                node = new BinaryExpression(node, BinaryOperator.Divide, new NumberExpression(100));
            }

            return node;
        }

        #region Helper

        private Token Current => _tokens[_position];

        private void Next()
        {
            if (_position < _tokens.Count - 1)
                _position++;
        }

        private void Expect(TokenType tokenType)
        {
            if (Current.Type != tokenType)
            {
                throw new ParserException($"'{tokenType}' erwartet, gefunden '{Current.Text}'.");
            }

            Next();
        }

        private VariableExpression ParseVariable()
        {
            string name = Current.Text;

            Next();

            return new VariableExpression(name);
        }
        #endregion

        private FunctionExpression ParseFunction(string functionName)
        {
            Expect(TokenType.LeftParenthesis);

            List<ExpressionNode> parameters = new();

            // mindestens ein Parameter
            if (Current.Type != TokenType.RightParenthesis)
            {
                parameters.Add(ParseExpression());

                while (Current.Type == TokenType.Comma)
                {
                    Next();

                    parameters.Add(ParseExpression());
                }
            }

            Expect(TokenType.RightParenthesis);

            return new FunctionExpression(functionName, parameters);
        }
    }
}
