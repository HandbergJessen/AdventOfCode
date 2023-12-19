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

        return "Not finished!";
    }


    private static (Dictionary<string, Workflow>, List<Rating>, List<string>) GenerateData(string[] data)
    {
        List<Rating> ratings = new();
        List<string> ids = new();
        Dictionary<string, Workflow> outWorkflows = new()
        {
            { "A", new Workflow("A") },
            { "R", new Workflow("R") }
        };
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
                        currentWF.Signs.Add(characters[1]);
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
            if (wf.Signs[i] == '<')
            {
                if (ratingValue < wf.Values[i])
                {
                    return LoopWorkflows(wf.NextWF[i], rating, workflows);
                }
            }
            else if (wf.Signs[i] == '>')
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
        public List<char> Signs { get; private set; }
        public List<int> Values { get; private set; }
        public List<string> NextWF { get; private set; }
        public string DefaultWF { get; set; }

        public Workflow(string id)
        {
            Id = id;
            Categories = new();
            Signs = new();
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
                Console.Write($"{workflow.Categories[i]}{workflow.Signs[i]}{workflow.Values[i]}:{workflow.NextWF[i]}, ");
            }
            Console.WriteLine($"{workflow.DefaultWF}");
        }
    }
}