using System.Reflection.Metadata;

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

        foreach (string line in input)
        {
            value += Power(line);
        }

        return value.ToString();
    }

    private int Power(string line)
    {
        int redPower = 0;
        int greenPower = 0;
        int bluePower = 0;
        string[] cube = line.Split(new char[] { ':', ';', ',' });

        // Game X, from before : is skipped by i=1 instead of i=0
        for (int i = 1; i < cube.Length; i++)
        {
            string[] cubeDetails = cube[i].Split(' ');

            if (cubeDetails[2].Contains("red") && int.Parse(cubeDetails[1]) > redPower)
            {
                redPower = int.Parse(cubeDetails[1]);
            }
            else if (cubeDetails[2].Contains("green") && int.Parse(cubeDetails[1]) > greenPower)
            {
                greenPower = int.Parse(cubeDetails[1]);
            }
            else if (cubeDetails[2].Contains("blue") && int.Parse(cubeDetails[1]) > bluePower)
            {
                bluePower = int.Parse(cubeDetails[1]);
            }
        }

        return redPower * greenPower * bluePower;
    }


    private bool ValidGame(string line)
    {
        string[] splitLines = line.Split(new char[] { ':', ';', ',' });

        // Game X, from before : is skipped by i=1 instead of i=0
        for (int i = 1; i < splitLines.Length; i++)
        {
            if (!ValidRound(splitLines[i]))
            {
                return false;
            }
        }

        return true;
    }

    private bool ValidRound(string line)
    {
        string[] splitLine = line.Split(' ');

        if (line.Contains("red") && int.Parse(splitLine[1]) > 12)
        {
            return false;
        }
        else if (line.Contains("green") && int.Parse(splitLine[1]) > 13)
        {
            return false;
        }
        else if (line.Contains("blue") && int.Parse(splitLine[1]) > 14)
        {
            return false;
        }

        return true;
    }


}