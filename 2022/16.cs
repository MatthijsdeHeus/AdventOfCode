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

        public static int amountOfValvesWithFlow = 0;

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
                }

                valveInfo[name] = (int.Parse(flowrate), correctOptions);
            }

            foreach ((int flow, string[]) value in valveInfo.Values)
            {
                if(value.flow > 0)
                {
                    amountOfValvesWithFlow++;
                }
            }


            foreach (string key in valveInfo.Keys)
            {
                FillDistanceDictionary2(key);
            }

            List<string> list = new List<string>();
            list.Add("AA");
            list.Add("JJ");
            list.Add("BB");
            list.Add("CC");

            List<string> elephantList = new List<string>();
            elephantList.Add("AA");
            elephantList.Add("DD");
            elephantList.Add("HH");
            elephantList.Add("EE");

            (List<string> list1, List<string> list2) exampleRoute = (list, elephantList);

            //Console.WriteLine(calculateScore2(exampleRoute.list1, exampleRoute.list2));



            //Console.WriteLine(SearchPart1());
            Console.WriteLine(SearchPart2());
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

        public static int SearchPart1()
        {
            int bestScore = 0;

            Queue<List<string>> queue = new Queue<List<string>>();

            List<string> start = new List<string>();

            start.Add("AA");

            queue.Enqueue(start);

            while(queue.Count > 0)
            {
                List<string> list = queue.Dequeue();
                Console.WriteLine($"Dequeued {string.Join(", ", list)}");

                // Keys that could be visited
                List<string> remainingValves = valveInfo.Keys.ToList();
                remainingValves.RemoveAll(x => list.Contains(x) || valveInfo[x].Item1 == 0);

                foreach (string currentvalve in remainingValves)
                {
                    List<string> newList = new List<string>();

                    newList.AddRange(list);

                    newList.Add(currentvalve);

                    int newScore = calculateScore(newList);

                    if (newScore > bestScore)
                        bestScore = newScore;

                    if(calculateTime(newList) <= 28)
                        queue.Enqueue(newList);
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

        public static int calculateTime(List<string> route)
        {
            int currentTime = 0;

            for (int i = 0; i < route.Count(); i++)
            {
                if (i > 0)
                {
                    string from = route[i - 1];
                    string to = route[i];

                    currentTime += valveDistance[(from, to)];

                    // Open valve
                    if (valveInfo[to].Item1 > 0)
                    {
                        currentTime++;
                    }
                }
            }

            return currentTime;
        }

        public static int SearchPart2()
        {
            int bestScore = 0;

            // define the queues
            Queue<(List<string> humanRoute, List<string> elephantRoute)> Queue = new Queue<(List<string>, List<string>)>();

            // start node
            List<string> HumanStart = new List<string>() { "AA" };
            List<string> ElephantStart = new List<string>() { "AA" };

            Queue.Enqueue((HumanStart, ElephantStart));



            // main bfs loop
            while (Queue.Count() > 0)
            {
                // current routes of human and elephant
                (List<string> humanRoute, List<string> elephantRoute) = Queue.Dequeue();

                // get a list of all valves with pressure which are not in either route
                List<string> valvesWithPressureNotInEitherRoute = valveInfo.Keys.ToList();
                valvesWithPressureNotInEitherRoute.RemoveAll(valveWithMaybePressure => humanRoute.Contains(valveWithMaybePressure) || elephantRoute.Contains(valveWithMaybePressure)  || (valveInfo[valveWithMaybePressure].Item1 == 0));

                // create a route for the human go to every remaining valves
                foreach (string unvisitedValveForHuman in valvesWithPressureNotInEitherRoute)
                {
                    // Create copy of list of valves visited by the human
                    List<string> newHumanList = new List<string>();
                    foreach (string humanClosedValve in humanRoute)
                    {
                        newHumanList.Add(humanClosedValve);
                    }

                    // add unvisited valve in the route for the human
                    newHumanList.Add(unvisitedValveForHuman);

                    // create copy of the valvesWithPressureNotInEitherRoute list except for the one added to the human route
                    List<string> valvesWithNotInEitherRouteNew = valveInfo.Keys.ToList();
                    valvesWithNotInEitherRouteNew.RemoveAll(valveWithMaybePressure => newHumanList.Contains(valveWithMaybePressure) || elephantRoute.Contains(valveWithMaybePressure) || (valveInfo[valveWithMaybePressure].Item1 == 0));

                    // loop over them for the elephant
                    foreach (string unvisitedValveForElephant in valvesWithNotInEitherRouteNew)
                    {
                        // Copy old route of elephant
                        List<string> newElephantList = new List<string>();
                        foreach (string elephantClosedValve in elephantRoute)
                        {
                            newElephantList.Add(elephantClosedValve);
                        }

                        // Add the unvisited valve in the route for the elephant
                        newElephantList.Add(unvisitedValveForElephant);

                        // Check if newly created list is the best yet
                        int newScore = calculateScore2(newHumanList, newElephantList);

                        if (newScore > bestScore)
                        {
                            bestScore = newScore;
                        }

                        // if either the elephant or human have time left enqueue route again and there are more valves to close
                        if ((calculateTime(newHumanList) <= 22 || calculateTime(newElephantList) <= 22) && newElephantList.Count + newHumanList.Count - 2 < amountOfValvesWithFlow)
                        {
                            Console.WriteLine($"Enqueued \n{string.Join(", ", newHumanList)}\n{string.Join(", ", newElephantList)}");
                            Console.WriteLine();
                            Queue.Enqueue((newHumanList, newElephantList));
                        }
                            
                    }

                }
            }

            return bestScore;
        }

        public static int calculateScore2(List<string> route1, List<string> route2)
        {
            int totalScore = 0;

            int currentTime1 = 0;

            List<string> openValves1 = new List<string>();

            for (int i = 0; i < route1.Count(); i++)
            {
                if (i > 0)
                {
                    string from = route1[i - 1];
                    string to = route1[i];

                    // Travel time to valve
                    currentTime1 += valveDistance[(from, to)];

                    //Console.WriteLine($"time = {currentTime}");
                    //Console.WriteLine($"Move to = {to}");

                    if (currentTime1 >= 26)
                    {
                        break;
                    }

                    // Open valve
                    if (valveInfo[to].Item1 > 0)
                    {
                        currentTime1++;
                        openValves1.Add(to);
                        totalScore += (26 - currentTime1) * valveInfo[to].Item1;
                        //Console.WriteLine($"time = {currentTime}");
                        //Console.WriteLine($"Opened valve = {to}");

                    }
                }
            }

            int currentTime2 = 0;

            List<string> openValves2 = new List<string>();

            for (int i = 0; i < route2.Count(); i++)
            {
                if (i > 0 && route2.Count() >= 2)
                {
                    string from = route2[i - 1];
                    string to = route2[i];

                    // Travel time to valve
                    currentTime2 += valveDistance[(from, to)];

                    //Console.WriteLine($"time = {currentTime}");
                    //Console.WriteLine($"Move to = {to}");

                    if (currentTime2 >= 26)
                    {
                        break;
                    }

                    // Open valve
                    if (valveInfo[to].Item1 > 0)
                    {
                        currentTime2++;
                        openValves2.Add(to);
                        totalScore += (26 - currentTime2) * valveInfo[to].Item1;
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