namespace AdventOfCode.Interface;

public class Interface
{
    private static readonly Dictionary<string, IChallenge> challenges = new() {
        { "2023-1", new Challenge1() },
        { "2023-2", new Challenge2() },
        { "2023-3", new Challenge3() },
        { "2023-4", new Challenge4() },
        { "2023-5", new Challenge5() },
        { "2023-6", new Challenge6() },
        { "2023-7", new Challenge7() },
        { "2023-8", new Challenge8() },
        { "2023-9", new Challenge9() },
        { "2023-10", new Challenge10() },
        { "2023-11", new Challenge11() },
        { "2023-12", new Challenge12() },
        { "2023-13", new Challenge13() },
        { "2023-14", new Challenge14() },
        { "2023-15", new Challenge15() },
        { "2023-16", new Challenge16() },
        { "2023-17", new Challenge17() },
        { "2023-18", new Challenge18() },
        { "2023-19", new Challenge19() },
        { "2023-20", new Challenge20() },
        { "2023-21", new Challenge21() },
        { "2023-22", new Challenge22() },
        { "2023-23", new Challenge23() },
        { "2023-24", new Challenge24() },
        { "2023-25", new Challenge25() }
    };

    public static void Run(IDictionary<string, ValueObject> arguments)
    {
        if (arguments["challenge"].IsTrue)
        {
            string user = arguments["<user>"].ToString();
            string year = arguments["<year>"].ToString();
            string day = arguments["<day>"].ToString();

            IChallenge? challenge = challenges.GetValueOrDefault(year + "-" + day);

            if (challenge != null)
            {
                string[] data = new DataAccess().GetData(user, year, day);

                Console.WriteLine();
                Console.WriteLine("User: " + user);
                Console.WriteLine("Challenge: " + year + "-" + day);
                Console.WriteLine();

                if (arguments["a"].IsTrue)
                {
                    PartA(challenge, data);
                }
                else if (arguments["b"].IsTrue)
                {
                    PartB(challenge, data);
                }
                else
                {
                    PartA(challenge, data);
                    PartB(challenge, data);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The challenge you are trying to run does not exists!");
                Console.WriteLine();
            }
        }
    }

    private static void PartA(IChallenge challenge, string[] data)
    {
        Console.WriteLine("Answer to part A.");
        Console.WriteLine(challenge.PartA(data));
        Console.WriteLine();
    }

    private static void PartB(IChallenge challenge, string[] data)
    {
        Console.WriteLine("Answer to part B.");
        Console.WriteLine(challenge.PartB(data));
        Console.WriteLine();
    }
}