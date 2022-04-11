using System;
using System.Collections.Generic;

namespace CompilerFinal.CodeAnalysis
{
    public class Evaluator
    {
        public void Evaluate()
        {
            while (true)
            {
                var listTokens = new List<Token>();
                Console.Write(">> ");
                var line = Console.ReadLine();

                if (line == "exit")
                    break;
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var lexer = new AnalizadorLexico(line);

                Console.WriteLine("\nTOKENS:");
                while (true)
                {
                    var token = lexer.NextToken();
                    listTokens.Add(token);
                    if (token.Kind == TipoToken.EndOfFileToken)
                        break;
                    Console.WriteLine($"<{token.Kind}> {token.Text}");
                }
                var semantic = new AnalizadorSemantico(listTokens);
                semantic.Evaluate().PrintExpression();
            }
        }
    }
}
