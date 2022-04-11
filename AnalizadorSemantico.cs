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
            if (_tokens.First().Kind != TipoToken.NumberToken || _tokens.Last().Kind != TipoToken.NumberToken)
            {
                return false;
            }

            else if (_tokens.Count == 1 && _tokens[0].Kind == TipoToken.NumberToken)
            {
                return true;
            }

            int num = 0;
            int oper = 0;

            for (int i = 0; i < _tokens.Count; i++)
            {
                if(_tokens[i].Kind == TipoToken.NumberToken)
                {
                    num++;

                    if (i == _tokens.Count - 1)
                    {
                        break;
                    }

                    if (_tokens[i + 1].Kind == TipoToken.PlusToken || _tokens[i + 1].Kind == TipoToken.MinusToken || _tokens[i + 1].Kind == TipoToken.StarToken || _tokens[i + 1].Kind == TipoToken.SlashToken)
                    {
                        continue;
                    }

                    else
                    {
                        break;
                    }  
                        
                }

                else if(_tokens[i].Kind == TipoToken.PlusToken || _tokens[i].Kind == TipoToken.MinusToken || _tokens[i].Kind == TipoToken.StarToken || _tokens[i].Kind == TipoToken.SlashToken)
                {
                    oper++;

                    if (_tokens[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        break;
                    }

                }

            }
            
            if(num == oper + 1)
            {
                return true;
            }

            return false;
        }

        public bool IsMathOperation(List<Token> lt)
        {
            if (lt.First().Kind != TipoToken.NumberToken || lt.Last().Kind != TipoToken.NumberToken)
            {
                return false;
            }

            else if (lt.Count == 1 && lt[0].Kind == TipoToken.NumberToken)
            {
                return true;
            }

            int num = 0;
            int oper = 0;

            for (int i = 0; i < lt.Count; i++)
            {
                if (lt[i].Kind == TipoToken.NumberToken)
                {
                    num++;

                    if (i == _tokens.Count - 1)
                    {
                        break;
                    }

                    if (lt[i + 1].Kind == TipoToken.PlusToken || lt[i + 1].Kind == TipoToken.MinusToken || lt[i + 1].Kind == TipoToken.StarToken || lt[i + 1].Kind == TipoToken.SlashToken)
                    {
                        continue;
                    }

                    else
                    {
                        break;
                    }

                }

                else if (lt[i].Kind == TipoToken.PlusToken || lt[i].Kind == TipoToken.MinusToken || lt[i].Kind == TipoToken.StarToken || lt[i].Kind == TipoToken.SlashToken)
                {
                    oper++;

                    if (lt[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        break;
                    }

                }

            }

            if (num == oper + 1)
            {
                return true;
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

        public bool isBoolean()
        {
            if (_tokens.First().Kind != TipoToken.NumberToken || _tokens.Last().Kind != TipoToken.NumberToken)
            {
                return false;
            }

            int equalToken = 0;

            for (int i = 0; i < _tokens.Count; i++)
            {
                if(_tokens[i].Kind == TipoToken.NumberToken)
                {
                    if (i == _tokens.Count - 1)
                    {
                        break;
                    }

                    if (_tokens[i + 1].Kind == TipoToken.EqualToken || _tokens[i + 1].Kind == TipoToken.LessToken || _tokens[i + 1].Kind == TipoToken.GreaterToken || _tokens[i + 1].Kind == TipoToken.AmpersandToken || _tokens[i + 1].Kind == TipoToken.BarrierToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }

                }

                else if (_tokens[i].Kind == TipoToken.AmpersandToken)
                {
                    if(_tokens[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if(_tokens[i].Kind == TipoToken.BarrierToken)
                {
                    if (_tokens[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if(_tokens[i].Kind == TipoToken.GreaterToken)
                {
                    if(_tokens[i + 1].Kind  == TipoToken.EqualToken)
                    {
                        equalToken++;
                        continue;
                    }

                    else if (_tokens[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if (_tokens[i].Kind == TipoToken.LessToken)
                {
                    if (_tokens[i + 1].Kind == TipoToken.EqualToken)
                    {
                        equalToken++;
                        continue;
                    }

                    else if (_tokens[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if(_tokens[i].Kind == TipoToken.EqualToken)
                {
                    if(_tokens[i + 1].Kind == TipoToken.NumberToken && equalToken == 1)
                    {
                        equalToken = 0;
                        continue;
                    }

                    else if(_tokens[i + 1].Kind == TipoToken.EqualToken)
                    {
                        equalToken++;
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public bool isBoolean(List<Token> lt)
        {
            if (lt.First().Kind != TipoToken.NumberToken || lt.Last().Kind != TipoToken.NumberToken)
            {
                return false;
            }

            int equalToken = 0;

            for (int i = 0; i < lt.Count; i++)
            {
                if (lt[i].Kind == TipoToken.NumberToken)
                {
                    if (i == lt.Count - 1)
                    {
                        break;
                    }

                    if (lt[i + 1].Kind == TipoToken.EqualToken || lt[i + 1].Kind == TipoToken.LessToken || lt[i + 1].Kind == TipoToken.GreaterToken || lt[i + 1].Kind == TipoToken.AmpersandToken || lt[i + 1].Kind == TipoToken.BarrierToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }

                }

                else if (lt[i].Kind == TipoToken.AmpersandToken)
                {
                    if (lt[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if (lt[i].Kind == TipoToken.BarrierToken)
                {
                    if (lt[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if (lt[i].Kind == TipoToken.GreaterToken)
                {
                    if (lt[i + 1].Kind == TipoToken.EqualToken)
                    {
                        equalToken++;
                        continue;
                    }

                    else if (lt[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if (lt[i].Kind == TipoToken.LessToken)
                {
                    if (lt[i + 1].Kind == TipoToken.EqualToken)
                    {
                        equalToken++;
                        continue;
                    }

                    else if (lt[i + 1].Kind == TipoToken.NumberToken)
                    {
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

                else if (lt[i].Kind == TipoToken.EqualToken)
                {
                    if (lt[i + 1].Kind == TipoToken.NumberToken && equalToken == 1)
                    {
                        equalToken = 0;
                        continue;
                    }

                    else if (lt[i + 1].Kind == TipoToken.EqualToken)
                    {
                        equalToken++;
                        continue;
                    }

                    else
                    {
                        return false;
                    }
                }

            }

            return true;
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

                return IsString(tokens) || IsMathOperation(tokens);
            }
            return false;
        }
    }
}
