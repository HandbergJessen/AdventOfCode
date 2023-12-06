namespace AdventOfCode.Process;

public class Day4 : IDay
{
    public string PartA(string[] input)
    {
        int sum = 0;

        foreach (string line in input)
        {
            int matches = GetMatch(line);

            if (matches > 0)
            {
                sum += (int)Math.Pow(2, matches - 1);
            }
        }

        return sum.ToString();
    }

    public string PartB(string[] input)
    {
        Dictionary<int, int> cards = new();
        for (int i = 0; i < input.Length; i++)
        {
            cards[i] = 1;
        }

        int sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            int matches = GetMatch(input[i]);
            int value = cards[i];

            for (int j = i + 1; j <= i + matches; j++)
            {
                cards[j] += value;
            }

            sum += value;
        }

        return sum.ToString();
    }

    private static int GetMatch(string line)
    {
        string[] lineParts = line.Split(':', '|');

        List<int> winningNumbers = Utilities.LineToInts(lineParts[1]);
        List<int> myNumbers = Utilities.LineToInts(lineParts[2]);

        return myNumbers.Intersect(winningNumbers).Count();
    }
}