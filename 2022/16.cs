using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day16
    {
        public static bool useTestInput = true;

        public static Dictionary<string, (int, string[])> valveInfo = new Dictionary<string, (int, string[])>();

        public static Dictionary<(string, string), int> valveDistance = new Dictionary<(string, string), int>();

        public static int inputSize;

        public static void Run()
        {
            List<string> input = Program.GetInput(16, useTestInput);

            inputSize = input.Count;

            Solution(input);

            Console.ReadKey();
        }

        public static void Solution(List<string> input)
        {
            foreach (string line in input)
            {
                string[] split = line.Split(' ');

                string name = split[1];
                string flowrate = split[4].TrimEnd(';').Split('=')[1];

                string[] options = split.Skip(9).ToArray();

                string[] correctOptions = new string[options.Length];

                for (int i = 0; i < options.Length; i++)
                {
                    correctOptions[i] = options[i].TrimEnd(',');
                    //valveDistance[(name, correctOptions[i])] = 1;
                    //Console.WriteLine($"Added distance {1} from {name} to {correctOptions[i]}");
                }

                valveInfo[name] = (int.Parse(flowrate), correctOptions);
            }

            foreach (string key in valveInfo.Keys)
            {
                FillDistanceDictionary2(key);
            }

            Console.WriteLine(valveDistance.Count);


            //Console.WriteLine(shortestDistanceBetweenTwoPoints("AA", "CC"));


            //List<string> routeExample = new List<string>();

            //Console.WriteLine(calculateScore(routeExample));

            Console.WriteLine(Search());
        }

        public static void FillDistanceDictionary(string start)
        {
            Queue<Valve> queue = new Queue<Valve>();

            queue.Enqueue(new Valve()
            {
                Name = start,
            });

            while(queue.Count > 0)
            {
                Valve currentValve = queue.Dequeue();

                string[] possiblePaths = valveInfo[currentValve.Name].Item2;

                foreach(string possiblePath in possiblePaths)
                {
                    if(possiblePath != currentValve.Name)
                    {
                        //if (!currentValve.previousPath.Exists(valve => valve.Name == possiblePath))
                        {
                            Valve newValve = new Valve()
                            {
                                Name = possiblePath,
                                previousPath = currentValve.previousPath.Append(currentValve).ToList(),
                                //previousPath = currentValve.previousPath.ToList(),
                            };

                            // update previous path values 

                            List<Valve> history = newValve.previousPath;

                            history.Reverse();

                            for (int i = 0; i < history.Count; i++)
                            {
                                string from = history[i].Name;
                                string to = possiblePath;

                                if(from == "BB")
                                {
                                    if(to == "JJ")
                                    {

                                    }
                                }

                                

                                (string, string) key = (from, to);

                                if (valveDistance.ContainsKey(key))
                                {
                                    int notedDistance = valveDistance[key];

                                    if (notedDistance > i + 1)
                                    {
                                        valveDistance[key] = i + 1;
                                        //Console.WriteLine($"Updated distance {i + 2} from {from} to {to}");
                                    }
                                } 
                                else
                                {
                                    if(from != to)
                                    {
                                        valveDistance.Add(key, i + 1);
                                        //Console.WriteLine($"Added distance {i + 2} from {from} to {to}");
                                    }
                                }
                            }

                            if(newValve.previousPath.Distinct().Count() < inputSize)
                            {
                                queue.Enqueue(newValve);
                            }

                            
                        }
                    }
                }
            }
        }

        public static void FillDistanceDictionary2(string start)
        {
            List<string> AllValves = valveInfo.Keys.ToList();

            foreach(string AdjacentValve in AllValves)
            {
                valveDistance.Add((start, AdjacentValve), shortestDistanceBetweenTwoPoints(start, AdjacentValve));
            }
        }

        public static int shortestDistanceBetweenTwoPoints(string from, string to)
        {
            if (from == to)
            {
                return 0;
            }

            Queue<List<string>> queue = new Queue<List<string>>();

            List<string> expanded = new List<string>();
            expanded.Add(from);

            queue.Enqueue(new List<string>() { from });

            while(queue.Count > 0)
            {
                List<string> currentValveList = queue.Dequeue();

                string[] adjacentValves = valveInfo[currentValveList.Last()].Item2;

                foreach(string adjacentValve in adjacentValves)
                {
                    if(adjacentValve == to)
                    {
                        return currentValveList.Count();
                    }
                    else if(!expanded.Contains(adjacentValve))
                    {
                        queue.Enqueue(currentValveList.Append(adjacentValve).ToList());
                        expanded.Add(adjacentValve);
                    }
                }
            }

            Console.WriteLine("error");
            return 10000;
        }

        public static int Search()
        {
            int bestScore = 0;

            Queue<List<string>> queue = new Queue<List<string>>();

            Dictionary<List<string>, bool> visited = new Dictionary<List<string>, bool>();

            List<string> start = new List<string>();

            start.Add("AA");

            queue.Enqueue(start);

            while(queue.Count > 0)
            {
                if(queue.Count % 10000 == 0)
                {
                    Console.WriteLine(queue.Count);
                }
                
                List<string> list = queue.Dequeue();

                // Keys that could be visited
                List<string> remainingValves = valveInfo.Keys.ToList();
                remainingValves.RemoveAll(x => list.Contains(x) || valveInfo[x].Item1 == 0);

                if(remainingValves.Count == 0)
                {
                    int newScore = calculateScore(list);
                    if(newScore > bestScore)
                        bestScore = newScore;
                }
                else
                {
                    foreach (string currentvalve in remainingValves)
                    {
                        List<string> newList = new List<string>();

                        newList.AddRange(list);

                        newList.Add(currentvalve);

                        if (!visited.ContainsKey(newList))
                        {
                            queue.Enqueue(newList);

                            visited.Add(newList, true);
                        }
                    }
                }
            }

            return bestScore;
        }

        public static int calculateScore(List<string> route)
        {
            int currentTime = 0;

            int totalScore = 0;

            List<string> openValves = new List<string>();

            for(int i = 0; i < route.Count(); i++)
            {
                if(i > 0)
                {
                    string from = route[i - 1];
                    string to = route[i];

                    // Travel time to valve
                    currentTime += valveDistance[(from, to)];

                    //Console.WriteLine($"time = {currentTime}");
                    //Console.WriteLine($"Move to = {to}");

                    if(currentTime >= 30)
                    {
                        return totalScore;
                    }

                    // Open valve
                    if(valveInfo[to].Item1 > 0)
                    {
                        currentTime++;
                        openValves.Add(to);
                        totalScore += (30 - currentTime) * valveInfo[to].Item1;
                        //Console.WriteLine($"time = {currentTime}");
                        //Console.WriteLine($"Opened valve = {to}");

                    }
                }
            }

            return totalScore;
        }
    }

    public class Valve
    {
        public string Name;

        public List<Valve> previousPath = new List<Valve>();
    }
}