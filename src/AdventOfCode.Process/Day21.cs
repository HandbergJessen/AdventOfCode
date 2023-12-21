using System.Runtime.InteropServices;

namespace AdventOfCode.Process;

public class Day21 : IDay
{
    public string PartA(string[] input)
    {
        // 3818 3644
        GardenPlot[,] gardenPlots = GenerateGarden(input);
        //PrintGarden(gardenPlots);
        GardenPlot gardenPlot = GetStartPlot(gardenPlots);



        GetDistances(gardenPlots, gardenPlot);

        PrintDistances(gardenPlots);

        return CountPlots(gardenPlots).ToString();
    }

    public string PartB(string[] input)
    {
        return "Not finished!";
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

    private static void PrintDistances(GardenPlot[,] gardenPlots)
    {

        for (int y = 0; y < gardenPlots.GetLength(0); y++)
        {
            for (int x = 0; x < gardenPlots.GetLength(1); x++)
            {
                if (gardenPlots[y, x].Visual == '#' || gardenPlots[y, x].Distance > 64 || gardenPlots[y, x].Distance % 2 != 0)
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
    private static int CountPlots(GardenPlot[,] gardenPlots)
    {
        int countPlots = 0;
        foreach (GardenPlot gardenPlot in gardenPlots)
        {
            if (gardenPlot.Visual != '#' && gardenPlot.Distance <= 64 && gardenPlot.Distance % 2 == 0)
            {
                countPlots++;
            }
        }
        return countPlots;
    }
}