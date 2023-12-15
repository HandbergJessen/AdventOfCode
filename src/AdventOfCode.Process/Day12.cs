namespace AdventOfCode.Process;

public class Day12 : IDay
{
    public string PartA(string[] input)
    {
        int arrangements = 0;
        foreach (string line in input)
        {
            string[] lineDetails = line.Split(' ');

            int single = GetTotalArrangements(lineDetails[0], lineDetails[1]);

            Console.WriteLine($"{lineDetails[0]}, {lineDetails[1]}, {single}");

            arrangements += single;
        }

        return arrangements.ToString();
    }

    public string PartB(string[] input)
    {
        int arrangements = 0;
        foreach (string line in input)
        {
            string[] lineDetails = line.Split(' ');

            int single = GetTotalArrangements(lineDetails[0] + "?" + lineDetails[0] + "?" + lineDetails[0] + "?" + lineDetails[0] + "?" + lineDetails[0], lineDetails[1] + "," + lineDetails[1] + "," + lineDetails[1] + "," + lineDetails[1] + "," + lineDetails[1]);

            Console.WriteLine($"{lineDetails[0] + "?" + lineDetails[0] + "?" + lineDetails[0] + "?" + lineDetails[0] + "?" + lineDetails[0]}, {lineDetails[1] + "," + lineDetails[1] + "," + lineDetails[1] + "," + lineDetails[1] + "," + lineDetails[1]}, {single}");

            arrangements += single;
        }

        return arrangements.ToString();
    }

    private static int GetTotalArrangements(string condition, string groupCollection)
    {
        List<int> groups = Utilities.LineToInts(groupCollection);

        int currentDamaged = condition.Split('#').Length - 1;
        int totalDamaged = 0;

        foreach (int group in groups)
        {
            totalDamaged += group;
        }

        if (currentDamaged == totalDamaged)
        {
            return 1;
        }

        List<string> arrangements = GetArrangements(condition, groups, currentDamaged, totalDamaged, 0);

        return arrangements.Count;
    }

    private static List<string> GetArrangements(string condition, List<int> groups, int currentDamaged, int totalDamaged, int index)
    {
        List<string> arrangements = new();

        for (int i = index; i < condition.Length; i++)
        {
            if (condition[i] == '?')
            {
                string newCondition = ReplaceAt(condition, i, '#');

                if (currentDamaged + 1 >= totalDamaged)
                {
                    newCondition = newCondition.Replace('?', '.');

                    if (!IsValidCondition(newCondition, groups))
                    {
                        condition = ReplaceAt(condition, i, '.');
                        continue;
                    }

                    arrangements.Add(newCondition);
                }
                else
                {
                    if (!IsValidCondition(newCondition, groups))
                    {
                        condition = ReplaceAt(condition, i, '.');
                        continue;
                    }

                    arrangements.AddRange(GetArrangements(newCondition, groups, currentDamaged + 1, totalDamaged, i + 1));
                }

                condition = ReplaceAt(condition, i, '.');
            }
        }

        return arrangements;
    }

    private static bool IsValidCondition(string condition, List<int> groups)
    {
        List<int> conditionGroups = new();

        int value = 0;
        bool done = true;
        foreach (char character in condition)
        {
            if (character == '.')
            {
                if (value > 0)
                {
                    conditionGroups.Add(value);
                    value = 0;
                }
            }
            else if (character == '#')
            {
                value++;
            }
            else
            {
                if (value > 0)
                {
                    conditionGroups.Add(value);
                    value = 0;
                }

                done = false;
                break;
            }
        }

        if (done)
        {
            if (value > 0)
            {
                conditionGroups.Add(value);
            }

            if (conditionGroups.Count != groups.Count)
            {
                return false;
            }

            for (int i = 0; i < conditionGroups.Count; i++)
            {
                if (conditionGroups[i] != groups[i])
                {
                    return false;
                }
            }
        }
        else
        {
            if (conditionGroups.Count > groups.Count)
            {
                return false;
            }

            for (int i = 0; i < conditionGroups.Count; i++)
            {
                if (conditionGroups[i] > groups[i])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static string ReplaceAt(string input, int index, char newChar)
    {
        char[] chars = input.ToCharArray();
        chars[index] = newChar;
        return new string(chars);
    }
}