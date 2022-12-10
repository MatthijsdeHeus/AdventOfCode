using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day7
    {

        public static int sum = 0;
        public static int sum2 = 0;

        public static void Run()
        {
            List<string> input = Program.GetInput(7, false);

            Node head = new Node()
            {
                Name = "/",
            };

            Node currentTreeItem = head;

            input.RemoveAt(0);

            foreach (var line in input)
            {
                var split = line.Split(' ');

                if (split[0] == "$" && split[1] == "cd" && split[2] != "..")
                {
                    currentTreeItem = (Node)currentTreeItem.SubItems.Find(TreeItem => TreeItem.Name == split[2]);
                }

                if (split[0] == "$" && split[1] == "cd" && split[2] == "..")
                {
                    currentTreeItem = currentTreeItem.Parent;
                }

                if (split[0] != "$")
                {
                    if (split[0] == "dir")
                    {
                        currentTreeItem.SubItems.Add(new Node()
                        {
                            Name = split[1],
                            Parent = currentTreeItem,
                        });
                    }
                    else
                    {
                        currentTreeItem.SubItems.Add(new Leaf()
                        {
                            Name = split[1],
                            Parent = currentTreeItem,
                            Size = Int32.Parse(split[0]),
                        });
                    }
                }
            }

            head.Size = FindSize(head);

            ShowTree(head, 0);
            Console.WriteLine(sum);
            Console.WriteLine(sum2);
            Console.WriteLine(CheckTree(head));
            Console.ReadKey();
        }

        public static int FindSize(TreeItem treeItem)
        {
            int totalSize = 0;

            if (treeItem is Node)
            {
                Node item = (Node)treeItem;

                foreach (var subItem in item.SubItems)
                {
                    int foundSize = FindSize(subItem);

                    treeItem.Size += foundSize;

                    totalSize += foundSize;
                }
                if (totalSize <= 100000)
                {
                    sum += totalSize;
                }

            }
            else
            {
                totalSize += treeItem.Size;
            }

            return totalSize;

        }

        public static void ShowTree(TreeItem treeItem, int indentation)
        {
            string treeindentation = new String('-', indentation);

            //Console.WriteLine(treeindentation + treeItem.Name + " " + treeItem.Size);

            if (treeItem is Node)
            {


                Node item = (Node)treeItem;

                if (item.Size <= 100000)
                    sum2 += item.Size;

                if (item.Size >= 10216456)
                    Console.WriteLine("Found = " + item.Size);

                foreach (var childItem in item.SubItems)
                {
                    ShowTree(childItem, indentation + 1);
                }
            }
        }

        public static bool CheckTree(TreeItem treeItem)
        {
            if (treeItem is Node)
            {
                Node item = (Node)treeItem;

                int sumOfSubItemsItems = 0;

                foreach (TreeItem SubItem in item.SubItems)
                {

                    sumOfSubItemsItems += SubItem.Size;


                    if (CheckTree(SubItem) is false)
                    {
                        return false;
                    }
                }

                return sumOfSubItemsItems == treeItem.Size;
            }

            return true;
        }

        public class TreeItem
        {
            public string Name;

            public Node Parent;

            public int Size;
        }

        public class Node : TreeItem
        {
            public List<TreeItem> SubItems = new List<TreeItem>();
        }

        public class Leaf : TreeItem
        {

        }
    }
}
