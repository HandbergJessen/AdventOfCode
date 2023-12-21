namespace AdventOfCode.Process;

public class Day13 : IDay
{
    private char part;
    public bool acceptRuleUsed;
    public Day13()
    {
        part = 'A';
        acceptRuleUsed = false;
    }

    public string PartA(string[] input)
    {
        List<int> lenghts = GetLenghts(input);
        List<string[]> pattern = GeneratePattern(input, lenghts);

        //Print(pattern);

        return SummarizePattern(pattern).ToString();
    }

    public string PartB(string[] input)
    {
        part = 'B';
        List<int> lenghts = GetLenghts(input);
        List<string[]> pattern = GeneratePattern(input, lenghts);

        //Print(pattern);
        return SummarizePattern(pattern).ToString();
    }

    private static List<int> GetLenghts(string[] data)
    {
        List<int> lenghts = new();
        int lenght = 0;
        for (int y = 0; y < data.Length; y++)
        {
            if (data[y] == "")
            {
                lenghts.Add(lenght);
                lenght = 0;
            }
            else
            {
                lenght++;
            }
        }
        lenghts.Add(lenght);

        return lenghts;
    }
    private static List<string[]> GeneratePattern(string[] data, List<int> lenghts)
    {
        List<string[]> pattern = new();

        int currentY = 0;
        int numbLenghts = 0;
        string[] yLines = new string[lenghts[numbLenghts]];
        for (int y = 0; y < data.Length; y++)
        {
            if (data[y] == "")
            {
                pattern.Add(yLines);
                currentY = 0;
                numbLenghts++;
                yLines = new string[lenghts[numbLenghts]];
            }
            else
            {
                yLines[currentY] = data[y];
                currentY++;
            }
        }
        pattern.Add(yLines);
        return pattern;
    }

    private int SummarizePattern(List<string[]> pattern)
    {
        int summarizePattern = 0;

        foreach (string[] yStrings in pattern)
        {
            acceptRuleUsed = false;
            int mirrorValue = GetMirrorValue(yStrings) * 100;
            if (mirrorValue == 0)
            {
                string[] xStrings = ConvertToXStrings(yStrings);
                mirrorValue = GetMirrorValue(xStrings);
            }
            summarizePattern += mirrorValue;
        }
        return summarizePattern;
    }

    private int GetMirrorValue(string[] strings)
    {
        for (int i = 0; i < strings.Length - 1; i++)
        {
            if (MirrorFound(strings, i, 0))
            {
                if (acceptRuleUsed)
                {
                    return i + 1;
                }
                else { continue; }
            }
        }

        return 0;
    }
    private bool MirrorFound(string[] strings, int i, int x)
    {
        int lowerI = i - x;
        int upperI = i + 1 + x;
        int next = x + 1;
        if (lowerI < 0 || upperI >= strings.Length)
        {
            return true;
        }
        else if (StringsIsEqual(strings[lowerI], strings[upperI]))
        {
            if (MirrorFound(strings, i, next))
            {
                return true;
            }
            else { return false; }
        }
        else { return false; }
    }
    private bool StringsIsEqual(string lowerString, string upperString)
    {
        if (part != 'B')
        {
            if (lowerString == upperString)
            {
                acceptRuleUsed = true;
                return true;
            }
            else return false;
        }
        else
        {
            char[] lowerChars = lowerString.ToCharArray();
            char[] upperChars = upperString.ToCharArray();
            int numbNotMatch = 0;
            for (int i = 0; i < lowerChars.Length; i++)
            {
                if (lowerChars[i] != upperChars[i])
                {
                    numbNotMatch++;
                }
                if (numbNotMatch > 1)
                {
                    acceptRuleUsed = false;
                    return false;
                }
            }
            if (numbNotMatch == 1)
            {
                if (acceptRuleUsed)
                {
                    return false;
                }
                else
                {
                    acceptRuleUsed = true;
                    return true;
                }
            }
            return true;
        }
    }
    private static string[] ConvertToXStrings(string[] yLines)
    {
        string[] xStrings = new string[yLines[0].Length];
        for (int y = 0; y < yLines.GetLength(0); y++)
        {
            char[] xValues = yLines[y].ToCharArray();
            for (int x = 0; x < xValues.Length; x++)
            {
                xStrings[x] += xValues[x];
            }
        }
        return xStrings;
    }
    private static void Print(List<string[]> pattern)
    {
        foreach (string[] yStrings in pattern)
        {
            for (int y = 0; y < yStrings.GetLength(0); y++)
            {
                Console.WriteLine(yStrings[y]);
            }
            Console.WriteLine();
        }
    }
}