namespace System.Windows.Calculator
{
    using System.Globalization;

    public class Token
    {
        public TokenType Type { get; }

        public string Text { get; }

        public double? Number { get; }

        public Token(TokenType type, string text)
        {
            Type = type;
            Text = text;

            if (type == TokenType.Number)
            {
                Number = double.Parse(text, CultureInfo.InvariantCulture);
            }
        }
    }
}
