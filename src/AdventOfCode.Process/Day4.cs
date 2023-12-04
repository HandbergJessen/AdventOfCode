namespace AdventOfCode.Process;

public class Day4 : IDay
{
    public string PartA(string[] input)
    {
        int sum = 0;

        foreach (string line in input)
        {
            sum += GetWorth(line);
        }

        return sum.ToString();
    }

    public string PartB(string[] input)
    {
        return "Not finished!";
    }

    private static int GetWorth(string line)
    {
        string[] lineParts = line.Split(':', '|');

        HashSet<int> winningNumbers = GetNumbers(lineParts[1]);
        HashSet<int> myNumbers = GetNumbers(lineParts[2]);

        int winningOccurences = myNumbers.Intersect(winningNumbers).Count();
        if (winningOccurences == 0)
        {
            return winningOccurences;
        }
        else
        {
            return (int)Math.Pow(2, winningOccurences - 1);
        }
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