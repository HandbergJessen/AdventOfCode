namespace AdventOfCode.Process;

public class Day20 : IDay
{
    public string PartA(string[] input)
    {
        Dictionary<string, Module> modules = GenerateData(input);

        StartButton4000(modules);
        PrintModules(modules);

        return CountPulses(modules).ToString();
    }

    public string PartB(string[] input)
    {
        return "Not finished!";
    }

    private static void StartButton4000(Dictionary<string, Module> modules)
    {
        for (int i = 1; i <= 2; i++)
        {
            List<string> destinations = new() { "broadcaster" };
            List<char> pulses = new() { 'L' };

            StartPulse(destinations, pulses, modules);
        }
    }
    private static void StartPulse(List<string> destinations, List<char> pulses, Dictionary<string, Module> modules)
    {
        List<string> newDestinations = new();
        List<char> newPulses = new();
        for (int i = 0; i < destinations.Count; i++)
        {
            Module module = modules[destinations[i]];
            module.RecentPulse = pulses[i];

            if (module.Prefix == 'B') // Broadcaster
            {
                if (module.RecentPulse == 'L')
                    module.LowPulseCount++;
                else
                    module.HighPulseCount++;

                Console.WriteLine($" -> broadcaster -> {module.RecentPulse} {module.LowPulseCount} {module.HighPulseCount}");
                for (int j = 0; j < module.Destinations.Count; j++)
                {
                    newDestinations.Add(module.Destinations[j]);
                    newPulses.Add(module.RecentPulse);
                    Console.WriteLine($"broadcaster -> {module.RecentPulse} {module.Destinations[j]}");
                }
            }
            else if (module.Prefix == '%') // Flip Flop
            {
                if (module.RecentPulse == 'L')
                {
                    module.LowPulseCount++;

                    for (int j = 0; j < module.Destinations.Count; j++)
                    {
                        newDestinations.Add(module.Destinations[j]);
                        char tempPuls;
                        if (module.ChangeSwitchToOn())
                        {
                            tempPuls = 'H';
                            newPulses.Add('H');
                        }
                        else
                        {
                            tempPuls = 'L';
                            newPulses.Add('L');
                        }
                        Console.WriteLine($"{module.Id} -> {tempPuls} {module.Destinations[j]} {module.LowPulseCount} {module.HighPulseCount}");
                    }
                }
                else
                {
                    module.HighPulseCount++;
                    Console.WriteLine($"{module.Id} -> {module.RecentPulse} END?? {module.LowPulseCount} {module.HighPulseCount}");
                }
            }
            else if (module.Prefix == '&') // Conjunction
            {
                for (int j = 0; j < module.Destinations.Count; j++)
                {
                    newDestinations.Add(module.Destinations[j]);
                    if (module.RecentPulse == 'L')
                    {
                        module.LowPulseCount++;
                    }
                    else
                    {
                        module.HighPulseCount++;
                    }
                    module.AllRecentPulses.Add(module.RecentPulse);

                    bool allHighPulses = true;
                    foreach (char recentPulse in module.AllRecentPulses)
                    {
                        if (recentPulse == 'L') allHighPulses = false;
                    }
                    char tempPuls;
                    if (allHighPulses)
                    {
                        tempPuls = 'L';
                        newPulses.Add('L');
                    }
                    else
                    {
                        tempPuls = 'H';
                        newPulses.Add('H');
                    }
                    Console.WriteLine($"{module.Id} -> {tempPuls} {module.Destinations[j]} {module.LowPulseCount} {module.HighPulseCount}");
                }
            }
        }
        if (newDestinations.Count > 0)
            StartPulse(newDestinations, newPulses, modules);
    }

    private static Dictionary<string, Module> GenerateData(string[] data)
    {
        Dictionary<string, Module> modules = new();
        foreach (string line in data)
        {
            string[] splitData = line.Split(" -> ");
            string[] destSplit = splitData[1].Split(", ");

            List<string> destinations = new();
            for (int i = 0; i < destSplit.Length; i++)
            {
                destinations.Add(destSplit[i]);
            }

            if (splitData[0] == "broadcaster")
            {
                modules.Add("broadcaster", new Module("broadcaster", 'B', destinations));
            }
            else
            {
                string[] id = splitData[0].Split('&', '%');
                char[] prefix = splitData[0].ToCharArray();
                modules.Add(id[1], new Module(id[1], prefix[0], destinations));
            }
        }
        return modules;
    }

    private class Module
    {
        public string Id { get; private set; }
        public char Prefix { get; private set; }
        public List<string> Destinations { get; private set; }
        public List<char> AllRecentPulses { get; set; }
        public char RecentPulse { get; set; }
        public long LowPulseCount { get; set; }
        public long HighPulseCount { get; set; }
        public bool SwitchOn { get; set; }

        public Module(string id, char prefix, List<string> destinations)
        {
            Id = id;
            Prefix = prefix;
            Destinations = destinations;
            AllRecentPulses = new();
            RecentPulse = 'L';
            LowPulseCount = 0;
            HighPulseCount = 0;
            SwitchOn = false;
        }
        public bool ChangeSwitchToOn()
        {
            if (!SwitchOn) SwitchOn = true;
            else SwitchOn = false;

            return SwitchOn;
        }
    }
    private static (long, long) CountPulses(Dictionary<string, Module> modules)
    {
        long countLowPulses = 0;
        long countHighPulses = 0;
        //foreach (string key  in ids)

        foreach (string key in modules.Keys)
        {
            Module module = modules[key];
            countLowPulses += module.LowPulseCount;
            countHighPulses += module.HighPulseCount;
        }
        return (countLowPulses, countHighPulses);
    }
    private static void PrintModules(Dictionary<string, Module> modules)
    {
        Console.WriteLine();
        Console.WriteLine();

        //foreach (string key  in ids)
        foreach (string key in modules.Keys)
        {
            Module module = modules[key];
            Console.Write($"{module.Prefix}{module.Id} {module.LowPulseCount} {module.HighPulseCount} -> ");
            for (int i = 0; i < module.Destinations.Count; i++)
            {
                Console.Write($"{module.Destinations[i]} ");
            }
            Console.WriteLine();
        }
    }
}
