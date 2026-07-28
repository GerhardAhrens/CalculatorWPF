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
                throw new ParserException(
                    $"Unerwartetes Token '{Current.Text}'.");

            return expression;
        }

        #endregion

        #region Expression

        private ExpressionNode ParseExpression()
        {
            return ParseAddition();
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

                left = new BinaryExpression(
                    left,
                    op == TokenType.Plus
                        ? BinaryOperator.Add
                        : BinaryOperator.Subtract,
                    right);
            }

            return left;
        }

        #endregion

        #region * /

        private ExpressionNode ParseMultiplication()
        {
            ExpressionNode left = ParseUnary();

            while (Current.Type == TokenType.Multiply ||
                   Current.Type == TokenType.Divide)
            {
                TokenType op = Current.Type;

                Next();

                ExpressionNode right = ParseUnary();

                left = new BinaryExpression(left, op == TokenType.Multiply ? BinaryOperator.Multiply : BinaryOperator.Divide, right);
            }

            return left;
        }

        #endregion

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
            ExpressionNode left = ParsePrimary();

            if (Current.Type == TokenType.Power)
            {
                Next();

                ExpressionNode right = ParseUnary();

                return new BinaryExpression(left, BinaryOperator.Power, right);
            }

            return left;
        }

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
                return ParseFunction();
            }

            throw new ParserException($"Unerwartetes Token '{Current.Text}'.");
        }

        #endregion

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
                throw new ParserException(
                    $"'{tokenType}' erwartet, gefunden '{Current.Text}'.");
            }

            Next();
        }

        #endregion

        private FunctionExpression ParseFunction()
        {
            string functionName = Current.Text;

            Next();

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
