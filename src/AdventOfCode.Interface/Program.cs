const string usage = @"AdventOfCode

Usage: 
    AOF day (<user>) (<dayNumber>) [a | b]
";

Interface.Run(new Docopt().Apply(usage, args, exit: true)!);