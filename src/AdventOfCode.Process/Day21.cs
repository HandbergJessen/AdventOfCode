using System.Runtime.InteropServices;

namespace AdventOfCode.Process;

public class Day21 : IDay
{
    public string PartA(string[] input)
    {
        GardenPlot[,] gardenPlots = GenerateGarden(input);

        var (start, steps) = GetValues(input.Length, 'A');
        GardenPlot startPlot = gardenPlots[start, start];

        GetDistances(gardenPlots, startPlot);
        PrintDistances(gardenPlots, steps);

        return CountPlots(gardenPlots, steps).ToString();
    }

    public string PartB(string[] input)
    {
        GardenPlot[,] gardenPlots = GenerateGardenB(input);
        var (start, steps) = GetValues(input.Length, 'B');
        GardenPlot startPlot = gardenPlots[start, start];

        GetDistances(gardenPlots, startPlot);
        //PrintDistances(gardenPlots, steps);
        int[,] blocks = CountPlotsXY(gardenPlots, steps);

        int m = blocks[2, 2];
        int e = blocks[1, 2];
        int t = blocks[2, 0] + blocks[0, 2] + blocks[4, 2] + blocks[2, 4];
        int a = blocks[1, 1] + blocks[1, 3] + blocks[3, 1] + blocks[3, 3];
        int b = blocks[0, 1] + blocks[0, 3] + blocks[4, 1] + blocks[4, 3];
        long n = 26501365 / 131;
        long total = (n - 1) * (n - 1) * m + n * n * e + (n - 1) * a + n * b + t;

        Console.WriteLine(m);
        Console.WriteLine(e);
        Console.WriteLine(t);
        Console.WriteLine(a);
        Console.WriteLine(b);

        return total.ToString();
    }

    private static int[,] CountPlotsXY(GardenPlot[,] gardenPlots, int steps)
    {
        int[,] blocks = new int[5, 5];
        int blockX;
        int blockY;
        for (int y = 0; y < gardenPlots.GetLength(0); y++)
        {
            for (int x = 0; x < gardenPlots.GetLength(1); x++)
            {
                blockX = x / 131;
                blockY = y / 131;
                GardenPlot gardenPlot = gardenPlots[y, x];
                if (gardenPlot.Visual != '#' && gardenPlot.Distance <= steps && gardenPlot.Distance % 2 == 0)
                {
                    blocks[blockY, blockX]++;
                }
            }
        }

        /*for (int y = 0; y < blocks.GetLength(0); y++)
        {
            for (int x = 0; x < blocks.GetLength(1); x++)
            {
                Console.Write($"  {blocks[y, x]}");
            }
            Console.WriteLine();
        }*/

        return blocks;
    }

    public static (int, int) GetValues(int length, char part)
    {

        int start = 0;

        int steps = 0;
        if (part == 'A')
        {
            start = (length - 1) / 2;
            if (length < 131)
                steps = 6;
            else
                steps = 64;
        }
        else
        {
            start = ((length - 1) / 2) + 2 * length;
            if (length < 131)
                steps = 2 * length + ((length - 1) / 2);
            else
                steps = 2 * length + ((length - 1) / 2);
        }

        return (start, steps);
    }
    private static GardenPlot GetStartPlot(GardenPlot[,] gardenPlots)
    {
        foreach (GardenPlot gardenPlot in gardenPlots)
        {
            if (gardenPlot.Visual == 'S')
                return gardenPlot;
        }
        throw new Exception("Could not find start plot");
    }

    private static void GetDistances(GardenPlot[,] gardenPlots, GardenPlot gardenPlot)
    {
        Queue<GardenPlot> q = new();
        gardenPlot.Explored = true;
        gardenPlot.Distance = 0;

        q.Enqueue(gardenPlot);
        while (q.Count != 0)
        {
            GardenPlot currentPlot = q.Dequeue();
            int x = currentPlot.X;
            int y = currentPlot.Y;

            if (x + 1 < gardenPlots.GetLength(1) && gardenPlots[y, x + 1].Visual != '#' && !gardenPlots[y, x + 1].Explored)
            {
                gardenPlots[y, x + 1].Explored = true;
                gardenPlots[y, x + 1].Distance = currentPlot.Distance + 1;
                q.Enqueue(gardenPlots[y, x + 1]);
            }

            if (x - 1 >= 0 && gardenPlots[y, x - 1].Visual != '#' && !gardenPlots[y, x - 1].Explored)
            {
                gardenPlots[y, x - 1].Explored = true;
                gardenPlots[y, x - 1].Distance = currentPlot.Distance + 1;
                q.Enqueue(gardenPlots[y, x - 1]);
            }

            if (y + 1 < gardenPlots.GetLength(0) && gardenPlots[y + 1, x].Visual != '#' && !gardenPlots[y + 1, x].Explored)
            {
                gardenPlots[y + 1, x].Explored = true;
                gardenPlots[y + 1, x].Distance = currentPlot.Distance + 1;
                q.Enqueue(gardenPlots[y + 1, x]);
            }

            if (y - 1 >= 0 && gardenPlots[y - 1, x].Visual != '#' && !gardenPlots[y - 1, x].Explored)
            {
                gardenPlots[y - 1, x].Explored = true;
                gardenPlots[y - 1, x].Distance = currentPlot.Distance + 1;
                q.Enqueue(gardenPlots[y - 1, x]);
            }
        }
    }

    private static GardenPlot[,] GenerateGarden(string[] input)
    {
        GardenPlot[,] gardenplots = new GardenPlot[input[0].Length, input.Length];
        for (int y = 0; y < input.Length; y++)
        {
            string row = input[y];
            for (int x = 0; x < row.Length; x++)
            {
                char plot = row[x];
                gardenplots[y, x] = new GardenPlot(y, x, plot);

            }
        }
        return gardenplots;
    }
    private static GardenPlot[,] GenerateGardenB(string[] input)
    {

        GardenPlot[,] gardenplots = new GardenPlot[input.Length * 5, input.Length * 5];
        for (int y = 0; y < input.Length * 5; y++)
        {
            string row = input[y % input.Length];
            for (int x = 0; x < row.Length * 5; x++)
            {
                char plot = row[x % input.Length];
                gardenplots[y, x] = new GardenPlot(y, x, plot);

            }
        }
        return gardenplots;
    }
    private class GardenPlot
    {
        public int Y { get; private set; }
        public int X { get; private set; }
        public char Visual { get; set; }
        public int Distance { get; set; }
        public bool Explored { get; set; }

        public GardenPlot(int y, int x, char visual)
        {
            Y = y;
            X = x;
            Visual = visual;
            Distance = -1;
            Explored = false;
        }
    }

    private static void PrintGarden(GardenPlot[,] gardenPlots)
    {

        for (int y = 0; y < gardenPlots.GetLength(0); y++)
        {
            for (int x = 0; x < gardenPlots.GetLength(1); x++)
            {
                Console.Write(gardenPlots[y, x].Visual);
            }
            Console.WriteLine();
        }
    }

    private static void PrintDistances(GardenPlot[,] gardenPlots, int steps)
    {

        for (int y = 0; y < gardenPlots.GetLength(0); y++)
        {
            for (int x = 0; x < gardenPlots.GetLength(1); x++)
            {
                if (gardenPlots[y, x].Visual == '#' || gardenPlots[y, x].Distance > steps || gardenPlots[y, x].Distance % 2 != 0)
                {
                    Console.Write(gardenPlots[y, x].Visual);
                }
                else
                {
                    Console.Write(gardenPlots[y, x].Distance % 10);

                }

            }
            Console.WriteLine();
        }
    }
    private static int CountPlots(GardenPlot[,] gardenPlots, int steps)
    {
        int countPlots = 0;
        foreach (GardenPlot gardenPlot in gardenPlots)
        {
            if (gardenPlot.Visual != '#' && gardenPlot.Distance <= steps && gardenPlot.Distance % 2 == 0)
            {
                countPlots++;
            }
        }
        return countPlots;
    }
}