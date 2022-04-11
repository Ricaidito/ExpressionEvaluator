using System;
using System.Collections.Generic;

namespace CompilerFinal
{
    public class Program
    {
        static void Main()
        {
            List<Token> lt = new();

            while (true)
            {
                Console.Write(">>> ");
                var line = Console.ReadLine();

                if (line == "exit")
                    break;

                if (string.IsNullOrWhiteSpace(line))
                    return;

                var lexer = new AnalizadorLexico(line);

                while (true)
                {
                    var token = lexer.NextToken();
                    lt.Add(token);
                    if (token.Kind == TipoToken.EndOfFileToken)
                    {
                        Console.WriteLine($"{token.Kind}");
                        break;
                    }
                    Console.WriteLine($"{token.Kind}: {token.Text}");
                }

            }

            var analizador = new AnalizadorSemantico(lt);
            Console.WriteLine(analizador.isBoolean());

            //if (lt[0].Kind == TipoToken.NumberToken && lt[2].Kind == TipoToken.PlusToken && lt[4].Kind == TipoToken.NumberToken)
            //{
            //    Console.WriteLine("Es una suma!");
            //}
        }
    }
}
