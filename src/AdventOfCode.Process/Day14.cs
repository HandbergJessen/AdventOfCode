using System.Runtime.CompilerServices;

namespace AdventOfCode.Process;

public class Day14 : IDay
{
    public string PartA(string[] input)
    {
        Rock[,] grid = GenerateGrid(input);

        grid = PlatformTiltUp(grid);

        return TotalLoadY(grid).ToString();
    }

    public string PartB(string[] input)
    {
        Rock[,] grid = GenerateGrid(input);
        List<long> totalLoadsY = new();
        List<long> totalLoadsX = new();

        long totalLoadY = 0;
        long totalLoadX = 0;
        bool alreadyExists = false;
        long endValue = 0;
        do
        {
            if (totalLoadY != 0)
            {
                totalLoadsY.Add(totalLoadY);
                totalLoadsX.Add(totalLoadX);

            }
            grid = PlatformTiltUp(grid);
            grid = PlatformTiltLeft(grid);
            grid = PlatformTiltDown(grid);
            grid = PlatformTiltRight(grid);

            totalLoadY = TotalLoadY(grid);
            totalLoadX = TotalLoadX(grid);

            for (int i = 0; i < totalLoadsX.Count; i++)
            {
                if (totalLoadsY[i] == totalLoadY && totalLoadsX[i] == totalLoadX)
                {
                    endValue = totalLoadsY[(1000000000 - i) % (totalLoadsX.Count - i) + i - 1];

                    alreadyExists = true;
                    break;
                }
            }
        } while (!alreadyExists);

        return endValue.ToString();
    }


    private static Rock[,] GenerateGrid(string[] input)
    {
        Rock[,] grid = new Rock[input[0].Length, input.Length];

        for (int y = 0; y < input.Length; y++)
        {
            string row = input[y];

            for (int x = 0; x < row.Length; x++)
            {
                char rock = row[x];

                if (rock != '.')
                {
                    grid[y, x] = new Rock(x, y, rock);
                }
                else
                {
                    grid[y, x] = new Rock(x, y, ' ');
                }
            }
        }

        return grid;
    }
    private static Rock[,] PlatformTiltUp(Rock[,] grid)
    {

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].Visual == 'O')
                {
                    int newY = y;
                    for (int up = y - 1; up >= 0; up--)
                    {
                        if (grid[up, x].Visual == ' ')
                        {
                            newY = up;
                        }
                        else break;
                    }
                    if (newY != y)
                    {
                        grid[newY, x].Visual = 'O';
                        grid[y, x].Visual = ' ';
                    }
                }
            }
        }

        return grid;
    }
    private static Rock[,] PlatformTiltDown(Rock[,] grid)
    {
        for (int y = grid.GetLength(0) - 1; y >= 0; y--)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].Visual == 'O')
                {
                    int newY = y;
                    for (int down = y + 1; down < grid.GetLength(0); down++)
                    {
                        if (grid[down, x].Visual == ' ')
                        {
                            newY = down;
                        }
                        else break;
                    }
                    if (newY != y)
                    {
                        grid[newY, x].Visual = 'O';
                        grid[y, x].Visual = ' ';
                    }
                }
            }
        }

        return grid;
    }
    private static Rock[,] PlatformTiltLeft(Rock[,] grid)
    {

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].Visual == 'O')
                {
                    int newX = x;
                    for (int right = x - 1; right >= 0; right--)
                    {
                        if (grid[y, right].Visual == ' ')
                        {
                            newX = right;
                        }
                        else break;
                    }
                    if (newX != x)
                    {
                        grid[y, newX].Visual = 'O';
                        grid[y, x].Visual = ' ';
                    }
                }
            }
        }

        return grid;
    }
    private static Rock[,] PlatformTiltRight(Rock[,] grid)
    {

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = grid.GetLength(1) - 1; x >= 0; x--)
            {
                if (grid[y, x].Visual == 'O')
                {
                    int newX = x;

                    for (int left = x + 1; left < grid.GetLength(1); left++)
                    {
                        if (grid[y, left].Visual == ' ')
                        {
                            newX = left;
                        }
                        else break;
                    }
                    if (newX != x)
                    {
                        grid[y, newX].Visual = 'O';
                        grid[y, x].Visual = ' ';
                    }
                }
            }
        }

        return grid;
    }

    private class Rock
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Visual { get; set; }


        public Rock(int x, int y, char visual)
        {
            X = x;
            Y = y;
            Visual = visual;

        }
    }
    private static void Print(Rock[,] grid)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                Console.Write(grid[y, x].Visual);
            }
            Console.WriteLine();
        }
        Console.WriteLine();

    }
    private static long TotalLoadY(Rock[,] grid)
    {
        long totalValue = 0;

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].Visual == 'O')
                {
                    totalValue += grid.GetLength(0) - y;
                }
            }
        }
        return totalValue;
    }
    private static long TotalLoadX(Rock[,] grid)
    {
        long totalValue = 0;

        for (int x = 0; x < grid.GetLength(1); x++)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                if (grid[y, x].Visual == 'O')
                {
                    totalValue += grid.GetLength(1) - x;
                }
            }
        }
        return totalValue;
    }
}