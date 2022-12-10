using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day8
    {
        public static void Run()
        {
            List<string> input = Program.GetInput(8, false);

            Part2(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {

            int counter = 0;

            for (int y = 0; y < input.Count(); y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    if (!isInvisible(x, y, input))
                    {
                        counter++;
                    }
                }
            }

            Console.WriteLine(counter);
        }

        public static void Part2(List<string> input)
        {
            int bestScenicScore = 0;

            for (int y = 0; y < input.Count(); y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    int NewScenicScore = calculateScenicScore(x, y, input);
                    if (NewScenicScore > bestScenicScore)
                    {
                        bestScenicScore = NewScenicScore;
                    }
                }
            }

            Console.WriteLine(bestScenicScore);
        }

        public static bool isInvisible(int x, int y, List<string> input)
        {
            string row = input[y];
            Char charHeight = row[x];
            int height = int.Parse(charHeight.ToString());

            List<int> facingEast = new List<int>();
            List<int> facingWest = new List<int>();
            List<int> facingSouth = new List<int>();
            List<int> facingNorth = new List<int>();

            for (int i = 0; i < x; i++)
            {
                facingEast.Add((int)((input[y][i]) - '0'));
            }

            for (int i = input[y].Length - 1; i > x; i--)
            {
                facingWest.Add((int)((input[y][i]) - '0'));
            }

            for (int i = 0; i < y; i++)
            {
                facingSouth.Add((int)((input[i][x]) - '0'));
            }

            for (int i = input.Count - 1; i > y; i--)
            {
                facingNorth.Add((int)((input[i][x]) - '0'));
            }

            if (facingEast.Count == 0)
                return false;

            bool invisibleFromEast = false;
            bool invisibleFromWest = false;
            bool invisibleFromNorth = false;
            bool invisibleFromSouth = false;

            foreach (int i in facingEast)
            {
                if (i >= height)
                    invisibleFromEast = true;
            }

            foreach (int i in facingWest)
            {
                if (i >= height)
                    invisibleFromWest = true;
            }

            foreach (int i in facingNorth)
            {
                if (i >= height)
                    invisibleFromNorth = true;
            }

            foreach (int i in facingSouth)
            {
                if (i >= height)
                    invisibleFromSouth = true;
            }

            return (invisibleFromEast && invisibleFromNorth && invisibleFromSouth && invisibleFromWest);
        }

        public static int calculateScenicScore(int x, int y, List<string> input)
        {
            string row = input[y];
            Char charHeight = row[x];
            int ownHeight = int.Parse(charHeight.ToString());

            int viewingDistanceEast = 0;
            int viewingDistanceWest = 0;
            int viewingDistanceNorth = 0;
            int viewingDistanceSouth = 0;

            for (int i = x + 1; i < input[y].Length; i++)
            {
                if (int.Parse(input[y][i].ToString()) < ownHeight)
                {
                    viewingDistanceEast++;
                }
                else
                {
                    viewingDistanceEast++;
                    break;
                }
            }

            for (int i = x - 1; i >= 0; i--)
            {
                if (int.Parse(input[y][i].ToString()) < ownHeight)
                {
                    viewingDistanceWest++;
                }
                else
                {
                    viewingDistanceWest++;
                    break;
                }
            }

            for (int i = y + 1; i < input.Count(); i++)
            {
                if (int.Parse(input[i][x].ToString()) < ownHeight)
                {
                    viewingDistanceSouth++;
                }
                else
                {
                    viewingDistanceSouth++;
                    break;
                }
            }

            for (int i = y - 1; i >= 0; i--)
            {
                if (int.Parse(input[i][x].ToString()) < ownHeight)
                {
                    viewingDistanceNorth++;
                }
                else
                {
                    viewingDistanceNorth++;
                    break;
                }
            }

            return viewingDistanceEast * viewingDistanceNorth * viewingDistanceSouth * viewingDistanceWest;
        }
    }
}
