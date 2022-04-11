namespace CompilerFinal.CodeAnalysis
{
    public class AnalizadorLexico
    {
        private readonly string _text;
        private int _position;

        public AnalizadorLexico(string text)
        {
            _text = text;
        }

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }

        public Token NextToken()
        {

            if (_position >= _text.Length)
            {
                return new Token(TipoToken.EndOfFileToken, _position, "\0", null);
            }

            #region IsAWhiteSpace
            if (char.IsWhiteSpace(Current))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new Token(TipoToken.WhitespaceToken, start, text, null);

            }
            #endregion

            #region CheckForNumbers
            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new Token(TipoToken.NumberToken, start, text, value);
            }
            #endregion

            #region IsALetter
            if (char.IsLetter(Current))
            {
                var start = _position;

                while (char.IsLetter(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new Token(TipoToken.LetterToken, start, text, value);
            }
            #endregion

            #region Operators
            switch (Current)
            {
                case '+':
                    return new Token(TipoToken.PlusToken, _position++, "+", null);
                case '-':
                    return new Token(TipoToken.MinusToken, _position++, "-", null);
                case '*':
                    return new Token(TipoToken.StarToken, _position++, "*", null);
                case '/':
                    return new Token(TipoToken.SlashToken, _position++, "/", null);
                case '=':
                    return new Token(TipoToken.EqualToken, _position++, "=", null);
                case '<':
                    return new Token(TipoToken.LessToken, _position++, "<", null);
                case '>':
                    return new Token(TipoToken.GreaterToken, _position++, ">", null);
                case '&':
                    return new Token(TipoToken.AmpersandToken, _position++, "&", null);
                case '|':
                    return new Token(TipoToken.BarrierToken, _position++, "|", null);
                default:
                    break;
            }
            #endregion

            #region SpecialChars
            switch (Current)
            {
                case '"':
                    return new Token(TipoToken.DoubleQuoteToken, _position++, "\"", null);
                default:
                    break;
            }
            #endregion

            return new Token(TipoToken.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }
    }
}
