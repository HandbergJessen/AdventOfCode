namespace AdventOfCode.Process;

public class Day11 : IDay
{
    public string PartA(string[] input)
    {
        List<List<char>> universe = GenerateUniverse(input);
        List<Galaxy> galaxies = GenerateGalaxies(universe);

        ExpandGalaxies(universe, galaxies, 1);

        return GalaxyDistance(galaxies).ToString();
    }

    public string PartB(string[] input)
    {
        List<List<char>> universe = GenerateUniverse(input);
        List<Galaxy> galaxies = GenerateGalaxies(universe);

        ExpandGalaxies(universe, galaxies, 999999);

        return GalaxyDistance(galaxies).ToString();
    }

    private static List<List<char>> GenerateUniverse(string[] input)
    {
        List<List<char>> universe = new();

        foreach (string row in input)
        {
            universe.Add(row.ToList());
        }

        return universe;
    }

    private static List<Galaxy> GenerateGalaxies(List<List<char>> universe)
    {
        List<Galaxy> galaxies = new();

        for (int y = 0; y < universe.Count; y++)
        {
            for (int x = 0; x < universe[y].Count; x++)
            {
                if (universe[y][x] == '#')
                {
                    galaxies.Add(new Galaxy(x, y));
                }
            }
        }

        return galaxies;
    }

    private static void ExpandGalaxies(List<List<char>> universe, List<Galaxy> galaxies, int expansion)
    {
        for (int x = 0; x < universe[0].Count; x++)
        {
            if (IsEmptyColoum(universe, x))
            {
                foreach (Galaxy galaxy in galaxies)
                {
                    if (x < galaxy.X)
                    {
                        galaxy.ExpandedX += expansion;
                    }
                }
            }
        }

        for (int y = 0; y < universe.Count; y++)
        {
            if (!universe[y].Contains('#'))
            {
                foreach (Galaxy galaxy in galaxies)
                {
                    if (y < galaxy.Y)
                    {
                        galaxy.ExpandedY += expansion;
                    }
                }
            }
        }
    }

    private static long GalaxyDistance(List<Galaxy> galaxies)
    {
        long totalDistance = 0;

        for (int i = 0; i < galaxies.Count; i++)
        {
            Galaxy galaxy = galaxies[i];

            for (int j = i + 1; j < galaxies.Count; j++)
            {
                totalDistance += galaxy.GetDistance(galaxies[j]);
            }
        }

        return totalDistance;
    }

    private static bool IsEmptyColoum(List<List<char>> universe, int x)
    {
        for (int y = 0; y < universe.Count; y++)
        {
            if (universe[y][x] == '#')
            {
                return false;
            }
        }

        return true;
    }

    private class Galaxy
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public long ExpandedX { get; set; }
        public long ExpandedY { get; set; }

        public Galaxy(int x, int y)
        {
            X = x;
            Y = y;
            ExpandedX = x;
            ExpandedY = y;
        }

        public long GetDistance(Galaxy other)
        {
            return Math.Abs(ExpandedX - other.ExpandedX) + Math.Abs(ExpandedY - other.ExpandedY);
        }
    }
}