namespace AdventOfCode.Process;

public class Day6 : IDay
{
    public string PartA(string[] input)
    {
        List<int> times = Utilities.LineToInts(input[0]);
        List<int> distances = Utilities.LineToInts(input[1]);

        int product = 1;
        for (int i = 0; i < times.Count; i++)
        {
            product *= CalculateWinningCombinations(times[i], distances[i]);
        }

        return product.ToString();
    }

    public string PartB(string[] input)
    {
        long time = Utilities.LineToLong(input[0]);
        long distance = Utilities.LineToLong(input[1]);

        return CalculateWinningCombinations(time, distance).ToString();
    }

    private static int CalculateWinningCombinations(long time, long distance)
    {
        int min = (int)Math.Floor((time - Math.Sqrt(time * time - 4 * distance)) / 2 + 1);
        int max = (int)Math.Ceiling((time + Math.Sqrt(time * time - 4 * distance)) / 2 - 1);

        return max - min + 1;
    }
}