namespace System.Windows.Calculator
{
    using System.Collections.Generic;
    using System.Text;

    public class Tokenizer
    {
        private string _expression = string.Empty;
        private int _position;

        public List<Token> Tokenize(string expression)
        {
            _expression = expression ?? string.Empty;
            _position = 0;

            List<Token> tokens = new();

            while (!IsEnd())
            {
                SkipWhiteSpace();

                if (IsEnd())
                    break;

                char current = Current;

                // Zahl
                if (char.IsDigit(current))
                {
                    tokens.Add(ReadNumber());
                    continue;
                }

                // Funktion / Variable
                if (char.IsLetter(current) || current == '_')
                {
                    tokens.Add(ReadIdentifier());
                    continue;
                }

                if (Current == '"')
                {
                    tokens.Add(ReadString());
                    continue;
                }

                switch (current)
                {
                    case '+':
                        tokens.Add(new Token(TokenType.Plus, "+"));
                        _position++;
                        break;

                    case '-':
                        tokens.Add(new Token(TokenType.Minus, "-"));
                        _position++;
                        break;

                    case '*':
                        tokens.Add(new Token(TokenType.Multiply, "*"));
                        _position++;
                        break;

                    case '/':
                        tokens.Add(new Token(TokenType.Divide, "/"));
                        _position++;
                        break;

                    case '^':
                        tokens.Add(new Token(TokenType.Power, "^"));
                        _position++;
                        break;

                    case '%':
                        tokens.Add(new Token(TokenType.Percent, "%"));
                        _position++;
                        break;

                    case '(':
                        tokens.Add(new Token(TokenType.LeftParenthesis, "("));
                        _position++;
                        break;

                    case ')':
                        tokens.Add(new Token(TokenType.RightParenthesis, ")"));
                        _position++;
                        break;

                    case ';':
                    case ',':
                        tokens.Add(new Token(TokenType.Comma, current.ToString()));
                        _position++;
                        break;

                    case '=':
                        tokens.Add(new Token(TokenType.Equal, "="));
                        _position++;
                        break;

                    case '<':

                        _position++;

                        if (!IsEnd())
                        {
                            if (Current == '=')
                            {
                                tokens.Add(new Token(TokenType.LessOrEqual, "<="));
                                _position++;
                                break;
                            }

                            if (Current == '>')
                            {
                                tokens.Add(new Token(TokenType.NotEqual, "<>"));
                                _position++;
                                break;
                            }
                        }

                        tokens.Add(new Token(TokenType.Less, "<"));
                        break;

                    case '>':

                        _position++;

                        if (!IsEnd() && Current == '=')
                        {
                            tokens.Add(new Token(TokenType.GreaterOrEqual, ">="));
                            _position++;
                        }
                        else
                        {
                            tokens.Add(new Token(TokenType.Greater, ">"));
                        }

                        break;

                    default:
                        throw new TokenizerException($"Ungültiges Zeichen '{current}' an Position {_position + 1}.");
                }
            }

            tokens.Add(new Token(TokenType.End, string.Empty));

            return tokens;
        }

        #region Private

        private char Current => _expression[_position];

        private bool IsEnd()
        {
            return _position >= _expression.Length;
        }

        private void SkipWhiteSpace()
        {
            while (!IsEnd() && char.IsWhiteSpace(Current))
            {
                _position++;
            }
        }

        private Token ReadNumber()
        {
            int start = _position;
            bool decimalFound = false;

            while (!IsEnd())
            {
                char c = Current;

                if (char.IsDigit(c))
                {
                    _position++;
                    continue;
                }

                if (c == '.' && !decimalFound)
                {
                    decimalFound = true;
                    _position++;
                    continue;
                }

                break;
            }

            string number = _expression.Substring(start, _position - start);

            return new Token(TokenType.Number, number);
        }

        private Token ReadIdentifier()
        {
            int start = _position;

            while (!IsEnd())
            {
                char c = Current;

                if (char.IsLetterOrDigit(c) || c == '_')
                {
                    _position++;
                    continue;
                }

                break;
            }

            string identifier = _expression.Substring(start, _position - start);

            return new Token(TokenType.Identifier, identifier);
        }

        private Token ReadString()
        {
            // Öffnendes " überspringen
            _position++;

            int start = _position;

            while (!IsEnd() && Current != '"')
            {
                _position++;
            }

            if (IsEnd())
            {
                throw new TokenizerException("Zeichenkette wurde nicht abgeschlossen.");
            }

            string value = _expression.Substring(start, _position - start);

            // Schließendes " überspringen
            _position++;

            return new Token(TokenType.String, value);
        }

        private void Next()
        {
            if (!IsEnd())
            {
                _position++;
            }
        }
        #endregion
    }
}
