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

        public static Dictionary<int, int> rowCount = new Dictionary<int, int>();

        public static int minSearchValue = 0;

        public static int maxSearchValue;

        public static void Run()
        {
            List<string> input = Program.GetInput(15, useTestInput);

            Part1(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            Dictionary<(int, int), string> gridDict = new Dictionary<(int, int), string>();

            if(useTestInput)
            {
                maxSearchValue = 20;
            }
            else
            {
                maxSearchValue = 400000;
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

                markGrid(gridDict, sensorX, sensorY, closestbeaconX, closestbeaconY, minSearchValue, maxSearchValue);

                gridDict[(sensorX, sensorY)] = "S";
                gridDict[(closestbeaconX, closestbeaconY)] = "B";

                
            }

            foreach((int x, int y) key in gridDict.Keys)
            {
                if(!gridDict.ContainsKey((key.x + 1, key.y)) && gridDict.ContainsKey((key.x + 2, key.y)))
                {
                    if(gridDict[key] == "#")
                    {
                        long x = (key.x + 1) * 400000;

                        Console.WriteLine(x + key.y);
                    }
                    
                }
            }

            //markGrid(grid, 5 - xMin, 5 - yMin, 5 - xMin , 3 - yMin);
        }

        public static void markGrid(Dictionary<(int, int), string> gridDict, int sensorX, int sensorY, int closestbeaconX, int closestbeaconY, int minSearchValue, int maxSearchValue)
        {
            int range = Math.Abs(closestbeaconX - sensorX) + Math.Abs(closestbeaconY - sensorY);

            for(int y = Math.Max(minSearchValue, sensorY - range); y <= Math.Min(sensorY + range, maxSearchValue); y++)
            {
                if (rowCount.ContainsKey(y))
                {
                    if(rowCount[y] == maxSearchValue)
                    {
                        if(y == maxSearchValue)
                        {
                            maxSearchValue--;
                            Console.WriteLine(maxSearchValue);
                        } else if(y == minSearchValue)
                        {
                            minSearchValue++;
                        }
                    }
                        
                }
                else
                {
                    rowCount[y] = 0;
                }

                for (int x = Math.Max(minSearchValue, sensorX - range); x <= Math.Min(maxSearchValue, sensorX + range); x++)
                {
                    if (gridDict.ContainsKey((x, y)))
                        continue;

                    rowCount[y]++;
                    


                    /*if (y < 0 || y > 20)
                    {
                        continue;
                    }*/

                    int dy = Math.Abs(sensorY - y);
                    int dx = Math.Abs(sensorX - x);

                    if(dy + dx <= range)
                    {
                        gridDict[(x, y)] = "#";
                    }

                }
            }
            //Console.WriteLine("");
            //printGrid(gridDict);
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
                        if (x < 0 || x > 20 || y < 0 || y > 20)
                        {
                            row += " ";
                        }
                        else
                        {
                            row += "#";
                        }
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
                        if (x < 0 || x > 20 || y < 0 || y > 0)
                        {
                            row += " ";
                        }
                        else
                        {
                            row += ".";
                        }
                        
                    }

                    

                    //row += grid[x, y];
                }

                Console.WriteLine(row);
            }

            Console.WriteLine();
        }
    }
}
