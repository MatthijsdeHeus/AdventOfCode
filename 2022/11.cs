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
        public static bool useTestInput = false;

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
                

                //Console.WriteLine("After round " + round + ":");

                foreach(Monkey monkey in monkeys)
                {
                    monkey.ThrowItems();
                }

                if (round == 1 || round == 20 || round == 21 || round == 19 || round % 1000 == 0)
                    Console.WriteLine("== After round " + round + " ==");

                for (int i = 0; i < monkeys.Count(); i++)
                {
                    if (round == 1 || round == 20 || round == 21 || round == 19 || round % 1000 == 0)
                        Console.WriteLine("Monkey " + monkeys[i].Id + " inspected items " + monkeys[i].totalInspectionCount + " times.");
                }

                if (round == 1 || round == 20 || round == 21 || round == 19 || round % 1000 == 0)
                    Console.WriteLine("");

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

        public (bool, BigInteger) Inspect(BigInteger old, bool useTest)
        {
            totalInspectionCount++;

            BigInteger worry;

            if (useTest)
            {
                switch (Id)
                {
                    case 0:
                        worry = ((old * 19)) % 96577;
                        return (worry % 23 == 0, worry);
                    case 1:
                        worry = ((old + 6)) % 96577;
                        return ((worry % 19) == 0, worry);
                    case 2:
                        worry = ((old * old)) % 96577;
                        return ((worry % 13) == 0, worry);
                    case 3:
                        worry = ((old + 3)) % 96577;
                        return ((worry % 17 == 0), worry);

                }
            }
            else
            {
                switch (Id)
                {
                    case 0:
                        worry = ((old * 3) % 9699690);
                        return (worry % 13 == 0, worry);
                    case 1:
                        worry = ((old + 1) % 9699690);
                        return (worry % 3 == 0, worry);
                    case 2:
                        worry = ((old * 13) % 9699690);
                        return (worry % 7 == 0, worry);
                    case 3:
                        worry = ((old * old) % 9699690);
                        return (worry % 2 == 0, worry);
                    case 4:
                        worry = ((old + 7) % 9699690);
                        return (worry % 19 == 0, worry);
                    case 5:
                        worry = ((old + 8) % 9699690);
                        return (worry % 5 == 0, worry);
                    case 6:
                        worry = ((old + 4) % 9699690);
                        return (worry % 11 == 0, worry);
                    case 7:
                        worry = ((old + 5) % 9699690);
                        return (worry % 17 == 0, worry);
                }
            }
            Console.WriteLine("error");

            return (false, 0);
            
        }

        public bool Test(BigInteger worry, bool useTest)
        {
            

            if (useTest)
            {
                switch (Id)
                {
                    case 0:
                        return (worry % 23 == 0);
                    case 1:
                        return (worry % 19 == 0);
                    case 2:
                        return (worry % 13 == 0);
                    case 3:
                        return (worry % 17 == 0);
                }
            }
            else
            {
                switch (Id)
                {
                    case 0:
                        return (worry % 13 == 0);
                    case 1:
                        return (worry % 3 == 0);
                    case 2:
                        return (worry % 7 == 0);
                    case 3:
                        return (worry % 2 == 0);
                    case 4:
                        return (worry % 19 == 0);
                    case 5:
                        return (worry % 5 == 0);
                    case 6:
                        return (worry % 11 == 0);
                    case 7:
                        return (worry % 17 == 0);
                }
            }
            

            Console.WriteLine("error2");
            return false;
        }

        public void ThrowItems()
        {
            

            bool diagnostics = false;

            if(diagnostics)
                Console.WriteLine("Monkey " + Id + ":");



            while (ItemQueue.Any())
            {
                BigInteger ItemToThrow = ItemQueue.Dequeue();

                if (diagnostics)
                    Console.WriteLine("Monkey inspects " + ItemToThrow);

                (bool test, BigInteger worry) = Inspect(ItemToThrow, Day11.useTestInput);

                if(worry < 0)
                    Console.WriteLine("lower than 0");

                //if (diagnostics)
                //Console.WriteLine("Worry rises to " + worry);

               if(worry > 1000000000)
                    Console.WriteLine("=================== max integer ==============");
                
                //bool test = Test(worry, Day11.useTestInput);

                
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

                    prev.ItemQueue.Enqueue(worry);
                    if (diagnostics)
                        Console.WriteLine("throw to " + next.Id);
                }
                if (diagnostics)
                    Console.WriteLine("");
            }
        }
    }
}
