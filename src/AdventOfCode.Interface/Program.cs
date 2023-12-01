﻿using DocoptNet;
using AdventOfCode.Data;
using AdventOfCode.Process;
using AdventOfCode.Foundation;

const string usage = @"AdventOfCode

Usage: 
    AOF challenge (<user>) (<year>) (<day>)
";

IDictionary<string, ValueObject> arguments = new Docopt().Apply(usage, args, exit: true)!;

IDataAccess dataAccess = new DataAccess();
Dictionary<string, IChallenge> challenges = new() {
    { "2023-1", new Challenge1() },
    { "2023-2", new Challenge2() }
};

if (arguments["challenge"].IsTrue)
{
    string user = arguments["<user>"].ToString();
    string year = arguments["<year>"].ToString();
    string day = arguments["<day>"].ToString();

    string[] data = dataAccess.GetData(user, year, day);
    IChallenge? challenge = challenges.GetValueOrDefault(year + "-" + day);

    if (challenge != null)
    {
        Console.WriteLine();
        Console.WriteLine("User: " + user);
        Console.WriteLine("Challenge: " + year + "-" + day);
        Console.WriteLine();
        Console.WriteLine("Answer to part A.");
        Console.WriteLine(challenge.PartA(data));
        Console.WriteLine();
        Console.WriteLine("Answer to part B.");
        Console.WriteLine(challenge.PartB(data));
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("The challenge you are trying to run does not exists!");
    }
}