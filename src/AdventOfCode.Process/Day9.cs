namespace AdventOfCode.Process;

public class Day9 : IDay
{
    public string PartA(string[] input)
    {

        long totalHistoryValue = 0;
        for (int i = 0; i < input.Length; i++)
        {
            List<long> values = Utilities.LineToLongs(input[i]);
            long[,] report = new long[values.Count, values.Count];
            int colMax = values.Count;
            long historyValue = 0;
            for (int row = 0; row < values.Count; row++)
            {

                bool valueEqual = true;
                for (int col = 0; col < colMax; col++)
                {
                    if (row == 0)
                    {
                        report[row, col] = values[col];
                        valueEqual = false;
                    }
                    else
                    {
                        report[row, col] = report[row - 1, col + 1] - report[row - 1, col];
                        if (col > 0)
                        {
                            if (report[row, col] != report[row, col - 1] && valueEqual)
                            {
                                valueEqual = false;
                            }
                        }
                    }
                    if (col == colMax - 1)
                    {
                        historyValue += report[row, col];
                    }
                }
                colMax -= 1;
                if (valueEqual) break;
            }

            totalHistoryValue += historyValue;
        }

        return totalHistoryValue.ToString();
    }

    public string PartB(string[] input)
    {
        long totalHistoryValue = 0;
        for (int i = 0; i < input.Length; i++)
        {
            List<long> values = Utilities.LineToLongs(input[i]);
            long[,] report = new long[values.Count, values.Count];
            int colMax = values.Count;
            long historyValue = 0;
            for (int row = 0; row < values.Count; row++)
            {
                bool valueEqual = true;
                for (int col = 0; col < colMax; col++)
                {
                    if (row == 0)
                    {
                        report[row, col] = values[col];
                        valueEqual = false;
                    }
                    else
                    {
                        report[row, col] = report[row - 1, col + 1] - report[row - 1, col];
                        if (col > 0)
                        {
                            if (report[row, col] != report[row, col - 1] && valueEqual)
                            {
                                valueEqual = false;
                            }
                        }
                    }
                    if (col == 0)
                    {
                        if (row % 2 == 0)
                            historyValue += report[row, col];
                        else
                            historyValue -= report[row, col];
                    }
                }
                colMax -= 1;
                if (valueEqual) break;
            }

            totalHistoryValue += historyValue;
        }

        return totalHistoryValue.ToString();

    }
}