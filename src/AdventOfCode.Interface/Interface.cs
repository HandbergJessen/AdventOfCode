namespace AdventOfCode.Interface;

public class Interface
{
    private static readonly Dictionary<string, IDay> days = new() {
        { "1", new Day1() },
        { "2", new Day2() },
        { "3", new Day3() },
        { "4", new Day4() },
        { "5", new Day5() },
        { "6", new Day6() },
        { "7", new Day7() },
        { "8", new Day8() },
        { "9", new Day9() },
        { "10", new Day10() },
        { "11", new Day11() },
        { "12", new Day12() },
        { "13", new Day13() },
        { "14", new Day14() },
        { "15", new Day15() },
        { "16", new Day16() },
        { "17", new Day17() },
        { "18", new Day18() },
        { "19", new Day19() },
        { "20", new Day20() },
        { "21", new Day21() },
        { "22", new Day22() },
        { "23", new Day23() },
        { "24", new Day24() },
        { "25", new Day25() }
    };

    public static void Run(IDictionary<string, ValueObject> arguments)
    {
        if (arguments["day"].IsTrue)
        {
            string user = arguments["<user>"].ToString();
            string dayNumber = arguments["<dayNumber>"].ToString();

            IDataAccess dataAccess = new DataAccess();
            IDay? day = days.GetValueOrDefault($"{dayNumber}");

            if (day != null)
            {
                string[] data = dataAccess.GetData($"../AdventOfCode.Data/data/{user}/{dayNumber}.txt");

                Console.WriteLine();
                Console.WriteLine($"User: {user}");
                Console.WriteLine($"Day: {dayNumber}");
                Console.WriteLine();

                if (arguments["a"].IsTrue)
                {
                    PartA(day, data);
                }
                else if (arguments["b"].IsTrue)
                {
                    PartB(day, data);
                }
                else
                {
                    PartA(day, data);
                    PartB(day, data);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The day you are trying to run does not exists!");
                Console.WriteLine();
            }
        }
    }

    private static void PartA(IDay day, string[] data)
    {
        Console.WriteLine("Answer to part A:");
        Console.WriteLine(day.PartA(data));
        Console.WriteLine();
    }

    private static void PartB(IDay day, string[] data)
    {
        Console.WriteLine("Answer to part B:");
        Console.WriteLine(day.PartB(data));
        Console.WriteLine();
    }
}