using System.ComponentModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Process;

public class Day19 : IDay
{
    public string PartA(string[] input)
    {
        var (workflows, ratings, ids) = GenerateData(input);

        //PrintWorkflows(workflows, ids);

        SortRatings(ratings, workflows);
        //PrintRatings(ratings);

        return SumCalulate(ratings).ToString();
    }

    public string PartB(string[] input)
    {
        var (workflows, ratings, ids) = GenerateData(input);

        return FindCombinations(workflows).ToString();
    }

    private static long FindCombinations(Dictionary<string, Workflow> workflows)
    {

        return CombinationRecursive("in", workflows, new long[] { 1, 4000, 1, 4000, 1, 4000, 1, 4000 });
    }
    private static long CombinationRecursive(string id, Dictionary<string, Workflow> workflows, long[] ranges)
    {

        long outCombination = 0;

        if (id == "R")
        {
            return 0;
        }
        else if (id == "A")
        {
            long x = ranges[1] - ranges[0] + 1;
            long m = ranges[3] - ranges[2] + 1;
            long a = ranges[5] - ranges[4] + 1;
            long s = ranges[7] - ranges[6] + 1;

            return x * m * a * s;
        }

        Workflow wf = workflows[id];

        for (int i = 0; i < wf.Categories.Count; i++)
        {
            long[] newRanges = new[] { ranges[0], ranges[1], ranges[2], ranges[3], ranges[4], ranges[5], ranges[6], ranges[7] };

            long value = wf.Values[i];
            switch (wf.Categories[i], wf.GreaterThan[i])
            {

                case ('x', true):
                    if (value > ranges[0])
                    {
                        newRanges[0] = value + 1;
                        ranges[1] = value;
                    }
                    break;
                case ('x', false):
                    if (value < ranges[1])
                    {
                        newRanges[1] = value - 1;
                        ranges[0] = value;
                    }
                    break;
                case ('m', true):
                    if (value > ranges[2])
                    {
                        newRanges[2] = value + 1;
                        ranges[3] = value;
                    }
                    break;
                case ('m', false):
                    if (value < ranges[3])
                    {
                        newRanges[3] = value - 1;
                        ranges[2] = value;
                    }
                    break;
                case ('a', true):
                    if (value > ranges[4])
                    {
                        newRanges[4] = value + 1;
                        ranges[5] = value;
                    }
                    break;
                case ('a', false):
                    if (value < ranges[5])
                    {
                        newRanges[5] = value - 1;
                        ranges[4] = value;
                    }
                    break;
                case ('s', true):
                    if (value > ranges[6])
                    {
                        newRanges[6] = value + 1;
                        ranges[7] = value;
                    }
                    break;
                case ('s', false):
                    if (value < ranges[7])
                    {
                        newRanges[7] = value - 1;
                        ranges[6] = value;
                    }
                    break;
                default: break;
            }
            outCombination += CombinationRecursive(wf.NextWF[i], workflows, newRanges);
        }
        outCombination += CombinationRecursive(wf.DefaultWF, workflows, ranges);

        return outCombination;
    }


    private static (Dictionary<string, Workflow>, List<Rating>, List<string>) GenerateData(string[] data)
    {
        List<Rating> ratings = new();
        List<string> ids = new();
        Dictionary<string, Workflow> outWorkflows = new();
        bool workflowData = true;
        foreach (string line in data)
        {
            if (line == "")
            {
                workflowData = false;
            }
            string[] splitData = line.Split('{', '}');

            if (workflowData)
            {
                string wfId = splitData[0];
                ids.Add(wfId);
                outWorkflows.Add(wfId, new Workflow(wfId));
            }
            else if (line != "")
            {
                string[] ratValues = splitData[1].Split(',', '=');
                int xValue = int.Parse(ratValues[1]);
                int mValue = int.Parse(ratValues[3]);
                int aValue = int.Parse(ratValues[5]);
                int sValue = int.Parse(ratValues[7]);

                ratings.Add(new Rating(xValue, mValue, aValue, sValue));
            }
        }

        foreach (string line in data)
        {
            if (line == "")
            {
                break;
            }
            else
            {
                string[] wfData = line.Split('{', ',', '}');
                string wfKey = wfData[0];
                Workflow currentWF = outWorkflows[wfKey];

                for (int i = 0; i < wfData.Length; i++)
                {
                    if (i == 0 || i == wfData.Length - 1) continue;
                    if (i == wfData.Length - 2)
                    {
                        currentWF.DefaultWF = wfData[i];
                    }
                    else
                    {
                        string[] details = wfData[i].Split(':');
                        currentWF.NextWF.Add(details[1]);
                        char[] characters = details[0].ToCharArray();
                        currentWF.Categories.Add(characters[0]);
                        if (characters[1] == '>')
                            currentWF.GreaterThan.Add(true);
                        else
                            currentWF.GreaterThan.Add(false);
                        string[] values = details[0].Split('<', '>');
                        currentWF.Values.Add(int.Parse(values[1]));
                    }
                }
            }
        }

        return (outWorkflows, ratings, ids);
    }
    private static void SortRatings(List<Rating> ratings, Dictionary<string, Workflow> workflows)
    {
        foreach (Rating rating in ratings)
        {
            if (LoopWorkflows("in", rating, workflows) == "A")
            {
                rating.Accepted = true;
            }
        }
    }
    private static string LoopWorkflows(string id, Rating rating, Dictionary<string, Workflow> workflows)
    {
        if (id == "A" || id == "R") return id;
        Workflow wf = workflows[id];
        for (int i = 0; i < wf.Categories.Count; i++)
        {

            int ratingValue = 0;
            switch (wf.Categories[i])
            {
                case 'x': ratingValue = rating.X; break;
                case 'm': ratingValue = rating.M; break;
                case 'a': ratingValue = rating.A; break;
                case 's': ratingValue = rating.S; break;
                default: break;
            }
            if (!wf.GreaterThan[i])
            {
                if (ratingValue < wf.Values[i])
                {
                    return LoopWorkflows(wf.NextWF[i], rating, workflows);
                }
            }
            else if (wf.GreaterThan[i])
            {
                if (ratingValue > wf.Values[i])
                {
                    return LoopWorkflows(wf.NextWF[i], rating, workflows);
                }
            }
        }

        return LoopWorkflows(wf.DefaultWF, rating, workflows);
    }

    private static int SumCalulate(List<Rating> ratings)
    {
        int totalSum = 0;

        foreach (Rating rating in ratings)
        {
            if (rating.Accepted)
            {
                totalSum += rating.X + rating.M + rating.A + rating.S;
            }
        }

        return totalSum;
    }
    private class Workflow
    {
        public string Id { get; private set; }
        public List<char> Categories { get; private set; }
        public List<bool> GreaterThan { get; private set; }
        public List<int> Values { get; private set; }
        public List<string> NextWF { get; private set; }
        public string DefaultWF { get; set; }

        public Workflow(string id)
        {
            Id = id;
            Categories = new();
            GreaterThan = new();
            Values = new();
            NextWF = new();
            DefaultWF = "";
        }
    }
    private class Rating
    {
        public int X { get; private set; }
        public int M { get; private set; }
        public int A { get; private set; }
        public int S { get; private set; }
        public bool Accepted { get; set; }

        public Rating(int x, int m, int a, int s)
        {
            X = x;
            M = m;
            A = a;
            S = s;
            Accepted = false;
        }
    }
    private static void PrintRatings(List<Rating> ratings)
    {
        foreach (Rating rating in ratings)
        {
            Console.WriteLine($"{rating.X} {rating.M} {rating.A} {rating.S} {rating.Accepted}");
        }
    }
    private static void PrintWorkflows(Dictionary<string, Workflow> workflows, List<string> ids)
    {
        //foreach (string key  in ids)
        foreach (string key in workflows.Keys)
        {
            Workflow workflow = workflows[key];
            Console.Write($"{workflow.Id} ");
            for (int i = 0; i < workflow.Categories.Count; i++)
            {
                Console.Write($"{workflow.Categories[i]}{workflow.GreaterThan[i]}{workflow.Values[i]}:{workflow.NextWF[i]}, ");
            }
            Console.WriteLine($"{workflow.DefaultWF}");
        }
    }
}