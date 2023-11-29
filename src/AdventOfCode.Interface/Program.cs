using DocoptNet;
using AdventOfCode.Data;
using AdventOfCode.Process;
using AdventOfCode.Foundation;

const string usage = @"AdventOfCode

Usage: 
    AOF challenge (<year>) (<user>) (<number>)
";

Console.WriteLine("hello");

IDictionary<string, ValueObject> arguments = new Docopt().Apply(usage, args, exit: true)!;
IDataAccess dataAccess = new DataAccess();

Console.WriteLine("hello1");

Dictionary<string, IChallenge> challenges = new() {
    { "2023-1", new Challenge1() },
    { "2023-2", new Challenge2() }
};

Console.WriteLine("hello2");

if (arguments["challenge"].IsTrue) {
    string year = arguments["<year>"].ToString();
    string user = arguments["<user>"].ToString();
    string number = arguments["<number>"].ToString();

    Console.WriteLine("hello3");

    string[] data = dataAccess.GetData(year, user, number);
    IChallenge? challenge = challenges.GetValueOrDefault(year + "-" + number);

    if (challenge != null) {
        Console.WriteLine("hello4");

        challenge.ChallengeA(data);
    } else {
        Console.WriteLine("hello5");

        Console.WriteLine("The challenge does not exist yet");
    }
}