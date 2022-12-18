using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day18
    {
        public static void Run()
        {
            List<string> input = Program.GetInput(18, false);

            Part2(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            Queue<(int, int, int)> cubeQueue = new Queue<(int, int, int)>();

            int ConnectedCount = 0;

            foreach (var line in input)
            {
                string[] split = line.Split(',');

                cubeQueue.Enqueue((int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
            }

            for(int i = 0; i < cubeQueue.Count; i++) 
            {
                var currentCube = cubeQueue.Dequeue();

                if(cubeQueue.Any(cube => cube.Item1 == currentCube.Item1 + 1 && cube.Item2 == currentCube.Item2 && cube.Item3 == currentCube.Item3))
                {
                    ConnectedCount++;
                }

                if(cubeQueue.Any(cube => cube.Item1 == currentCube.Item1 - 1 && cube.Item2 == currentCube.Item2 && cube.Item3 == currentCube.Item3))
                {
                    ConnectedCount++;
                }

                if(cubeQueue.Any(cube => cube.Item1 == currentCube.Item1 && cube.Item2 == currentCube.Item2 + 1 && cube.Item3 == currentCube.Item3))
                {
                    ConnectedCount++;
                }

                if(cubeQueue.Any(cube => cube.Item1 == currentCube.Item1 && cube.Item2 == currentCube.Item2 - 1 && cube.Item3 == currentCube.Item3))
                {
                    ConnectedCount++;
                }

                if(cubeQueue.Any(cube => cube.Item1 == currentCube.Item1 && cube.Item2 == currentCube.Item2 && cube.Item3 == currentCube.Item3 + 1))
                {
                    ConnectedCount++;
                }

                if(cubeQueue.Any(cube => cube.Item1 == currentCube.Item1 && cube.Item2 == currentCube.Item2 && cube.Item3 == currentCube.Item3 - 1))
                {
                    ConnectedCount++;
                }

                cubeQueue.Enqueue(currentCube);
            }

            int totalSurface = cubeQueue.Count() * 6 - ConnectedCount;

            Console.WriteLine(totalSurface);
        }

        public static void Part2(List<string> input)
        {
            List<(int, int, int)> data = new List<(int, int, int)>();

            int xmin = int.MaxValue;
            int xmax = int.MinValue;

            int ymin = int.MaxValue;
            int ymax = int.MinValue;

            int zmin = int.MaxValue;
            int zmax = int.MinValue;

            foreach (var line in input)
            {
                string[] split = line.Split(',');

                int TempX = int.Parse(split[0]);
                int TempY = int.Parse(split[1]);
                int TempZ = int.Parse(split[2]);

                if(TempX > xmax)
                {
                    xmax = TempX + 2;
                }

                if(TempX < xmin)
                {
                    xmin = TempX - 2;
                }

                if (TempY > ymax)
                {
                    ymax = TempY + 2;
                }

                if (TempY < ymin)
                {
                    ymin = TempY - 2;
                }

                if (TempZ > zmax)
                {
                    zmax = TempZ + 2;
                }

                if (TempZ < zmin)
                {
                    zmin = TempZ - 2;
                }


                data.Add((TempX, TempY, TempZ));
            }

            

            List<(int, int, int)> exteriorMinMax = new List<(int, int, int)>();

            int hits = 0;

            Dictionary<(int, int), int> zMinDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> zMaxDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> xMinDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> xMaxDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> yMinDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> yMaxDictionary = new Dictionary<(int, int), int>();

            // z positive direction
            for (int x = xmin; x < xmax; x++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int z = zmin; z < zmax; z++)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("z+");
                            zMinDictionary.Add((x, y), z);
                            hits++;
                            break;
                        }
                    }
                }
            }

            // z negative direction
            for (int x = xmin; x < xmax; x++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int z = zmax; z > zmin; z--)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("z-");
                            zMaxDictionary.Add((x, y), z);
                            hits++;
                            break;
                        }
                    }
                }
            }

            // x positive direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int x = xmin; x < xmax; x++)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("x+");
                            xMinDictionary.Add((y, z), x);
                            hits++;
                            break;
                        }
                    }
                }
            }

            // x negative direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int x = xmax; x > xmin; x--)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("x-");
                            xMaxDictionary.Add((y, z), x);
                            hits++;
                            break;
                        }
                    }
                }
            }

            // y positive direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    for (int y = ymin; y < ymax; y++)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("y+");
                            yMinDictionary.Add((x, z), y);
                            hits++;
                            break;
                        }
                    }
                }
            }

            // y negative direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    for (int y = ymax; y > ymin; y--)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("y-");
                            yMaxDictionary.Add((x, z), y);
                            hits++;
                            break;
                        }
                    }
                }
            }





            LinkedList<(int, int, int)> cubeList = new LinkedList<(int, int, int)>();

            foreach (var line in input)
            {
                string[] split = line.Split(',');

                cubeList.AddFirst((int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
            }

            Console.WriteLine("Part 2.1:");

            for (int x = xmin; x < xmax; x++)
            {
                Console.WriteLine($"X: {x}/{xmax}");

                for (int y = ymin; y < ymax; y++)
                {
                    if(zMinDictionary.ContainsKey((x, y)))
                    {
                        for (int z = zMinDictionary[(x, y)]; z <= zMaxDictionary[(x, y)]; z++)
                        {
                            if (!data.Contains((x, y, z)))
                            {
                                if(cubeList.Contains((x, y, z - 1)))
                                {
                                    cubeList.AddFirst((x, y, z));

                                    (Dictionary<(int, int), int> d1, Dictionary<(int, int), int> d2, Dictionary<(int, int), int> d3, Dictionary<(int, int), int> d4, Dictionary<(int, int), int> d5, Dictionary<(int, int), int> d6) dictionaries = getDictionaries(cubeList, xmin, xmax, ymin, ymax, zmin, zmax);

                                    if (dictionaries.d1.Keys.Count == xMinDictionary.Keys.Count && dictionaries.d1.Keys.All(key => xMinDictionary.ContainsKey(key) && Equals(dictionaries.d1[key], xMinDictionary[key])) &&
                                        dictionaries.d2.Keys.Count == xMaxDictionary.Keys.Count && dictionaries.d2.Keys.All(key => xMaxDictionary.ContainsKey(key) && Equals(dictionaries.d2[key], xMaxDictionary[key])) &&
                                        dictionaries.d3.Keys.Count == yMinDictionary.Keys.Count && dictionaries.d3.Keys.All(key => yMinDictionary.ContainsKey(key) && Equals(dictionaries.d3[key], yMinDictionary[key])) &&
                                        dictionaries.d4.Keys.Count == yMaxDictionary.Keys.Count && dictionaries.d4.Keys.All(key => yMaxDictionary.ContainsKey(key) && Equals(dictionaries.d4[key], yMaxDictionary[key])) &&
                                        dictionaries.d5.Keys.Count == zMinDictionary.Keys.Count && dictionaries.d5.Keys.All(key => zMinDictionary.ContainsKey(key) && Equals(dictionaries.d5[key], zMinDictionary[key])) &&
                                        dictionaries.d6.Keys.Count == zMaxDictionary.Keys.Count && dictionaries.d6.Keys.All(key => zMaxDictionary.ContainsKey(key) && Equals(dictionaries.d6[key], zMaxDictionary[key])))
                                    {

                                    }
                                    else
                                    {
                                        hits++;
                                    }

                                    cubeList.RemoveFirst();
                                }

                                if (cubeList.Contains((x, y, z + 1)))
                                {
                                    cubeList.AddFirst((x, y, z));

                                    (Dictionary<(int, int), int> d1, Dictionary<(int, int), int> d2, Dictionary<(int, int), int> d3, Dictionary<(int, int), int> d4, Dictionary<(int, int), int> d5, Dictionary<(int, int), int> d6) dictionaries = getDictionaries(cubeList, xmin, xmax, ymin, ymax, zmin, zmax);

                                    if (dictionaries.d1.Keys.Count == xMinDictionary.Keys.Count && dictionaries.d1.Keys.All(key => xMinDictionary.ContainsKey(key) && Equals(dictionaries.d1[key], xMinDictionary[key])) &&
                                        dictionaries.d2.Keys.Count == xMaxDictionary.Keys.Count && dictionaries.d2.Keys.All(key => xMaxDictionary.ContainsKey(key) && Equals(dictionaries.d2[key], xMaxDictionary[key])) &&
                                        dictionaries.d3.Keys.Count == yMinDictionary.Keys.Count && dictionaries.d3.Keys.All(key => yMinDictionary.ContainsKey(key) && Equals(dictionaries.d3[key], yMinDictionary[key])) &&
                                        dictionaries.d4.Keys.Count == yMaxDictionary.Keys.Count && dictionaries.d4.Keys.All(key => yMaxDictionary.ContainsKey(key) && Equals(dictionaries.d4[key], yMaxDictionary[key])) &&
                                        dictionaries.d5.Keys.Count == zMinDictionary.Keys.Count && dictionaries.d5.Keys.All(key => zMinDictionary.ContainsKey(key) && Equals(dictionaries.d5[key], zMinDictionary[key])) &&
                                        dictionaries.d6.Keys.Count == zMaxDictionary.Keys.Count && dictionaries.d6.Keys.All(key => zMaxDictionary.ContainsKey(key) && Equals(dictionaries.d6[key], zMaxDictionary[key])))
                                    {

                                    }
                                    else
                                    {
                                        hits++;
                                    }

                                    cubeList.RemoveFirst();
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Part 2.2:");

            for (int x = xmin; x < xmax; x++)
            {
                Console.WriteLine($"x: {x}/{xmax}");

                for (int z = zmin; z < zmax; z++)
                {
                    if (yMinDictionary.ContainsKey((x, z)))
                    {
                        for (int y = yMinDictionary[(x, z)]; y <= yMaxDictionary[(x, z)]; y++)
                        {
                            if (!data.Contains((x, y, z)))
                            {
                                if (cubeList.Contains((x, y - 1, z)))
                                {
                                    cubeList.AddFirst((x, y, z));

                                    (Dictionary<(int, int), int> d1, Dictionary<(int, int), int> d2, Dictionary<(int, int), int> d3, Dictionary<(int, int), int> d4, Dictionary<(int, int), int> d5, Dictionary<(int, int), int> d6) dictionaries = getDictionaries(cubeList, xmin, xmax, ymin, ymax, zmin, zmax);

                                    if (dictionaries.d1.Keys.Count == xMinDictionary.Keys.Count && dictionaries.d1.Keys.All(key => xMinDictionary.ContainsKey(key) && Equals(dictionaries.d1[key], xMinDictionary[key])) &&
                                        dictionaries.d2.Keys.Count == xMaxDictionary.Keys.Count && dictionaries.d2.Keys.All(key => xMaxDictionary.ContainsKey(key) && Equals(dictionaries.d2[key], xMaxDictionary[key])) &&
                                        dictionaries.d3.Keys.Count == yMinDictionary.Keys.Count && dictionaries.d3.Keys.All(key => yMinDictionary.ContainsKey(key) && Equals(dictionaries.d3[key], yMinDictionary[key])) &&
                                        dictionaries.d4.Keys.Count == yMaxDictionary.Keys.Count && dictionaries.d4.Keys.All(key => yMaxDictionary.ContainsKey(key) && Equals(dictionaries.d4[key], yMaxDictionary[key])) &&
                                        dictionaries.d5.Keys.Count == zMinDictionary.Keys.Count && dictionaries.d5.Keys.All(key => zMinDictionary.ContainsKey(key) && Equals(dictionaries.d5[key], zMinDictionary[key])) &&
                                        dictionaries.d6.Keys.Count == zMaxDictionary.Keys.Count && dictionaries.d6.Keys.All(key => zMaxDictionary.ContainsKey(key) && Equals(dictionaries.d6[key], zMaxDictionary[key])))
                                    {

                                    }
                                    else
                                    {
                                        hits++;
                                    }

                                    cubeList.RemoveFirst();
                                }

                                if (cubeList.Contains((x, y + 1, z)))
                                {
                                    cubeList.AddFirst((x, y, z));

                                    (Dictionary<(int, int), int> d1, Dictionary<(int, int), int> d2, Dictionary<(int, int), int> d3, Dictionary<(int, int), int> d4, Dictionary<(int, int), int> d5, Dictionary<(int, int), int> d6) dictionaries = getDictionaries(cubeList, xmin, xmax, ymin, ymax, zmin, zmax);

                                    if (dictionaries.d1.Keys.Count == xMinDictionary.Keys.Count && dictionaries.d1.Keys.All(key => xMinDictionary.ContainsKey(key) && Equals(dictionaries.d1[key], xMinDictionary[key])) &&
                                        dictionaries.d2.Keys.Count == xMaxDictionary.Keys.Count && dictionaries.d2.Keys.All(key => xMaxDictionary.ContainsKey(key) && Equals(dictionaries.d2[key], xMaxDictionary[key])) &&
                                        dictionaries.d3.Keys.Count == yMinDictionary.Keys.Count && dictionaries.d3.Keys.All(key => yMinDictionary.ContainsKey(key) && Equals(dictionaries.d3[key], yMinDictionary[key])) &&
                                        dictionaries.d4.Keys.Count == yMaxDictionary.Keys.Count && dictionaries.d4.Keys.All(key => yMaxDictionary.ContainsKey(key) && Equals(dictionaries.d4[key], yMaxDictionary[key])) &&
                                        dictionaries.d5.Keys.Count == zMinDictionary.Keys.Count && dictionaries.d5.Keys.All(key => zMinDictionary.ContainsKey(key) && Equals(dictionaries.d5[key], zMinDictionary[key])) &&
                                        dictionaries.d6.Keys.Count == zMaxDictionary.Keys.Count && dictionaries.d6.Keys.All(key => zMaxDictionary.ContainsKey(key) && Equals(dictionaries.d6[key], zMaxDictionary[key])))
                                    {

                                    }
                                    else
                                    {
                                        hits++;
                                    }

                                    cubeList.RemoveFirst();
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Part 2.3:");

            for (int y = ymin; y < ymax; y++)
            {
                Console.WriteLine($"{y}/{ymax}");
                for (int z = zmin; z < zmax; z++)
                {
                    if (xMinDictionary.ContainsKey((y, z)))
                    {
                        for (int x = xMinDictionary[(y, z)]; x <= xMaxDictionary[(y, z)]; x++)
                        {
                            if (!data.Contains((x, y, z)))
                            {
                                if (cubeList.Contains((x - 1, y, z)))
                                {
                                    cubeList.AddFirst((x, y, z));

                                    (Dictionary<(int, int), int> d1, Dictionary<(int, int), int> d2, Dictionary<(int, int), int> d3, Dictionary<(int, int), int> d4, Dictionary<(int, int), int> d5, Dictionary<(int, int), int> d6) dictionaries = getDictionaries(cubeList, xmin, xmax, ymin, ymax, zmin, zmax);

                                    if (dictionaries.d1.Keys.Count == xMinDictionary.Keys.Count && dictionaries.d1.Keys.All(key => xMinDictionary.ContainsKey(key) && Equals(dictionaries.d1[key], xMinDictionary[key])) &&
                                        dictionaries.d2.Keys.Count == xMaxDictionary.Keys.Count && dictionaries.d2.Keys.All(key => xMaxDictionary.ContainsKey(key) && Equals(dictionaries.d2[key], xMaxDictionary[key])) &&
                                        dictionaries.d3.Keys.Count == yMinDictionary.Keys.Count && dictionaries.d3.Keys.All(key => yMinDictionary.ContainsKey(key) && Equals(dictionaries.d3[key], yMinDictionary[key])) &&
                                        dictionaries.d4.Keys.Count == yMaxDictionary.Keys.Count && dictionaries.d4.Keys.All(key => yMaxDictionary.ContainsKey(key) && Equals(dictionaries.d4[key], yMaxDictionary[key])) &&
                                        dictionaries.d5.Keys.Count == zMinDictionary.Keys.Count && dictionaries.d5.Keys.All(key => zMinDictionary.ContainsKey(key) && Equals(dictionaries.d5[key], zMinDictionary[key])) &&
                                        dictionaries.d6.Keys.Count == zMaxDictionary.Keys.Count && dictionaries.d6.Keys.All(key => zMaxDictionary.ContainsKey(key) && Equals(dictionaries.d6[key], zMaxDictionary[key])))
                                    {

                                    }
                                    else
                                    {
                                        hits++;
                                    }
                                }

                                if (cubeList.Contains((x + 1, y, z)))
                                {
                                    cubeList.AddFirst((x, y, z));

                                    (Dictionary<(int, int), int> d1, Dictionary<(int, int), int> d2, Dictionary<(int, int), int> d3, Dictionary<(int, int), int> d4, Dictionary<(int, int), int> d5, Dictionary<(int, int), int> d6) dictionaries = getDictionaries(cubeList, xmin, xmax, ymin, ymax, zmin, zmax);

                                    if (dictionaries.d1.Keys.Count == xMinDictionary.Keys.Count && dictionaries.d1.Keys.All(key => xMinDictionary.ContainsKey(key) && Equals(dictionaries.d1[key], xMinDictionary[key])) &&
                                        dictionaries.d2.Keys.Count == xMaxDictionary.Keys.Count && dictionaries.d2.Keys.All(key => xMaxDictionary.ContainsKey(key) && Equals(dictionaries.d2[key], xMaxDictionary[key])) &&
                                        dictionaries.d3.Keys.Count == yMinDictionary.Keys.Count && dictionaries.d3.Keys.All(key => yMinDictionary.ContainsKey(key) && Equals(dictionaries.d3[key], yMinDictionary[key])) &&
                                        dictionaries.d4.Keys.Count == yMaxDictionary.Keys.Count && dictionaries.d4.Keys.All(key => yMaxDictionary.ContainsKey(key) && Equals(dictionaries.d4[key], yMaxDictionary[key])) &&
                                        dictionaries.d5.Keys.Count == zMinDictionary.Keys.Count && dictionaries.d5.Keys.All(key => zMinDictionary.ContainsKey(key) && Equals(dictionaries.d5[key], zMinDictionary[key])) &&
                                        dictionaries.d6.Keys.Count == zMaxDictionary.Keys.Count && dictionaries.d6.Keys.All(key => zMaxDictionary.ContainsKey(key) && Equals(dictionaries.d6[key], zMaxDictionary[key])))
                                    {

                                    }
                                    else
                                    {
                                        hits++;
                                    }

                                    cubeList.RemoveFirst();
                                }
                            }
                        }
                    }
                }
            }


            Console.WriteLine(hits);

        }


        public static (Dictionary<(int, int), int>, Dictionary<(int, int), int>, Dictionary<(int, int), int>, Dictionary<(int, int), int>, Dictionary<(int, int), int>, Dictionary<(int, int), int>) getDictionaries(LinkedList<(int, int, int)> data, int xmin, int xmax, int ymin, int ymax, int zmin, int zmax)
        {


            /*
            int xMinSize = 0;
            int xMaxSize = 0;
            int yMinSize = 0;
            int yMaxSize = 0;
            int zMinSize = 0;
            int zMaxSize = 0;

            xmin = -10;
            xmax = 50;
            ymin = -10;
            ymax = 50;
            zmin = -10;
            zmax = 50;*/


            Dictionary<(int, int), int> zMinDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> zMaxDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> xMinDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> xMaxDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> yMinDictionary = new Dictionary<(int, int), int>();
            Dictionary<(int, int), int> yMaxDictionary = new Dictionary<(int, int), int>();

            // z positive direction
            for (int x = xmin; x < xmax; x++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int z = zmin; z < zmax; z++)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("z+");
                            zMinDictionary.Add((x, y), z);
                            break;
                        }
                    }
                }
            }

            // z negative direction
            for (int x = xmin; x < xmax; x++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int z = zmax; z > zmin; z--)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("z-");
                            zMaxDictionary.Add((x, y), z);
                            break;
                        }
                    }
                }
            }

            // x positive direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int x = xmin; x < xmax; x++)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("x+");
                            xMinDictionary.Add((y, z), x);
                            break;
                        }
                    }
                }
            }

            // x negative direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int y = ymin; y < ymax; y++)
                {
                    for (int x = xmax; x > xmin; x--)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("x-");
                            xMaxDictionary.Add((y, z), x);
                            break;
                        }
                    }
                }
            }

            // y positive direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    for (int y = ymin; y < ymax; y++)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("y+");
                            yMinDictionary.Add((x, z), y);
                            break;
                        }
                    }
                }
            }

            // y negative direction
            for (int z = zmin; z < zmax; z++)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    for (int y = ymax; y > ymin; y--)
                    {
                        if (data.Contains((x, y, z)))
                        {
                            //Console.WriteLine("y-");
                            yMaxDictionary.Add((x, z), y);
                            break;
                        }
                    }
                }
            }

            return (xMinDictionary, xMaxDictionary, yMinDictionary, yMaxDictionary, zMinDictionary, zMaxDictionary);
        }
    }
}
