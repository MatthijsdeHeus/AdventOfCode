using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day14
    {
        public static bool useTestInput = false;

        public static void Run()
        {
            List<string> input = Program.GetInput(14, useTestInput);

            Part2(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            int sandRestCount = 0;

            string[,] grid = new string[1000, 1005];

            foreach(string s in input)
            {
                string[] coordsArray = s.Split(" -> ".ToCharArray());

                List<(int, int)> rockList = new List<(int, int)>();

                foreach(string coord in coordsArray)
                {
                    if (coord != "")
                    {
                        string[] xySplit = coord.Split(',');

                        int x = int.Parse(xySplit[0]);
                        int y = int.Parse(xySplit[1]);

                        rockList.Add((x, y));
                    }
                }

                for(int i = 0; i < rockList.Count; i++)
                {
                    (int fromX, int fromY) = rockList[i];

                    if(i + 1 < rockList.Count)
                    {
                        (int toX, int toY) = rockList[i + 1];

                        int xDif = toX - fromX;
                        int yDif = toY - fromY;

                        int currentX = fromX;
                        int currentY = fromY;

                        while (currentX != toX || currentY != toY)
                        {
                            grid[currentX, currentY] = "#";

                            currentX += Math.Sign(xDif);
                            currentY += Math.Sign(yDif);
                        }

                        grid[currentX, currentY] = "#";
                    }
                }
            }

            printGrid(grid);

            while (true)
            {
                (int x, int y) = getSandCoord(grid);

                if(y >= 1000)
                {
                    break;
                }

                grid[x, y] = "O";

                sandRestCount++;
            }

            Console.WriteLine(sandRestCount);

        }

        public static void Part2(List<string> input)
        {
            int sandRestCount = 0;

            string[,] grid = new string[1000, 1005];

            int floor;

            foreach (string s in input)
            {
                string[] coordsArray = s.Split(" -> ".ToCharArray());

                List<(int, int)> rockList = new List<(int, int)>();

                foreach (string coord in coordsArray)
                {
                    if (coord != "")
                    {
                        string[] xySplit = coord.Split(',');

                        int x = int.Parse(xySplit[0]);
                        int y = int.Parse(xySplit[1]);

                        rockList.Add((x, y));
                    }
                }

                for (int i = 0; i < rockList.Count; i++)
                {
                    (int fromX, int fromY) = rockList[i];

                    if (i + 1 < rockList.Count)
                    {
                        (int toX, int toY) = rockList[i + 1];

                        int xDif = toX - fromX;
                        int yDif = toY - fromY;

                        int currentX = fromX;
                        int currentY = fromY;

                        while (currentX != toX || currentY != toY)
                        {
                            grid[currentX, currentY] = "#";

                            currentX += Math.Sign(xDif);
                            currentY += Math.Sign(yDif);
                        }

                        grid[currentX, currentY] = "#";
                    }
                }
            }

            int maxY = 0;

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] != null)
                    {
                        if (y > maxY)
                        {
                            maxY = y;
                        }
                    }
                }
            }

            floor = maxY + 2;

            int currentX2 = 0;
            int currentY2 = floor;

            while (currentX2 != 1000)
            {
                grid[currentX2, currentY2] = "#";

                currentX2 += 1;
            }

            printGrid(grid);

            while (true)
            {
                (int x, int y) = getSandCoord(grid);

                if (y >= 1000)
                {
                    break;
                }

                

                grid[x, y] = "O";
                
                sandRestCount++;

                if (grid[500, 0] != null)
                {
                    break;
                }
            }

            Console.WriteLine(sandRestCount);

        }

        public static (int x, int y) getSandCoord(string[,] grid)
        {
            int currentX = 500;
            int currentY = 0;

            while (true)
            {
                if (currentY >= 1000)
                {
                    break;
                }

                if (grid[currentX, currentY + 1] == null)
                {
                    currentY++;
                    continue;
                } 

                if (grid[currentX - 1, currentY + 1] == null)
                {
                    currentX--;
                    currentY++;
                    continue;
                }

                if(grid[currentX + 1, currentY + 1] == null)
                {
                    currentX++;
                    currentY++;
                    continue;
                }

                break;
            }

            return (currentX, currentY);
        }

        public static (int x, int y) getSandCoord2(string[,] grid)
        {
            int currentX = 500;
            int currentY = 0;

            while (true)
            {
                if (currentY == 1000)
                {
                    break;
                }

                if (grid[currentX, currentY + 1] == null)
                {
                    currentY++;
                    continue;
                }

                if (grid[currentX - 1, currentY + 1] == null)
                {
                    currentX--;
                    currentY++;
                    continue;
                }

                if (grid[currentX + 1, currentY + 1] == null)
                {
                    currentX++;
                    currentY++;
                    continue;
                }

                break;
            }

            return (currentX, currentY);
        }

        public static void printGrid(string[,] grid)
        {
            int minX = 500;
            int maxX = -500;

            int minY = 0;
            int maxY = 1;

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] != null)
                    {
                        if (x < minX)
                        {
                            minX = x;
                        }

                        if (x > maxX)
                        {
                            maxX = x;
                        }

                        if (y > maxY)
                        {
                            maxY = y;
                        }
                    }
                }
            }

            for (int y = minY; y <= maxY; y++)
            {
                string line = "";

                for (int x = minX; x <= maxX; x++)
                {
                    if(grid[x, y] == null)
                    {
                        line += ".";
                    }
                    else
                    {
                        line += grid[x, y];
                    }
                    
                }

                Console.WriteLine(line);
            }
        }
    }
}
