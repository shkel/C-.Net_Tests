using System;

namespace CalculatorReversePolishNotation
{
    internal class CalculatorNumber : Lexeme
    {
        private CalculatorNumber(int num)
        {
            Num = num;
        }

        internal Int32 Num { private set; get; }

        internal static CalculatorNumber TryParse(string s)
        {
            int num;
            return Int32.TryParse(s, out num) ? new CalculatorNumber(num) : null;
        }
    }
}
