using AdventOfCode.Foundation;

namespace AdventOfCode.Process;

public class Challenge1 : IChallenge
{
    public string PartA(string[] input)
    {
        int value = 0;

        foreach (string line in input)
        {
            value += GetValueA(line);
        }

        return value.ToString();
    }

    public string PartB(string[] input)
    {
        int value = 0;

        foreach (string line in input)
        {
            value += GetValueB(line);
        }

        return "Not finished!";
    }

    private static int GetValueA(string line)
    {
        int first = 0;
        int last = 0;

        foreach (char character in line)
        {
            if (int.TryParse(character.ToString(), out int value))
            {
                if (first == 0)
                {
                    first = value;
                }

                last = value;
            }
        }

        return first * 10 + last;
    }

    private static int GetValueB(string line)
    {
        return 0;
    }
}