const string usage = @"AdventOfCode

Usage: 
    AOF challenge (<user>) (<year>) (<day>) [a | b]
";

Interface.Run(new Docopt().Apply(usage, args, exit: true)!);