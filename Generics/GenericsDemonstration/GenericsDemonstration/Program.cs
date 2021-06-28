using GenericsLib;
using System;
using System.Collections.Generic;

namespace GenericsDemonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1
            Int32[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9};
            Console.WriteLine("BinarySearch (1) : " + BinarySearch.Search(arr, 4));
            Console.WriteLine("BinarySearch (1) : " + BinarySearch.Search(arr, -1));

            // 2
            foreach(var v in FibonaccisNumbers.GetNumbers(50))
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

            // 3.1
            MyStack<String> stack = new MyStack<String>();
            String res;
            bool b = stack.TryPop(out res);
            stack.Push("s1");
            stack.Push("s2");
            stack.Push("s3");
            stack.Push("s4");
            Console.WriteLine("Stack cnt = " + stack.Count());
            foreach (var v in stack)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();
            stack.Pop();
            Console.WriteLine("cnt = " + stack.Count());
            foreach (var v in stack)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();

            // 3.2
            MyQueue<String> queue = new MyQueue<String>();
            queue.TryDequeue(out res);
            queue.Enqueue("s1");
            queue.Enqueue("s2");
            queue.Enqueue("s3");
            queue.Enqueue("s4");
            Console.WriteLine("Queue cnt = " + queue.Count());
            foreach (var v in queue)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();
            queue.Dequeue();
            Console.WriteLine("cnt = " + queue.Count());
            foreach (var v in queue)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();
            queue.Clear();
            Console.WriteLine("cnt = " + queue.Count());
        }
    }
}
