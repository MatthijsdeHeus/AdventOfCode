using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2022
{
    public static class Day12
    {
        public static bool useTestInput = false;

        public static void Run()
        {
            List<string> input = Program.GetInput(12, useTestInput);

            Part2(input);

            Console.ReadKey();
        }

        public static void Part1(List<string> input)
        {
            int[,] grid = new int[input[0].Length, input.Count];
            (int col, int row) start = (-1, -1);
            (int col, int row) end = (-1, -1);



            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Count(); col++)
                {
                    if (input[row][col] == 'S')
                    {
                        start = (col, row);
                        grid[col, row] = 0;
                    } 
                    else if(input[row][col] == 'E')
                    {
                        end = (col, row);
                        grid[col, row] = 27;
                    }
                    else
                    {
                        grid[col, row] = (int)input[row][col] % 32;
                    }
                }
            }

            BFS bfs = new BFS(grid);

            bfs.hasVisited.Add((start.col, start.row), true);

            bfs.queue.Enqueue(new Node()
            {
                x = start.col,
                y = start.row,
            });

            bfs.Start(end.col, end.row);
        }

        public static void Part2(List<string> input)
        {
            int[,] grid = new int[input[0].Length, input.Count];
            (int col, int row) end = (-1, -1);


            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Count(); col++)
                {
                    if (input[row][col] == 'S')
                    {
                        //start = (col, row);
                        grid[col, row] = 0;
                    }
                    else if (input[row][col] == 'E')
                    {
                        end = (col, row);
                        grid[col, row] = 27;
                    }
                    else
                    {
                        grid[col, row] = (int)input[row][col] % 32;
                    }
                }
            }

            int shortestPathLength = int.MaxValue;

            for (int row = 0; row < grid.GetLength(1); row++)
            {
                for (int col = 0; col < grid.GetLength(0); col++)
                {
                    int elevation = grid[col, row];

                    if(elevation == 1)
                    {
                        BFS bfs = new BFS(grid);

                        bfs.hasVisited.Add((col, row), true);

                        bfs.queue.Enqueue(new Node()
                        {
                            x = col,
                            y = row,
                        });

                        int score = bfs.Start(end.col, end.row);
                        if (score < shortestPathLength)
                        {
                            shortestPathLength = score;
                        }
                    }

                    
                }
            }

            Console.WriteLine("best score = " + shortestPathLength);
                    
        }
    }

    public class BFS
    {
        public Queue<Node> queue = new Queue<Node>();

        //List<Node> edges = new List<Node>();

        public Dictionary<(int, int), bool> hasVisited = new Dictionary<(int, int), bool>();

        int[,] grid;

        public BFS(int[,] inputGrid)
        {
            grid = inputGrid;
        }

        public void ExploreEdge(Node node)
        {
            for(int Dcol = -1; Dcol <= 1; Dcol++)
            {
                for (int Drow = -1; Drow <= 1; Drow++)
                {
                    if(Drow == 0 ^ Dcol == 0)
                    {
                        int newRow = node.y + Drow;
                        int newCol = node.x + Dcol;

                        if (newRow >= 0 && newRow < grid.GetLength(1) && newCol >= 0 && newCol < grid.GetLength(0))
                        {
                            if(grid[node.x, node.y] >= grid[newCol, newRow] || grid[node.x, node.y] + 1 == grid[newCol, newRow])
                            {
                                bool alreadyVisited = hasVisited.ContainsKey((newCol, newRow));

                                if (!alreadyVisited)
                                {
                                    // Added new node with
                                    //Console.WriteLine("Added new node with coords (" + newCol + ", " + newRow + ")");

                                    Node newNode = new Node()
                                    {
                                        Parent = node,
                                        x = newCol,
                                        y = newRow,
                                    };

                                    node.Children.Add(newNode);

                                    queue.Enqueue(newNode);
                                }

                                hasVisited[(newCol, newRow)] = true;
                            }
                        }
                    }
                }
            }
        }

        public int Start(int endX, int endY)
        {
            while(queue.Count > 0)
            {
                Node node = queue.Dequeue();

                ExploreEdge(node);

                if(node.x == endX && node.y == endY)
                {

                    int counter = 0;

                    Node currentNode = node;
                    while(currentNode.Parent != null)
                    {
                        counter++;
                        Console.WriteLine(currentNode.x + ", " + currentNode.y);

                        currentNode = currentNode.Parent;
                    }

                    return counter;
                    //Console.WriteLine("found solution = " + counter);
                }
            }

            return int.MaxValue;
        }
    }

    public class Node
    {
        public int x;
        public int y;

        public Node Parent;
        public List<Node> Children = new List<Node>();
    }
}
