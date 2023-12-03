namespace AdventOfCode.Process;

public class Challenge2 : IChallenge
{
    public string PartA(string[] input)
    {
        int value = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (ValidGame(input[i]))
            {
                value += i + 1;
            }
        }

        return value.ToString();
    }

    public string PartB(string[] input)
    {
        int value = 0;

        foreach (string game in input)
        {
            value += Power(game);
        }

        return value.ToString();
    }

    private static bool ValidGame(string game)
    {
        string[] rounds = game.Split(new char[] { ':', ';', ',' });

        for (int i = 1; i < rounds.Length; i++)
        {
            if (!ValidRound(rounds[i]))
            {
                return false;
            }
        }

        return true;
    }

    private static bool ValidRound(string round)
    {
        string[] roundDetails = round.Split(' ');

        int value = int.Parse(roundDetails[1]);
        string color = roundDetails[2];

        switch (color, value)
        {
            case ("red", > 12):
                return false;
            case ("green", > 13):
                return false;
            case ("blue", > 14):
                return false;
            default:
                return true;
        }
    }

    private static int Power(string game)
    {
        string[] cube = game.Split(new char[] { ':', ';', ',' });

        int red = 0;
        int green = 0;
        int blue = 0;

        for (int i = 1; i < cube.Length; i++)
        {
            string[] cubeDetails = cube[i].Split(' ');

            int value = int.Parse(cubeDetails[1]);
            string color = cubeDetails[2];

            if (color.Contains("red") && value > red)
            {
                red = value;
            }
            else if (color.Contains("green") && value > green)
            {
                green = value;
            }
            else if (color.Contains("blue") && value > blue)
            {
                blue = value;
            }
        }

        return red * green * blue;
    }
}