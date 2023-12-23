namespace AdventOfCode.Process;

public class Day22 : IDay
{
    public string PartA(string[] input)
    {
        List<Brick> bricks = GenerateBricks(input);
        bricks.Sort();
        MoveBricks(bricks);

        return CountBricksDisintetrated(bricks).ToString();
    }

    public string PartB(string[] input)
    {
        List<Brick> bricks = GenerateBricks(input);
        bricks.Sort();
        MoveBricks(bricks);

        return CountBricksFall(bricks).ToString();
    }

    private static List<Brick> GenerateBricks(string[] input)
    {
        List<Brick> bricks = new();

        for (int i = 0; i < input.Length; i++)
        {
            string[] c = input[i].Split(',', '~'); // coordinates
            bricks.Add(new Brick(input[i], int.Parse(c[0]), int.Parse(c[3]), int.Parse(c[1]), int.Parse(c[4]), int.Parse(c[2]), int.Parse(c[5])));

        }
        return bricks;
    }

    private static void MoveBricks(List<Brick> bricks)
    {
        List<Brick> movedBricks = new();

        foreach (Brick brick in bricks)
        {
            bool brickIsBlocked = false;
            while (!brickIsBlocked)
            {
                if (IsBrickBelow(brick, movedBricks))
                {
                    movedBricks.Add(brick);
                    brickIsBlocked = true;
                }
                else
                {
                    brick.ZStart--;
                    brick.ZEnd--;
                }
            }
        }
    }

    private static bool IsBrickBelow(Brick brick, List<Brick> movedBricks)
    {
        if (brick.ZStart == 1)
        {
            return true;
        }
        bool isBrickBelow = false;
        foreach (Brick movedBrick in movedBricks)
        {
            if (movedBrick.IsBelow(brick))
            {
                isBrickBelow = true;

            }
        }
        return isBrickBelow;
    }

    private static int CountBricksDisintetrated(List<Brick> bricks)
    {
        int countBricks = 0;
        foreach (Brick brick in bricks)
        {
            if (brick.Support.Count == 0)
            {
                countBricks++;
                continue;
            }
            else
            {
                bool brickCanDisintegrates = true;
                foreach (Brick support in brick.Support)
                {
                    if (support.SupportedBy.Count < 2)
                    {
                        brickCanDisintegrates = false;
                    }
                }
                if (brickCanDisintegrates) countBricks++;
            }
        }
        return countBricks;
    }

    private static int CountBricksFall(List<Brick> bricks)
    {
        int countBricks = 0;

        foreach (Brick brick in bricks)
        {
            HashSet<Brick> fallenBricks = new()
            {
                brick
            };

            PriorityQueue<Brick, int> pq = new();
            foreach (Brick supportedBrick in brick.Support)
            {
                pq.Enqueue(supportedBrick, supportedBrick.ZStart);
            }

            while (pq.Count != 0)
            {

                Brick currentBrick = pq.Dequeue();

                if (!HasSupport(currentBrick, fallenBricks))
                {
                    fallenBricks.Add(currentBrick);
                    foreach (Brick supportedBrick in currentBrick.Support)
                    {
                        pq.Enqueue(supportedBrick, supportedBrick.ZStart);
                    }
                }
            }

            countBricks += fallenBricks.Count - 1;
        }

        return countBricks;
    }

    private static bool HasSupport(Brick currentBrick, HashSet<Brick> fallenBricks)
    {
        foreach (Brick supportedBrick in currentBrick.SupportedBy)
        {
            if (!fallenBricks.Contains(supportedBrick))
            {
                return true;
            }
        }
        return false;
    }

    private class Brick : IComparable<Brick>
    {
        public string Data { get; private set; }
        public int XStart { get; private set; }
        public int XEnd { get; private set; }
        public int YStart { get; private set; }
        public int YEnd { get; private set; }
        public int ZStart { get; set; }
        public int ZEnd { get; set; }
        public List<Brick> SupportedBy { get; set; }
        public List<Brick> Support { get; set; }

        public Brick(string data, int xStart, int xEnd, int yStart, int yEnd, int zStart, int zEnd)
        {
            Data = data;
            XStart = xStart;
            XEnd = xEnd;
            YStart = yStart;
            YEnd = yEnd;
            ZStart = zStart;
            ZEnd = zEnd;
            SupportedBy = new();
            Support = new();
        }
        public int CompareTo(Brick? other)
        {
            if (other == null)
            {
                return 0;
            }

            return ZStart.CompareTo(other.ZStart);
        }
        public bool IsBelow(Brick brick)
        {
            if (brick.ZStart - 1 == ZEnd)
            {
                for (int x = brick.XStart; x <= brick.XEnd; x++)
                {
                    for (int y = brick.YStart; y <= brick.YEnd; y++)
                    {
                        if (x >= XStart && x <= XEnd && y >= YStart && y <= YEnd)
                        {
                            Support.Add(brick);
                            brick.SupportedBy.Add(this);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    private static void PrintBricks(List<Brick> bricks, char direction)
    {



    }
}