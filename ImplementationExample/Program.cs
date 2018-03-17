using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericMarkovLib;

namespace ImplementationExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = new int[] { 1, 1, 2 };
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                StringBuilder sbOutput = new StringBuilder();
                Network<int> network = new Network<int>(input, rand.Next());
                while (network.HasNext())
                {
                    sbOutput.Append($"{network.Next()} ");
                }
                Console.WriteLine($"> {sbOutput.ToString()}");
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
