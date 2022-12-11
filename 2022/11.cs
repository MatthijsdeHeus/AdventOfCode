using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigMath;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day11
    {
        public static bool useTestInput = true;

        public static void Run()
        {
            List<string> input = Program.GetInput(11, useTestInput);

            Part1(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            List<Monkey> monkeys = new List<Monkey>();

            for(int i = 0; i < input.Count; i++)
            {
                if (input[i].Split(' ')[0] == "Monkey")
                {
                    // set id
                    int monkeyId = monkeys.Count();

                    monkeys.Add(new Monkey()
                    {
                        Id = monkeyId,
                    });

                    // set items
                    string itemsLine = input[i + 1];
                    string itemsListString = itemsLine.Remove(0, 17);

                    string[] itemsSplit = itemsListString.Split(',');

                    

                    foreach(string stringItem in itemsSplit)
                    {
                        string trimmedItem = stringItem.Trim();
                        int item = int.Parse(trimmedItem);
                        monkeys[monkeyId].ItemQueue.Enqueue(item);
                    }
                }
            }

            // set next and prev
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Split(' ')[0] == "Monkey")
                {
                    string split = input[i].Split(' ')[1].Remove(1,1);
                    int currentId = int.Parse(split);

                    string[] nextString = input[i + 4].Split(' ');
                    string[] prevString = input[i + 5].Split(' ');
                    

                    string prevId = prevString[9];
                    string nextId = nextString[9];

                    
                    monkeys[currentId].prev = monkeys[int.Parse(prevId)];
                    monkeys[currentId].next = monkeys[int.Parse(nextId)];
                }
            }

            //monkeys[1].ItemQueue.Enqueue(54);

            for(int round = 1; round <= 10000; round++)
            {
                foreach(Monkey monkey in monkeys)
                {
                    monkey.ThrowItems();
                }

                Console.WriteLine("After round " + round + ":");

                foreach(Monkey monkey in monkeys)
                {
                    Console.Write("Monkey " + monkey.Id + ": ");

                    foreach(var item in monkey.ItemQueue)
                    {
                        Console.Write(item + ", ");
                    }
                    Console.WriteLine("");
                }

                Console.WriteLine("");
                    /*if (round == 1 || round == 20 || round % 100 == 0)
                    {
                        Console.WriteLine("== After round " + round + " ==");
                        foreach (Monkey monkey in monkeys)
                        {

                            Console.WriteLine("Monkey " + monkey.Id + " inspected items " + monkey.totalInspectionCount + " times.");
                        }
                        Console.WriteLine("");
                    }*/
                }

      
        }



        public static void Part2(List<string> input)
        {

        }
    }

    public class Monkey
    {
        public int Id;

        public Queue<BigInteger> ItemQueue = new Queue<BigInteger>();

        public Monkey prev; // if operation == false
        public Monkey next; // if operation == true

        public int totalInspectionCount = 0;

        public BigInteger Worry(BigInteger old, bool useTest)
        {
            if (useTest)
            {
                switch (Id)
                {
                    case 0:
                        return old * 19;
                    case 1:
                        return old + 6;
                    case 2:
                        return old * old;
                    case 3:
                        return old + 3;

                }
            }
            else
            {
                switch (Id)
                {
                    case 0:
                        return old * 3;
                    case 1:
                        return old + 1;
                    case 2:
                        return old * 13;
                    case 3:
                        return old * old;
                    case 4:
                        return old + 7;
                    case 5:
                        return old + 8;
                    case 6:
                        return old + 4;
                    case 7:
                        return old + 5;
                }
            }
            

            return 0;
            Console.WriteLine("error");
        }

        public (bool, BigInteger) Test(BigInteger worry, bool useTest)
        {
            totalInspectionCount++;

            if (useTest)
            {
                switch (Id)
                {
                    case 0:
                        return (worry % 23 == 0, worry);
                    case 1:
                        return (worry % 19 == 0, worry);
                    case 2:
                        return (worry % 13 == 0, worry);
                    case 3:
                        return (worry % 17 == 0, worry);
                }
            }
            else
            {
                switch (Id)
                {
                    case 0:
                        return (worry % 13 == 0, worry);
                    case 1:
                        return (worry % 3 == 0, worry);
                    case 2:
                        return (worry % 7 == 0, worry);
                    case 3:
                        return (worry % 2 == 0, worry);
                    case 4:
                        return (worry % 19 == 0, worry);
                    case 5:
                        return (worry % 5 == 0, worry);
                    case 6:
                        return (worry % 11 == 0, worry);
                    case 7:
                        return (worry % 17 == 0, worry);
                }
            }
            

            Console.WriteLine("error2");
            return (false, 0);
        }

        public void ThrowItems()
        {
            bool diagnostics = false;

            if(diagnostics)
                Console.WriteLine("Monkey " + Id + ":");



            while (ItemQueue.Count > 0)
            {
                BigInteger ItemToThrow = ItemQueue.Dequeue();

                if (diagnostics)
                    Console.WriteLine("Monkey inspects " + ItemToThrow);

                BigInteger worry = Worry(ItemToThrow, Day11.useTestInput);

                //if (diagnostics)
                //Console.WriteLine("Worry rises to " + worry);

/*               if(worry > 1000000000)
                    Console.WriteLine("=================== max integer ==============");*/
                
                (bool test, BigInteger newWorry) = Test(worry, Day11.useTestInput);
                if (test)
                {
                    if (diagnostics)
                        Console.WriteLine("worry is dividable!");

                    if (diagnostics)
                        Console.WriteLine("Worry divided to " + worry);
                    
                    next.ItemQueue.Enqueue(worry);

                    if (diagnostics)
                        Console.WriteLine("throw to " + prev.Id);
                } else
                {
                    if (diagnostics)
                        Console.WriteLine("worry is not dividable");

                    prev.ItemQueue.Enqueue(newWorry);
                    if (diagnostics)
                        Console.WriteLine("throw to " + next.Id);
                }
                if (diagnostics)
                    Console.WriteLine("");
            }
        }
    }
}
