using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerFinal
{
    public class AnalizadorSemantico
    {
        List<Token> _tokens = new List<Token>();

        public AnalizadorSemantico(List<Token> tokenList)
        {
            _tokens = tokenList.Where(t => t.Kind != TipoToken.WhitespaceToken && t.Kind != TipoToken.EndOfFileToken).ToList();
        }

        public void EliminateEOF()
        {
            _tokens = _tokens.Where(t => t.Kind != TipoToken.WhitespaceToken && t.Kind != TipoToken.EndOfFileToken).ToList();
        }

        public void PrintTokens() => _tokens.ForEach(t => Console.WriteLine(t.Kind));

        public bool IsMathOperation()
        {
            if (_tokens[0].Kind != TipoToken.NumberToken)
            {
                return false;
            }
            else if (_tokens.Count == 1 && _tokens[0].Kind == TipoToken.NumberToken)
            {
                return true;
            }

            var temp = new TipoToken();

            foreach (var token in _tokens)
            {
                temp = token.Kind;

            }

            return false;
        }

        public bool IsString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            _tokens.ForEach(t => stringBuilder.Append(t.Text));

            return stringBuilder.ToString()[0] == '"' && stringBuilder.ToString()[stringBuilder.Length - 1] == '"';
        }

        private bool IsString(List<Token> lt)
        {
            StringBuilder stringBuilder = new StringBuilder();
            lt.ForEach(t => stringBuilder.Append(t.Text));

            return stringBuilder.ToString()[0] == '"' && stringBuilder.ToString()[stringBuilder.Length - 1] == '"';
        }

        public bool IsVariableDeclaration()
        {
            if (_tokens.Count > 3 && _tokens[0].Text == "var" && _tokens[1].Kind == TipoToken.LetterToken && _tokens[2].Text == "=")
            {
                List<Token> tokens = new List<Token>();
                for (int i = 3; i < _tokens.Count; i++)
                {
                    tokens.Add(_tokens[i]);
                }

                return IsString(tokens);
            }
            return false;
        }
    }
}
