using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day15
    {
        public static bool useTestInput = false;

        public static Dictionary<int, List<(int, int)>> rowCheckedList = new Dictionary<int, List<(int, int)>>();

        public static int minSearchValue = 0;

        public static int maxSearchValue;

        public static int checkRow;

        public static void Run()
        {
            List<string> input = Program.GetInput(15, useTestInput);

            Solution(input);

            Console.ReadKey();
        }

        public static void Solution(List<string> input)
        {
            Dictionary<(int, int), string> gridDict = new Dictionary<(int, int), string>();

            if(useTestInput)
            {
                checkRow = 10;

                for(int i = -10; i <= 30; i++)
                {
                    rowCheckedList[i] = new List<(int, int)>();
                }

                maxSearchValue = 20;
            }
            else
            {
                checkRow = 2000000;

                for (int i = -2_000_000; i <= 10_000_000; i++)
                {
                    rowCheckedList[i] = new List<(int, int)>();
                }

                maxSearchValue = 4_000_000;
            }

            foreach (string line in input)
            {
                Console.WriteLine("newLine");
                string[] parts = line.Split(' ');

                char[] charsToTrim = { ',', ':' };

                string sensorcoordx = parts[2].TrimEnd(charsToTrim);
                string sensorcoordy = parts[3].TrimEnd(charsToTrim);

                string closestbeaconcoordx = parts[8].TrimEnd(charsToTrim);
                string closestbeaconcoordy = parts[9].TrimEnd(charsToTrim);

                int sensorX = int.Parse(sensorcoordx.Split('=')[1]);
                int sensorY = int.Parse(sensorcoordy.Split('=')[1]);

                int closestbeaconX = int.Parse(closestbeaconcoordx.Split('=')[1]);
                int closestbeaconY = int.Parse(closestbeaconcoordy.Split('=')[1]);

                markGrid(gridDict, sensorX, sensorY, closestbeaconX, closestbeaconY);

                gridDict[(sensorX, sensorY)] = "S";
                gridDict[(closestbeaconX, closestbeaconY)] = "B";

                
            }

            // Part 2
            for(int y = 0; y < maxSearchValue; y++)
            {
                if(y % 100 == 0)
                    Console.WriteLine(y / (double)4000000 * 100 + "%");

                for (int x = 0; x <= maxSearchValue; x++)
                {
                    int result = isInsideInterval(x, y);

                    if (result == -1)
                    {
                        Console.WriteLine("FOUND x = " + x + " y = " + (y + 1));
                        Console.WriteLine($"Result = {x * 4000000 + y + 1}");
                        return;
                    }
                    else
                    {
                        x = result;
                    }
                }
            }
        }

        public static int isInsideInterval(int x, int row)
        {
            foreach ((int min, int max) interval in rowCheckedList[row + 1])
            {
                if (interval.min <= x && x <= interval.max)
                {
                    return interval.max;
                }
            }

            return -1;
        }

        public static void markGrid(Dictionary<(int, int), string> gridDict, int sensorX, int sensorY, int closestbeaconX, int closestbeaconY)
        {
            int range = Math.Abs(closestbeaconX - sensorX) + Math.Abs(closestbeaconY - sensorY);

            for(int y = sensorY + range; y >= sensorY - range; y--)
            {
                int minX = sensorX + Math.Abs(y - sensorY) - range;
                int maxX = sensorX - 1 * (Math.Abs(y - sensorY) - range);

                {
                    rowCheckedList[y].Add((minX, maxX));
                }
            }
        }

        public static void printGrid(Dictionary<(int, int), string> gridDict)
        {
            for(int y = -10; y <= 30; y++)
            {
                string row = "";

                for(int x = -10; x <= 30; x++)
                {

                    if (!gridDict.ContainsKey((x, y)))
                    {
                        row += ".";
                    }
                    else if(gridDict[(x, y)] == "#")
                    { 
                        row += "#";
                    }
                    else if (gridDict[(x, y)] == "B")
                    {
                        row += "B";
                    }
                    else if (gridDict[(x, y)] == "S")
                    {
                        row += "S";
                    }
                    else
                    {
                       /* if (x < 0 || x > 20 || y < 0 || y > 0)
                        {
                            row += " ";
                        }
                        else
                        {
                            row += ".";
                        }*/
                        
                    }

                    

                    //row += grid[x, y];
                }

                Console.WriteLine(row);
            }

            Console.WriteLine();
        }

    }
}
