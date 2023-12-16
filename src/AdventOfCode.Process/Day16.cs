using System.Collections;

namespace AdventOfCode.Process;

public class Day16 : IDay
{
    public string PartA(string[] input)
    {
        Tile[,] contraption = GenerateGrid(input);
        StartBeaming(contraption, 0, 0, Direction.Right);

        //Print(contraption);

        return TotalEnergized(contraption).ToString();
    }

    public string PartB(string[] input)
    {
        long endValue = 0;
        Tile[,] contraption = GenerateGrid(input);

        for (int y = 0; y < contraption.GetLength(0); y++)
        {
            StartBeaming(contraption, y, 0, Direction.Right);
            long totalEnergize = TotalEnergized(contraption);
            if (totalEnergize > endValue)
            {
                endValue = totalEnergize;
            }
            StartBeaming(contraption, y, contraption.GetLength(1) - 1, Direction.Left);
            totalEnergize = TotalEnergized(contraption);
            if (totalEnergize > endValue)
            {
                endValue = totalEnergize;
            }
        }
        for (int x = 0; x < contraption.GetLength(1); x++)
        {
            StartBeaming(contraption, 0, x, Direction.Down);
            long totalEnergize = TotalEnergized(contraption);
            if (totalEnergize > endValue)
            {
                endValue = totalEnergize;
            }
            StartBeaming(contraption, contraption.GetLength(0) - 1, x, Direction.Up);
            totalEnergize = TotalEnergized(contraption);
            if (totalEnergize > endValue)
            {
                endValue = totalEnergize;
            }
        }

        return endValue.ToString();
    }

    private static Tile[,] GenerateGrid(string[] input)
    {
        Tile[,] grid = new Tile[input[0].Length, input.Length];

        for (int y = 0; y < input.Length; y++)
        {
            string row = input[y];

            for (int x = 0; x < row.Length; x++)
            {
                char tile = row[x];

                if (tile != '.')
                {
                    grid[y, x] = new Tile(y, x, tile);
                }
                else
                {
                    grid[y, x] = new Tile(y, x, ' ');
                }
            }
        }

        return grid;
    }
    private static void StartBeaming(Tile[,] grid, int y, int x, Direction inDirection)
    {
        if (y < 0 || y >= grid.GetLength(0) || x < 0 || x >= grid.GetLength(1)) return;

        Tile tile = grid[y, x];

        if (tile.IsDirectionUsed(inDirection)) return;
        tile.Energized = true;

        switch (tile.Visual, inDirection)
        {
            case ('-', Direction.Up):
                StartBeaming(grid, y, x - 1, Direction.Left);
                StartBeaming(grid, y, x + 1, Direction.Right);
                break;
            case ('-', Direction.Down):
                StartBeaming(grid, y, x - 1, Direction.Left);
                StartBeaming(grid, y, x + 1, Direction.Right);
                break;
            case ('|', Direction.Left):
                StartBeaming(grid, y - 1, x, Direction.Up);
                StartBeaming(grid, y + 1, x, Direction.Down);
                break;
            case ('|', Direction.Right):
                StartBeaming(grid, y - 1, x, Direction.Up);
                StartBeaming(grid, y + 1, x, Direction.Down);
                break;
            case ('/', Direction.Up):
                StartBeaming(grid, y, x + 1, Direction.Right);
                break;
            case ('/', Direction.Down):
                StartBeaming(grid, y, x - 1, Direction.Left);
                break;
            case ('/', Direction.Left):
                StartBeaming(grid, y + 1, x, Direction.Down);
                break;
            case ('/', Direction.Right):
                StartBeaming(grid, y - 1, x, Direction.Up);
                break;
            case ('\\', Direction.Up):
                StartBeaming(grid, y, x - 1, Direction.Left);
                break;
            case ('\\', Direction.Down):
                StartBeaming(grid, y, x + 1, Direction.Right);
                break;
            case ('\\', Direction.Left):
                StartBeaming(grid, y - 1, x, Direction.Up);
                break;
            case ('\\', Direction.Right):
                StartBeaming(grid, y + 1, x, Direction.Down);
                break;
            default:
                switch (inDirection)
                {
                    case Direction.Up: StartBeaming(grid, y - 1, x, inDirection); break;
                    case Direction.Down: StartBeaming(grid, y + 1, x, inDirection); break;
                    case Direction.Left: StartBeaming(grid, y, x - 1, inDirection); break;
                    case Direction.Right: StartBeaming(grid, y, x + 1, inDirection); break;
                }
                break;
        }
    }

    private class Tile
    {
        public int Y { get; private set; }
        public int X { get; private set; }
        public char Visual { get; private set; }
        public bool Energized { get; set; }
        public List<Direction> usedDirections;

        public Tile(int y, int x, char visual)
        {
            Y = y;
            X = x;
            Visual = visual;
            Energized = false;
            usedDirections = new();

        }

        public bool IsDirectionUsed(Direction direction)
        {
            if (usedDirections.Contains(direction))
            {
                return true;
            }
            else
            {
                usedDirections.Add(direction);
                return false;
            }
        }
        public void Reset()
        {
            Energized = false;
            usedDirections = new();
        }
    }
    private enum Direction
    {
        Up = 1,
        Down = -1,
        Right = 2,
        Left = -2
    }

    private static long TotalEnergized(Tile[,] grid)
    {
        long totalEnergized = 0;
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].Energized == true)
                {
                    totalEnergized++;
                    grid[y, x].Reset();
                }
            }
        }

        return totalEnergized;
    }
    private static void Print(Tile[,] grid)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (grid[y, x].Energized == true)
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write(grid[y, x].Visual);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
