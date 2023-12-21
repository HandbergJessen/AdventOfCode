using System.Reflection.Metadata;

namespace AdventOfCode.Process;

public class Day20 : IDay
{
    public string PartA(string[] input)
    {
        Dictionary<string, Module> modules = GenerateData(input);

        StartButton1000(modules);
        //PrintModules(modules);

        return CountPulses(modules).ToString();
    }

    public string PartB(string[] input)
    {
        return "Not finished!";
    }

    private static void StartButton1000(Dictionary<string, Module> modules)
    {
        for (int i = 1; i <= 1000; i++)
        {
            List<string> destinations = new() { "broadcaster" };
            List<char> pulses = new() { 'L' };
            List<string> fromDestinations = new() { " " };

            StartPulse(destinations, pulses, modules, fromDestinations);
        }
    }
    private static void StartPulse(List<string> destinations, List<char> pulses, Dictionary<string, Module> modules, List<string> fromDestinations)
    {
        List<string> emptyDestinations = new();
        List<string> newDestinations = new();
        List<char> newPulses = new();
        List<string> newFromDestinations = new();
        for (int i = 0; i < destinations.Count; i++)
        {
            if (!modules.ContainsKey(destinations[i]))
            {
                modules.Add(destinations[i], new Module(destinations[i], 'O', emptyDestinations));
            }
            Module module = modules[destinations[i]];
            module.RecentPulse = pulses[i];

            if (module.Prefix == 'B') // Broadcaster
            {
                if (module.RecentPulse == 'L')
                    module.LowPulseCount++;
                else
                    module.HighPulseCount++;

                for (int j = 0; j < module.Destinations.Count; j++)
                {
                    newDestinations.Add(module.Destinations[j]);
                    newPulses.Add(module.RecentPulse);
                    newFromDestinations.Add(module.Id);
                }
            }
            else if (module.Prefix == '%') // Flip Flop
            {
                if (module.RecentPulse == 'L')
                {
                    module.LowPulseCount++;
                    char tempPuls;
                    if (module.ChangeSwitchToOn()) tempPuls = 'H';
                    else tempPuls = 'L';

                    for (int j = 0; j < module.Destinations.Count; j++)
                    {
                        newDestinations.Add(module.Destinations[j]);
                        newPulses.Add(tempPuls);
                        newFromDestinations.Add(module.Id);
                    }
                }
                else
                {
                    module.HighPulseCount++;
                }
            }
            else if (module.Prefix == '&') // Conjunction
            {
                if (module.AllRecentPulses.ContainsKey(fromDestinations[i]))
                {
                    module.AllRecentPulses[fromDestinations[i]] = module.RecentPulse;
                }
                else
                {
                    module.AllRecentPulses.Add(fromDestinations[i], module.RecentPulse);
                }
                char tempPuls = 'L';
                foreach (string key in module.AllRecentPulses.Keys)
                {
                    char recentPulse = module.AllRecentPulses[key];
                    if (recentPulse == 'L') tempPuls = 'H';
                }
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
                    newPulses.Add(tempPuls);
                    newFromDestinations.Add(module.Id);
                }
            }
            else if (module.Prefix == 'O')
            {
                if (module.RecentPulse == 'L')
                    module.LowPulseCount++;
                else
                    module.HighPulseCount++;
            }
        }

        if (newDestinations.Count > 0)
            StartPulse(newDestinations, newPulses, modules, newFromDestinations);
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
        foreach (string key in modules.Keys)
        {
            Module moduleKey = modules[key];
            if (moduleKey.Prefix == '&')
            {
                foreach (string key2 in modules.Keys)
                {
                    Module moduleKey2 = modules[key2];
                    foreach (string dest in moduleKey2.Destinations)
                    {
                        if (dest == key)
                        {
                            moduleKey.AllRecentPulses.Add(moduleKey2.Id, 'L');
                        }
                    }
                }
            }
        }
        return modules;
    }

    private class Module
    {
        public string Id { get; private set; }
        public char Prefix { get; private set; }
        public List<string> Destinations { get; private set; }
        public Dictionary<string, char> AllRecentPulses { get; set; }
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
            RecentPulse = 'H';
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
    private static long CountPulses(Dictionary<string, Module> modules)
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
        return countLowPulses * countHighPulses;
    }
    private static void PrintModules(Dictionary<string, Module> modules)
    {
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
