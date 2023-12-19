using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Process;

public class Day18 : IDay
{
    public string PartA(string[] input)
    {

        var (directions, lenghts, codes) = GenerateDigPlan(input);

        List<Cube> cubes = GenerateDigRoute(directions, lenghts);

        var (xMax, yMax) = GetBoundaries(cubes);

        Cube[,] allCubes = SetupAllCubes(xMax, yMax, cubes);
        PrintCubes(allCubes);
        return CountDigCubes(allCubes).ToString();

    }

    public string PartB(string[] input)
    {
        return "Not finished!";
    }

    private static Cube[,] SetupAllCubes(int xMax, int yMax, List<Cube> cubes)
    {

        Cube[,] allCubes = new Cube[xMax + 1, yMax + 1];

        for (int y = 0; y < allCubes.GetLength(1); y++)
        {
            for (int x = 0; x < allCubes.GetLength(0); x++)
            {
                allCubes[x, y] = new Cube(x, y, '.');
            }
        }
        foreach (Cube cube in cubes)
        {
            allCubes[cube.X, cube.Y] = cube;
        }


        return allCubes;
    }
    private static List<Cube> GenerateDigRoute(List<char> directions, List<int> lenghts)
    {

        List<Cube> cube = new();

        int xValue = 0;
        int yValue = 0;

        cube.Add(new Cube(xValue, yValue, '#'));
        for (int i = 0; i < lenghts.Count; i++)
        {
            //for (int j = 1; j <= lenghts[i]; j++)
            //{
            switch (directions[i])
            {
                case 'U': yValue -= lenghts[i]; break;
                case 'D': yValue += lenghts[i]; break;
                case 'L': xValue -= lenghts[i]; break;
                case 'R': xValue += lenghts[i]; break;
                default: break;
            }
            cube.Add(new Cube(xValue, yValue, '#'));
            // }

        }
        return cube;
    }

    private class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Visual { get; set; }

        public Cube(int x, int y, char visual)
        {
            X = x;
            Y = y;
            Visual = visual;
        }
    }

    private static (int, int) GetBoundaries(List<Cube> cubes)
    {
        int xMin = 0;
        int xMax = 0;
        int yMin = 0;
        int yMax = 0;

        foreach (Cube cube in cubes)
        {
            if (cube.X < xMin) xMin = cube.X;
            if (cube.X > xMax) xMax = cube.X;
            if (cube.Y < yMin) yMin = cube.Y;
            if (cube.Y > yMax) yMax = cube.Y;
        }
        foreach (Cube cube in cubes)
        {
            cube.X -= xMin;
            cube.Y -= yMin;
        }

        xMax -= xMin;
        yMax -= yMin;

        return (xMax, yMax);
    }

    public static (List<char>, List<int>, List<string>) GenerateDigPlan(string[] data)
    {

        List<char> directions = new();
        List<int> lengths = new();
        List<string> codes = new();

        foreach (string row in data)
        {

            string[] details = row.Split(' ');
            char[] dir = details[0].ToCharArray();
            directions.Add(dir[0]);

            lengths.Add(int.Parse(details[1]));

            codes.Add(details[2]);

        }
        return (directions, lengths, codes);
    }
    private static int CountDigCubes(Cube[,] allCubes)
    {
        int countDig = 0;
        for (int y = 0; y < allCubes.GetLength(1); y++)
        {
            for (int x = 0; x < allCubes.GetLength(0); x++)
            {
                if (allCubes[x, y].Visual == '#')
                {
                    countDig++;
                }
            }
        }
        return countDig;
    }
    private static void Print(List<char> dir, List<int> len, List<string> code)
    {
        for (int y = 0; y < dir.Count; y++)
        {
            Console.WriteLine($"{dir[y]} {len[y]} {code[y]}");
        }
    }
    private static void PrintCubes(Cube[,] allCubes)
    {
        for (int y = 0; y < allCubes.GetLength(1); y++)
        {
            for (int x = 0; x < allCubes.GetLength(0); x++)
            {
                Console.Write($"{allCubes[x, y].Visual}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}