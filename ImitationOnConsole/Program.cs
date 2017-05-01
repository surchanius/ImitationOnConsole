using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImitationOnConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            SimpleImitation example = new SimpleImitation(5, 6, 1000, 0.79, 0.80000001,-0.2, 0.8);
            var answer = example.FindStackelbergEquilibrium();
            Console.WriteLine(answer.Item1 + " " + answer.Item2);
            Console.WriteLine(example.fLeaderPayoffFunction(answer.Item1, answer.Item2) + " " + example.gFollowerPayoffFunction(answer.Item1, answer.Item2));

        }
    }
}
