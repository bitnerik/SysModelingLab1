using System;

namespace SystemModelingLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = new Main();
            Console.WriteLine("Processing please wait...");
            main.Start();

            Console.WriteLine("Generating test data sets...");
            MyRandom.RndTest();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
