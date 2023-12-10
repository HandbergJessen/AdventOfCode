namespace AdventOfCode.Process;

public class Utilities
{

    public static char[][] GenerateGrid(string[] data)
    {
        char[][] grid = new char[data.Length][];

        for (int y = 0; y < data.Length; y++)
        {
            grid[y] = data[y].ToCharArray();
        }

        return grid;
    }
    public static List<int> LineToInts(string line)
    {
        List<int> numbers = new();

        foreach (string value in line.Split(" "))
        {
            if (int.TryParse(value, out int number))
            {
                numbers.Add(number);
            }
        }

        return numbers;
    }

    public static int LineToInt(string line)
    {
        string number = "";

        foreach (char character in line.ToCharArray())
        {
            if (char.IsNumber(character))
            {
                number += character;
            }
        }

        return int.Parse(number);
    }

    public static List<long> LineToLongs(string line)
    {
        List<long> numbers = new();

        foreach (string value in line.Split(" "))
        {
            if (long.TryParse(value, out long number))
            {
                numbers.Add(number);
            }
        }

        return numbers;
    }

    public static long LineToLong(string line)
    {
        string number = "";

        foreach (char character in line.ToCharArray())
        {
            if (char.IsNumber(character))
            {
                number += character;
            }
        }

        return long.Parse(number);
    }
}