namespace AdventOfCode.Process;

public class Challenge1 : IChallenge
{
    public string PartA(string[] input)
    {
        int sum = 0;

        foreach (string line in input)
        {
            sum += GetValue(line);
        }

        return sum.ToString();
    }

    public string PartB(string[] input)
    {
        int sum = 0;

        foreach (string line in input)
        {
            sum += GetValue(Convert(line));
        }

        return sum.ToString();
    }

    private static int GetValue(string line)
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

    private static string Convert(string line)
    {
        return line.Replace("one", "o1e")
                   .Replace("two", "t2o")
                   .Replace("three", "t3e")
                   .Replace("four", "4")
                   .Replace("five", "5e")
                   .Replace("six", "6")
                   .Replace("seven", "7n")
                   .Replace("eight", "e8t")
                   .Replace("nine", "n9e");
    }
}