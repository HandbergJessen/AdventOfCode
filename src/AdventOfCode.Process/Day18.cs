using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Process;

public class Day18 : IDay
{
    public string PartA(string[] input)
    {

        var (directions, lenghts, codes) = GenerateDigPlan(input);

        List<Cube> cubes = GenerateDigRoute(directions, lenghts, 'A');

        var (xMax, yMax) = GetBoundaries(cubes);

        Cube[,] allCubes = SetupAllCubes(xMax, yMax, cubes);
        //PrintCubes(allCubes);
        return CountDigCubes(allCubes).ToString();

    }

    public string PartB(string[] input)
    {
        var (directions, lenghts, codes) = GenerateDigPlanB(input);
        List<Cube> cubes = GenerateDigRoute(directions, lenghts, 'B');
        var (xMax, yMax) = GetBoundaries(cubes);
        Console.WriteLine($"{xMax} {yMax} {cubes.Count}");
        //Cube[,] allCubes = SetupAllCubes(xMax, yMax, cubes);
        return " ";
        //return CountDigCubes(allCubes).ToString();
    }

    private static Cube[,] SetupAllCubes(long xMax, long yMax, List<Cube> cubes)
    {

        Cube[,] allCubes = new Cube[xMax + 1, yMax + 1];

        for (long y = 0; y < allCubes.GetLength(1); y++)
        {
            for (long x = 0; x < allCubes.GetLength(0); x++)
            {
                allCubes[x, y] = new Cube(x, y, '.');
            }
        }
        foreach (Cube cube in cubes)
        {
            allCubes[cube.X, cube.Y] = cube;
        }

        SetCubesOutSide(allCubes);

        return allCubes;
    }

    private static void SetCubesOutSide(Cube[,] allCubes)
    {
        for (long x = 0; x < allCubes.GetLength(0); x++)
        {
            for (long y = 0; y < allCubes.GetLength(1); y++)
            {
                if ((x == 0 || y == 0 || x == allCubes.GetLength(0) - 1 || y == allCubes.GetLength(1) - 1) && allCubes[x, y].Visual == '.')
                {
                    SetNeighborOutside(allCubes, x, y);
                    continue;
                }
            }
        }
    }
    private static void SetNeighborOutside(Cube[,] allCubes, long x, long y)
    {
        if (x < 0 || x > allCubes.GetLength(0) - 1 || y < 0 || y > allCubes.GetLength(1) - 1)
        {
            return;
        }
        if (allCubes[x, y].Visual != '.')
        {
            return;
        }
        else
        {
            allCubes[x, y].Visual = 'O';
            SetNeighborOutside(allCubes, x - 1, y);
            SetNeighborOutside(allCubes, x + 1, y);
            SetNeighborOutside(allCubes, x, y - 1);
            SetNeighborOutside(allCubes, x, y + 1);
        }
    }
    private static List<Cube> GenerateDigRoute(List<char> directions, List<int> lenghts, char part)
    {
        List<Cube> cube = new();

        int xValue = 0;
        int yValue = 0;

        cube.Add(new Cube(xValue, yValue, '#'));
        for (int i = 0; i < lenghts.Count; i++)
        {
            if (part == 'A')
            {
                for (int j = 1; j <= lenghts[i]; j++)
                {
                    switch (directions[i])
                    {
                        case 'U': yValue--; break;
                        case 'D': yValue++; break;
                        case 'L': xValue--; break;
                        case 'R': xValue++; break;
                        default: break;
                    }
                    cube.Add(new Cube(xValue, yValue, '#'));
                }
            }
            else if (part == 'B')
            {
                switch (directions[i])
                {
                    case 'U': yValue -= lenghts[i]; break;
                    case 'D': yValue += lenghts[i]; break;
                    case 'L': xValue -= lenghts[i]; break;
                    case 'R': xValue += lenghts[i]; break;
                    default: break;
                }
                cube.Add(new Cube(xValue, yValue, '#'));
            }
        }

        return cube;
    }

    private class Cube
    {
        public long X { get; set; }
        public long Y { get; set; }
        public char Visual { get; set; }
        public bool Ckecked { get; set; }

        public Cube(long x, long y, char visual)
        {
            X = x;
            Y = y;
            Visual = visual;
            Ckecked = false;
        }
    }

    private static (long, long) GetBoundaries(List<Cube> cubes)
    {
        long xMin = 0;
        long xMax = 0;
        long yMin = 0;
        long yMax = 0;

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
    public static (List<char>, List<int>, List<string>) GenerateDigPlanB(string[] data)
    {

        List<char> directions = new();
        List<int> lengths = new();
        List<string> codes = new();

        foreach (string row in data)
        {

            string[] details = row.Split('#', ')');
            char[] digits = details[1].ToCharArray();
            switch (digits[5])
            {
                case '0': directions.Add('R'); break;
                case '1': directions.Add('D'); break;
                case '2': directions.Add('L'); break;
                case '3': directions.Add('U'); break;
                default: break;
            }
            string hexValue = "";
            for (int i = 0; i < digits.Length - 1; i++)
            {
                hexValue += digits[i];
            }

            lengths.Add(Convert.ToInt32(hexValue, 16));

            codes.Add(details[1]);

        }
        return (directions, lengths, codes);
    }

    private static long CountDigCubes(Cube[,] allCubes)
    {
        long countDig = 0;
        for (long y = 0; y < allCubes.GetLength(1); y++)
        {
            for (long x = 0; x < allCubes.GetLength(0); x++)
            {
                if (allCubes[x, y].Visual != 'O')
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