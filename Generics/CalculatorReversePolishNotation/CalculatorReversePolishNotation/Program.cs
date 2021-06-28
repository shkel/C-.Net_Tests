using System;

namespace CalculatorReversePolishNotation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("5 + ((1 + 2) * 4) - 3   =   5 1 2 + 4 * + 3 -   =   ");
            Console.WriteLine(Calculator.Calculate("5 1 2 + 4 * + 3 -"));

            Console.Write("(3 + 4) * (5 - 2)   =   3 4 + 5 2 - *   =   ");
            Console.WriteLine(Calculator.Calculate("3 4 + 5 2 - *"));

        }
    }
}
