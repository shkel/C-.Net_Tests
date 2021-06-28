
using System.Collections.Generic;
using System.Numerics;

namespace GenericsLib
{
    public class FibonaccisNumbers
    {
        public static IEnumerable<BigInteger> GetNumbers(int maxNumbersCount)
        {
            BigInteger res = 0;
            BigInteger sum = 0;
            for (int i = 0; i < maxNumbersCount; i++)
            {
                if (i < 2)
                {
                    res = i;
                }
                else
                {
                    BigInteger prevSum = res;
                    res = checked(res + sum);
                    sum = prevSum;
                }
                yield return res;
            }
        }
    }
}
