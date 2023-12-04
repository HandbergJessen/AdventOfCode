namespace AdventOfCode.Interface;

public class Interface
{
    private static readonly Dictionary<string, IChallenge> challenges = new() {
        { "1", new Challenge1() },
        { "2", new Challenge2() },
        { "3", new Challenge3() },
        { "4", new Challenge4() },
        { "5", new Challenge5() },
        { "6", new Challenge6() },
        { "7", new Challenge7() },
        { "8", new Challenge8() },
        { "9", new Challenge9() },
        { "10", new Challenge10() },
        { "11", new Challenge11() },
        { "12", new Challenge12() },
        { "13", new Challenge13() },
        { "14", new Challenge14() },
        { "15", new Challenge15() },
        { "16", new Challenge16() },
        { "17", new Challenge17() },
        { "18", new Challenge18() },
        { "19", new Challenge19() },
        { "20", new Challenge20() },
        { "21", new Challenge21() },
        { "22", new Challenge22() },
        { "23", new Challenge23() },
        { "24", new Challenge24() },
        { "25", new Challenge25() }
    };

    public static void Run(IDictionary<string, ValueObject> arguments)
    {
        if (arguments["challenge"].IsTrue)
        {
            string user = arguments["<user>"].ToString();
            string day = arguments["<day>"].ToString();

            IDataAccess dataAccess = new DataAccess();
            IChallenge? challenge = challenges.GetValueOrDefault($"{day}");

            if (challenge != null)
            {
                string[] data = dataAccess.GetData($"../AdventOfCode.Data/data/{user}/{day}.txt");

                Console.WriteLine();
                Console.WriteLine($"User: {user}");
                Console.WriteLine($"Challenge: {day}");
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