namespace AdventOfCode.Process;

public class Day4 : IDay
{
    public string PartA(string[] input)
    {
        int sum = 0;

        foreach (string line in input)
        {
            sum += GetPoints(line);
        }

        return sum.ToString();
    }

    public string PartB(string[] input)
    {
        Dictionary<int, int> cards = new();

        for (int i = 0; i < input.Length; i++)
        {
            cards.Add(i, 1);
        }

        for (int i = 0; i < input.Length; i++)
        {
            int ocurrences = GetOcurrences(input[i]);
            int count = cards[i];

            for (int j = i + 1; j <= i + ocurrences; j++)
            {
                int cardAmount = cards[j];

                cards[j] = cardAmount + count;
            }
        }

        int sum = 0;

        foreach (int value in cards.Values)
        {
            sum += value;
        }

        return sum.ToString();
    }

    private static int GetPoints(string line)
    {
        int winningOccurences = GetOcurrences(line);
        if (winningOccurences == 0)
        {
            return winningOccurences;
        }
        else
        {
            return (int)Math.Pow(2, winningOccurences - 1);
        }
    }

    private static int GetOcurrences(string line)
    {
        string[] lineParts = line.Split(':', '|');

        HashSet<int> winningNumbers = GetNumbers(lineParts[1]);
        HashSet<int> myNumbers = GetNumbers(lineParts[2]);

        return myNumbers.Intersect(winningNumbers).Count();
    }

    private static HashSet<int> GetNumbers(string line)
    {
        HashSet<int> numbers = new();

        string number = "";
        foreach (char character in line.ToCharArray())
        {
            if (char.IsNumber(character))
            {
                number += character;
            }
            else
            {
                if (!number.Equals(""))
                {
                    numbers.Add(int.Parse(number));
                    number = "";
                }
            }
        }

        if (!number.Equals(""))
        {
            numbers.Add(int.Parse(number));
        }

        return numbers;
    }
}