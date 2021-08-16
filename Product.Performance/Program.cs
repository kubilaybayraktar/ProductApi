using BenchmarkDotNet.Running;
using System;

namespace Product.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkHarness>();
            Console.ReadKey();
        }
    }
}
