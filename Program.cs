using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Day14.Run();
        }

        public static List<string> GetInput(int day, bool test)
        {
            string[] textFile;

            if (test == false)
            {
                textFile = File.ReadAllLines($"../../2022/Input/{day}.txt");
            }
            else
            {
                textFile = File.ReadAllLines($"../../2022/Input/{day}_test.txt");
            }

            var input = new List<string>(textFile);

            return input;
        }
    }
}
