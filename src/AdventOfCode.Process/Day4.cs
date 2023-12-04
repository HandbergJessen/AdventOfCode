namespace AdventOfCode.Process;

public class Day4 : IDay
{
    public string PartA(string[] input)
    {
        int sum = 0;

        foreach (string line in input)
        {
            int occurences = GetOcurrences(line);

            if (occurences > 0)
            {
                sum += (int)Math.Pow(2, occurences - 1);
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
            int occurences = GetOcurrences(input[i]);
            int value = cards[i];

            for (int j = i + 1; j <= i + occurences; j++)
            {
                cards[j] += value;
            }

            sum += value;
        }

        return sum.ToString();
    }

    private static int GetOcurrences(string line)
    {
        string[] lineParts = line.Split(':', '|');

        List<int> winningNumbers = GetNumbers(lineParts[1]);
        List<int> myNumbers = GetNumbers(lineParts[2]);

        return myNumbers.Intersect(winningNumbers).Count();
    }

    private static List<int> GetNumbers(string line)
    {
        List<int> numbers = new();

        foreach (string value in line.Split(' '))
        {
            if (int.TryParse(value, out int number))
            {
                numbers.Add(number);
            }
        }

        return numbers;
    }
}