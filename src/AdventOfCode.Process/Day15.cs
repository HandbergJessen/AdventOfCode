namespace AdventOfCode.Process;

public class Day15 : IDay
{
    public string PartA(string[] input)
    {

        string[] sequences = input[0].Split(',');
        long focusPower = 0;
        foreach (string sequence in sequences)
        {
            int currentValue = 0;
            char[] details = sequence.ToCharArray();
            foreach (char detail in details)
            {
                currentValue += detail;
                currentValue = GetHASHValue(currentValue);

            }
            focusPower += currentValue;
        }

        return focusPower.ToString();
    }

    public string PartB(string[] input)
    {
        string[] sequences = input[0].Split(',');
        long focusPower = 0;

        IDictionary<int, List<string>> boxes = new Dictionary<int, List<string>>();
        IDictionary<string, int> focalLengths = new Dictionary<string, int>();
        foreach (string sequence in sequences)
        {
            string[] lenseDetails = sequence.Split('-', '=');

            _ = int.TryParse(lenseDetails[1], out int focalLenght);
            string lenseId = lenseDetails[0];

            focalLengths[lenseId] = focalLenght;

            int box = 0;
            char[] details = lenseDetails[0].ToCharArray();
            foreach (char detail in details)
            {
                box += detail;
                box = GetHASHValue(box);
            }

            if (!boxes.ContainsKey(box))
            {
                boxes[box] = new();
            }

            if (RemoveLens(sequence))
            {
                boxes[box].Remove(lenseId);
            }
            else
            {
                if (!boxes[box].Contains(lenseId))
                {
                    boxes[box].Add(lenseId);
                }
            }
        }

        foreach (int boxer in boxes.Keys)
        {
            List<string> lensIds = boxes[boxer];
            for (int i = 0; i < lensIds.Count; i++)
            {
                int focalLength = focalLengths[lensIds[i]];

                focusPower += (boxer + 1) * focalLength * (i + 1);
            }
        }

        return focusPower.ToString();
    }

    private static bool RemoveLens(string operation)
    {
        if (operation.Contains('-')) return true;
        else return false;
    }
    private static int GetHASHValue(int currentValue)
    {
        currentValue *= 17;
        currentValue %= 256;

        return currentValue;
    }
}