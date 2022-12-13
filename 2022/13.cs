using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day13
    {
        public static bool useTestInput = false;

        public static void Run()
        {
            List<string> input = Program.GetInput(13, useTestInput);

            Part2(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            List<int> correctIndices = new List<int>();

            int pairIndex = 1;

            for(int index = 0; index < input.Count(); index++)
            {
                if(index % 3 == 0)
                {

                    if(pairIndex == 6)
                    {
                        
                    }

                    Console.WriteLine($"== Pair {pairIndex} ==");
                    

                    string left = input[index];
                    string right = input[index + 1];

                    Console.WriteLine($"- Compare {left} vs {right}");

                    List<PacketData> leftPacketList = ParseStringToPacketData(left);
                    List<PacketData> rightPacketList = ParseStringToPacketData(right);

                    

                    bool correct = checkPair(leftPacketList, rightPacketList);

                    if (correct)
                    {
                        Console.WriteLine($"Added {pairIndex} to sum");
                        correctIndices.Add(pairIndex);
                    }
                    pairIndex++;
                    Console.WriteLine("");
                }
            }

            Console.WriteLine(correctIndices.Sum());
        }

        public static void Part2(List<string> input)
        {
            input.Add("[[2]]");
            input.Add("[[6]]");

            List<PacketDataList> unordered = new List<PacketDataList>();

            foreach(string line in input)
            {
                if(line != "")
                {
                    List<PacketData>ListOfPacketData = ParseStringToPacketData(line);

                    PacketDataList packetDataList = new PacketDataList()
                    {
                        list = ListOfPacketData,
                    };

                    unordered.Add(packetDataList);
                }
            }

            unordered.Sort();

            foreach (PacketDataList packetDataList in unordered)
            {
                Console.WriteLine(GetPacketDataListString(packetDataList));
            }

            int firstIndex = unordered.FindIndex(x => GetPacketDataListString(x) == "[[2,],]") + 1;
            int secondIndex = unordered.FindIndex(x => GetPacketDataListString(x) == "[[6,],]") + 1;

            Console.WriteLine($"Answer = {firstIndex * secondIndex}");
        }

        public static bool checkPair(List<PacketData> leftPacket, List<PacketData> rightPacket)
        {
            for(int i = 0; i < leftPacket.Count; i++)
            {
                if(i >= rightPacket.Count)
                {
                    return false;
                }

                var result = CheckTwoValues(leftPacket[i], rightPacket[i]);

                if (result == null)
                {
                    Console.WriteLine("   - Left side is equal to right side, continue with input");
                    continue;
                } else if ((bool)result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            Console.WriteLine("left side ran out of items, so inputs are in the correct order");
            return true;
        }

        public static bool? CheckTwoValues(PacketData left, PacketData right)
        {
            if (left.GetType() == typeof(PacketDataInt) && right.GetType() == typeof(PacketDataInt))
            {
                Console.WriteLine($"- Compare {((PacketDataInt)left).value} vs {((PacketDataInt)right).value}");

                int leftValue = ((PacketDataInt)left).value;
                int rightValue = ((PacketDataInt)right).value;

                if (leftValue < rightValue)
                {
                    Console.WriteLine("   - Left side is smaller, so inputs are in the right order!");
                    return true;
                } 
                else if(leftValue > rightValue)
                {
                    Console.WriteLine("   - Left side is larger, so inputs are not the right order!");
                    return false;
                }
                else
                {
                    Console.WriteLine("   - Left side is equal to right side, continue with input");
                    return null;
                }
            }

            if (left.GetType() == typeof(PacketDataList) && right.GetType() == typeof(PacketDataList))
            {
                PacketDataList leftPacketList = (PacketDataList)left;
                PacketDataList rightPacketList = (PacketDataList)right;

                Console.WriteLine($"- compare {GetPacketDataListString(leftPacketList)} vs {GetPacketDataListString(rightPacketList)}");


                for (int index = 0; index < leftPacketList.list.Count; index++)
                {
                    if (index >= rightPacketList.list.Count)
                    {
                        Console.WriteLine("Right side ran out of items, so inputs are not in the correct order");
                        return false;
                    }

                    var result = CheckTwoValues(leftPacketList.list[index], rightPacketList.list[index]);

                    if (result == null)
                    {
                        continue;
                    }
                    else if ((bool)result)
                    {
                        return true;
                    }
                    else if (!(bool)result)
                    {
                        return false;
                    }
                }

                if(leftPacketList.list.Count == 0)
                {

                }

                if(leftPacketList.list.Count < rightPacketList.list.Count)
                {
                    Console.WriteLine("Left side ran out of items, so inputs are in the correct order");
                    return true;
                }


            }

            if (left.GetType() == typeof(PacketDataList) && right.GetType() == typeof(PacketDataInt))
            {
                PacketDataList leftPacketList = (PacketDataList)left;
                PacketDataInt rightPacketInt = (PacketDataInt)right;

                PacketDataList rightPacketList = new PacketDataList()
                {
                    list = new List<PacketData>()
                    {
                        rightPacketInt,
                    }
                };

                Console.WriteLine($"- compare {GetPacketDataListString(leftPacketList)} vs {rightPacketInt.value}");

                return CheckTwoValues(leftPacketList, rightPacketList);
            }

            if (left.GetType() == typeof(PacketDataInt) && right.GetType() == typeof(PacketDataList))
            {
                PacketDataInt leftPacketInt = (PacketDataInt)left;
                PacketDataList rightPacketList = (PacketDataList)right;

                PacketDataList leftPacketList = new PacketDataList()
                {
                    list = new List<PacketData>()
                    {
                        leftPacketInt,
                    }
                };

                Console.WriteLine($"- compare {leftPacketInt.value} vs {GetPacketDataListString(rightPacketList)}");

                return CheckTwoValues(leftPacketList, rightPacketList);
                
            }
            //Console.WriteLine("Correct!");
            return null;
        }

        public static List<PacketData> ParseStringToPacketData(string input)
        {
            List<PacketData> output = new List<PacketData>();

            input = input.Remove(0, 1);
            input = input.Substring(0, input.Length - 1);

            for(int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]))
                {
                    int currentInt;

                    if (input[i] == '1' && i + 1 < input.Length)
                    {
                        if (input[i + 1] == '0')
                        {
                            currentInt = 10;
                        }
                        else
                        {
                            currentInt = 1;
                        }
                    }
                    else
                    {
                        currentInt = int.Parse(input[i].ToString());
                    }

                    PacketData result = new PacketDataInt()
                    {
                        value = currentInt,
                    };

                    output.Add(result);

                }
                else if (input[i] == '[')
                {
                    int amountOpenBrackets = 0;
                    int amountCloseBrackets = 0;

                    string list = "";

                    bool startLoop = true;

                    while (amountOpenBrackets > amountCloseBrackets || startLoop == true)
                    {
                        startLoop = false;

                        if(input[i] == '[')
                        {
                            amountOpenBrackets++;
                        }
                        else if(input[i] == ']')
                        {
                            amountCloseBrackets++;
                        }

                        

                        list += input[i];
                        if(i + 1 < input.Length) 
                            i++;
                    }

                    List<PacketData> result = ParseStringToPacketData(list);

                    PacketData packetResult = new PacketDataList()
                    {
                        list = result,
                    };

                    output.Add(packetResult);
                }
            }

            return output;
        }

        public static string GetPacketDataListString(PacketDataList list)
        {
            string output = "";

            output += "[";
            foreach (PacketData data in list.list)
            {
                if (data.GetType() == typeof(PacketDataList))
                {
                    output += GetPacketDataListString((PacketDataList)data);
                }
                else if (data.GetType() == typeof(PacketDataInt))
                {
                    output += ((PacketDataInt)data).value.ToString();
                }
                output += ",";
            }
            output += "]";

            return output;
        }
    }

    public class PacketData
    {
        
    }

    public class PacketDataInt : PacketData
    {
        public int value;

    }

    public class PacketDataList : PacketData, IComparable<PacketDataList>
    {
        public List<PacketData> list = new List<PacketData>();

        public int CompareTo(PacketDataList packet)
        {
            var result = Day13.CheckTwoValues(this, packet);

            if (result == null)
            {
                return 0;
            }
            else if ((bool)result == true)
            {
                return -1;
            }
            else
            {
                return 1;
            }

        }
    }
}
