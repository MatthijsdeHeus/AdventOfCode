using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day10
    {
        public static void Run()
        {
            List<string> input = Program.GetInput(10, false);

            Part1(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            int RegisterX = 1;

            int iteration = 1;

            int sum = 0;

            foreach (string line in input)
            {
                var split = line.Split(' ');

                if (split[0] == "noop")
                {
                    iteration++;
                    if (iteration == 20 || iteration == 60 || iteration == 100 || iteration == 140 || iteration == 180 || iteration == 220)
                    {
                        /*Console.WriteLine(iteration);
                        Console.WriteLine(RegisterX);*/
                        sum += iteration * RegisterX;
                    }

                }
                else if (split[0] == "addx")
                {
                    iteration += 1;
                    if (iteration == 20 || iteration == 60 || iteration == 100 || iteration == 140 || iteration == 180 || iteration == 220)
                    {
                        //Console.WriteLine(iteration);
                        //Console.WriteLine(RegisterX);
                        sum += iteration * RegisterX;
                    }
                    iteration++;

                    RegisterX += int.Parse(split[1]);

                    if (iteration == 20 || iteration == 60 || iteration == 100 || iteration == 140 || iteration == 180 || iteration == 220)
                    {
                        //Console.WriteLine(iteration);
                        //Console.WriteLine(RegisterX);
                        sum += iteration * RegisterX;
                    }
                }
            }

            Console.WriteLine(sum);
        }
        public static void Part2(List<string> input)
        {
            int RegisterX = 1;

            int iteration = 1;

            string drawing = "";

            drawing = Draw(RegisterX, iteration, drawing);

            foreach (string line in input)
            {
                var split = line.Split(' ');

                if (split[0] == "noop")
                {
                    iteration++;
                    drawing = Draw(RegisterX, iteration, drawing);

                }
                else if (split[0] == "addx")
                {
                    iteration++;
                    drawing = Draw(RegisterX, iteration, drawing);
                    iteration++;

                    RegisterX += int.Parse(split[1]);

                    drawing = Draw(RegisterX, iteration, drawing);
                }
            }

            for (int i = 0; i < drawing.Length; i++)
            {
                if (i % 40 == 0 && i > 0)
                {
                    Console.Write("\n");
                }

                Console.Write(drawing[i]);
            }
        }

        public static string Draw(int registerx, int iteration, string drawing)
        {

            int position = (iteration - 1) % 40;

            int rowPosition = registerx % 40;

            if (position == rowPosition || position == rowPosition - 1 || position == rowPosition + 1)
            {
                drawing += '#';
            }
            else
            {
                drawing += '.';
            }

            return drawing;
        }
    }
}
