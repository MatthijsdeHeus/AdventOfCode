using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day9
    {
        public static void Run()
        {
            List<string> input = Program.GetInput(9, false);

            Part2(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            List<(int, int)> CoordsVisited = new List<(int, int)>();

            int HeadX = 0;
            int HeadY = 0;

            int TailX = 0;
            int TailY = 0;

            CoordsVisited.Add((TailX, TailY));
            Console.WriteLine("Head = (" + HeadX + ", " + HeadY + ") Tail = (" + TailX + ", " + TailY + ")");

            foreach (string step in input)
            {
                var split = step.Split(' ');

                var direction = split[0];
                var amount = int.Parse(split[1]);



                Console.WriteLine(" ===== " + direction + " " + amount + " =====");

                for (int i = 1; i <= amount; i++)
                {
                    switch (direction)
                    {
                        case "U":
                            HeadY++;
                            break;
                        case "R":
                            HeadX++;
                            break;
                        case "D":
                            HeadY--;
                            break;
                        case "L":
                            HeadX--;
                            break;
                    }

                    ((HeadX, HeadY), (TailX, TailY)) = GetNewCoords2(HeadX, HeadY, TailX, TailY);

                    CoordsVisited.Add((TailX, TailY));
                }
            }

            Console.WriteLine(CoordsVisited.Distinct().Count());
        }

        public static ((int, int), (int, int)) GetNewCoords(int HeadX, int HeadY, int TailX, int TailY, string Direction)
        {
            switch (Direction)
            {
                case "U":
                    HeadY++;
                    break;
                case "R":
                    HeadX++;
                    break;
                case "D":
                    HeadY--;
                    break;
                case "L":
                    HeadX--;
                    break;
            }

            if (HeadY == TailY)
            {
                if (HeadX + 1 < TailX)
                {
                    TailX--;
                    return ((HeadX, HeadY), (TailX, TailY));
                }
                else if (HeadX > TailX + 1)
                {
                    TailX++;
                    return ((HeadX, HeadY), (TailX, TailY));
                }
            }

            if (HeadX == TailX)
            {
                if (HeadY + 1 < TailY)
                {
                    TailY--;

                    return ((HeadX, HeadY), (TailX, TailY));
                }
                else if (HeadY > TailY + 1)
                {
                    TailY++;

                    return ((HeadX, HeadY), (TailX, TailY));
                }
            }

            int xDifference = HeadX - TailX;
            int yDifference = HeadY - TailY;

            if (Math.Abs(xDifference) >= 2 || Math.Abs(yDifference) >= 2)
            {
                TailX += Math.Sign(xDifference);
                TailY += Math.Sign(yDifference);
            }

            return ((HeadX, HeadY), (TailX, TailY));
            //Console.WriteLine("Head = (" + HeadX + ", " + HeadY + ") Tail = (" + TailX + ", " + TailY + ")");
        }

        public static ((int, int), (int, int)) GetNewCoords2(int HeadX, int HeadY, int TailX, int TailY)
        {

            if (HeadY == TailY)
            {
                if (HeadX + 1 < TailX)
                {
                    TailX--;
                    return ((HeadX, HeadY), (TailX, TailY));
                }
                else if (HeadX > TailX + 1)
                {
                    TailX++;
                    return ((HeadX, HeadY), (TailX, TailY));
                }
            }

            if (HeadX == TailX)
            {
                if (HeadY + 1 < TailY)
                {
                    TailY--;

                    return ((HeadX, HeadY), (TailX, TailY));
                }
                else if (HeadY > TailY + 1)
                {
                    TailY++;

                    return ((HeadX, HeadY), (TailX, TailY));
                }
            }

            int xDifference = HeadX - TailX;
            int yDifference = HeadY - TailY;

            if (Math.Abs(xDifference) >= 2 || Math.Abs(yDifference) >= 2)
            {
                TailX += Math.Sign(xDifference);
                TailY += Math.Sign(yDifference);
            }

            return ((HeadX, HeadY), (TailX, TailY));
            //Console.WriteLine("Head = (" + HeadX + ", " + HeadY + ") Tail = (" + TailX + ", " + TailY + ")");
        }

        public static void Part2(List<string> input)
        {
            List<(int, int)> CoordsVisited = new List<(int, int)>();

            int HeadX = 0;
            int HeadY = 0;

            int Tail1X = 0;
            int Tail1Y = 0;

            int Tail2X = 0;
            int Tail2Y = 0;

            int Tail3X = 0;
            int Tail3Y = 0;

            int Tail4X = 0;
            int Tail4Y = 0;

            int Tail5X = 0;
            int Tail5Y = 0;

            int Tail6X = 0;
            int Tail6Y = 0;

            int Tail7X = 0;
            int Tail7Y = 0;

            int Tail8X = 0;
            int Tail8Y = 0;

            int Tail9X = 0;
            int Tail9Y = 0;

            //int tailDistanceToHead = 0;
            CoordsVisited.Add((Tail9X, Tail9Y));
            //Console.WriteLine("Head = (" + HeadX + ", " + HeadY + ") Tail = (" + TailX + ", " + TailY + ")");

            foreach (string step in input)
            {
                var split = step.Split(' ');

                var direction = split[0];
                var amount = int.Parse(split[1]);

                //Console.WriteLine(" ===== " + direction + " " + amount + " =====");

                for (int i = 1; i <= amount; i++)
                {
                    switch (direction)
                    {
                        case "U":
                            HeadY++;
                            break;
                        case "R":
                            HeadX++;
                            break;
                        case "D":
                            HeadY--;
                            break;
                        case "L":
                            HeadX--;
                            break;
                    }

                    ((HeadX, HeadY), (Tail1X, Tail1Y)) = GetNewCoords2(HeadX, HeadY, Tail1X, Tail1Y);
                    ((Tail1X, Tail1Y), (Tail2X, Tail2Y)) = GetNewCoords2(Tail1X, Tail1Y, Tail2X, Tail2Y);
                    ((Tail2X, Tail2Y), (Tail3X, Tail3Y)) = GetNewCoords2(Tail2X, Tail2Y, Tail3X, Tail3Y);
                    ((Tail3X, Tail3Y), (Tail4X, Tail4Y)) = GetNewCoords2(Tail3X, Tail3Y, Tail4X, Tail4Y);
                    ((Tail4X, Tail4Y), (Tail5X, Tail5Y)) = GetNewCoords2(Tail4X, Tail4Y, Tail5X, Tail5Y);
                    ((Tail5X, Tail5Y), (Tail6X, Tail6Y)) = GetNewCoords2(Tail5X, Tail5Y, Tail6X, Tail6Y);
                    ((Tail6X, Tail6Y), (Tail7X, Tail7Y)) = GetNewCoords2(Tail6X, Tail6Y, Tail7X, Tail7Y);
                    ((Tail7X, Tail7Y), (Tail8X, Tail8Y)) = GetNewCoords2(Tail7X, Tail7Y, Tail8X, Tail8Y);
                    ((Tail8X, Tail8Y), (Tail9X, Tail9Y)) = GetNewCoords2(Tail8X, Tail8Y, Tail9X, Tail9Y);

                    CoordsVisited.Add((Tail9X, Tail9Y));
                }
            }

            Console.WriteLine(CoordsVisited.Distinct().Count());
        }
    }
}
