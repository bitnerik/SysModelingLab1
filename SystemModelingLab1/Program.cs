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

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
