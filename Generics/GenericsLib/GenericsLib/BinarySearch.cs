using System;

namespace GenericsLib
{
    public class BinarySearch
    {
        public static int Search<T>(T[] sortedElements, T value)
            where T : IComparable
        {
            if (sortedElements == null)
            {
                throw new ArgumentNullException("Sorted elements is Null");
            }
            if (value == null)
            {
                throw new ArgumentNullException("Value is Null");
            }

            int startIndex = 0;
            int endIndex = sortedElements.Length - 1;
            int middleIndex;
            while (startIndex <= endIndex)
            {
                middleIndex = (startIndex + endIndex) / 2;
                if (sortedElements[middleIndex] == null)
                {
                    throw new ArgumentNullException("Element is Null");
                }
                int compareResult = sortedElements[middleIndex].CompareTo(value);
                if (compareResult < 0)
                {
                    startIndex = middleIndex + 1;
                }
                else if (compareResult > 0)
                {
                    endIndex = middleIndex - 1;
                }
                else
                {
                    return middleIndex;
                }
            }
            return -1;
        }
    }
}
