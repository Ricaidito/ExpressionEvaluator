using CompilerFinal.CodeAnalysis;
using System;

namespace CompilerFinal
{
    public class Program
    {
        static void Main()
        {
            /*
             * P3
             * Evaluación final compilador
             * 
             * Alumnos:
             * Ricardo Ramírez
             * Carlos Collado
             * Fernando Reyes
             * Luis Lapaix
             */
            var evaluator = new Evaluator();
            Console.WriteLine("COMPILADOR ONI-CHAN V 1.0");
            Console.WriteLine("A continuación va a entrar en modo de evaluación\n Para salir escriba 'exit'\n");
            evaluator.Evaluate();
        }
    }
}
