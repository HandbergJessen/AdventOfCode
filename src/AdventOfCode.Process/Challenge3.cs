namespace AdventOfCode.Process;

public class Challenge3 : IChallenge
{
    public string PartA(string[] input)
    {
        char[][] grid = GenerateGrid(input);
        List<Number> numbers = GenerateNumbers(grid);

        int sum = 0;
        foreach (Number number in numbers)
        {
            if (number.HasSpecialNeighbour(grid))
            {
                sum += number.GetValue();
            }
        }

        return sum.ToString();
    }

    public string PartB(string[] input)
    {
        char[][] grid = GenerateGrid(input);
        List<Star> stars = GenerateStars(grid);

        int sum = 0;
        foreach (Star star in stars)
        {
            sum += star.GetGearValue(grid);
        }

        return sum.ToString();
    }

    private class Star
    {
        private readonly int xStart;
        private readonly int yStart;
        private readonly int xEnd;
        private readonly int yEnd;
        private readonly int xPos;

        public Star(int xPos, int yPos)
        {
            this.xPos = xPos;
            xStart = xPos - 1;
            yStart = yPos - 1;
            xEnd = xPos + 1;
            yEnd = yPos + 1;
        }

        public int GetGearValue(char[][] grid)
        {
            int totalGearValues = 0;
            int numbHit = 0;
            int[] gearValue = new int[6];

            for (int y = yStart; y <= yEnd; y++)
            {
                bool xHit = false;
                bool xStartHit = false;
                int factor;
                int intValue;

                for (int x = xEnd; x >= xStart; x--)
                {
                    if (x == xPos && xHit)
                    {
                        // x has been processed earlier
                    }
                    else if (x == xStart && xStartHit)
                    {
                        // xStart has been processed earlier
                    }
                    else if (char.IsNumber(grid[y][x]))
                    {
                        numbHit += 1;
                        intValue = (int)char.GetNumericValue(grid[y][x]);
                        gearValue[numbHit] += intValue;
                        factor = 10;

                        if (char.IsNumber(grid[y][x - 1]))
                        {
                            xHit = true;

                            if (x == xPos)
                            {
                                xStartHit = true;
                            }

                            intValue = (int)char.GetNumericValue(grid[y][x - 1]);
                            gearValue[numbHit] += intValue * factor;

                            if (char.IsNumber(grid[y][x - 2]))
                            {
                                xStartHit = true;
                                intValue = (int)char.GetNumericValue(grid[y][x - 2]);
                                factor = 100;
                                gearValue[numbHit] += intValue * factor;
                            }
                            else if (char.IsNumber(grid[y][x + 1]))
                            {
                                intValue = (int)char.GetNumericValue(grid[y][x + 1]);
                                gearValue[numbHit] *= factor;
                                gearValue[numbHit] += intValue;
                            }
                        }
                        else if (char.IsNumber(grid[y][x + 1]))
                        {
                            intValue = (int)char.GetNumericValue(grid[y][x + 1]);
                            gearValue[numbHit] *= factor;
                            gearValue[numbHit] += intValue;

                            if (char.IsNumber(grid[y][x + 2]))
                            {
                                intValue = (int)char.GetNumericValue(grid[y][x + 2]);
                                gearValue[numbHit] *= factor;
                                gearValue[numbHit] += intValue;
                            }
                        }
                    }
                }
            }

            if (numbHit == 2)
            {
                totalGearValues = gearValue[1] * gearValue[2];
            }

            return totalGearValues;
        }
    }

    private class Number
    {
        private int xStart;
        private int yStart;
        private int xEnd;
        private int yEnd;
        private readonly string value;

        public Number(int xPos, int yPos, string value)
        {
            xStart = xPos - 1;
            yStart = yPos - 1;
            xEnd = xPos + value.Length;
            yEnd = yPos + 1;
            this.value = value;
        }

        public int GetValue()
        {
            return int.Parse(value);
        }

        public bool HasSpecialNeighbour(char[][] grid)
        {
            ValidateCoordinates(grid);

            for (int y = yStart; y <= yEnd; y++)
            {
                for (int x = xStart; x <= xEnd; x++)
                {
                    if (grid[y][x] != '.' && !char.IsNumber(grid[y][x]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ValidateCoordinates(char[][] grid)
        {
            if (xStart < 0)
            {
                xStart = 0;
            }

            if (yStart < 0)
            {
                yStart = 0;
            }

            if (xEnd >= grid[0].Length)
            {
                xEnd = grid[0].Length - 1;
            }

            if (yEnd >= grid.Length)
            {
                yEnd = grid.Length - 1;
            }
        }
    }

    private static char[][] GenerateGrid(string[] data)
    {
        char[][] grid = new char[data.Length][];

        for (int y = 0; y < data.Length; y++)
        {
            grid[y] = data[y].ToCharArray();
        }

        return grid;
    }

    private static List<Number> GenerateNumbers(char[][] grid)
    {
        List<Number> numbers = new();

        for (int y = 0; y < grid.Length; y++)
        {
            char[] row = grid[y];
            int xPos = -1;
            string value = "";
            for (int x = 0; x < row.Length; x++)
            {
                if (char.IsNumber(row[x]))
                {
                    if (xPos == -1)
                    {
                        xPos = x;
                    }
                    value += row[x];
                }
                else
                {
                    if (xPos != -1)
                    {
                        numbers.Add(new Number(xPos, y, value));
                        xPos = -1;
                        value = "";
                    }
                }
            }

            numbers.Add(new Number(xPos, y, value));
        }

        return numbers;
    }

    private static List<Star> GenerateStars(char[][] grid)
    {
        List<Star> stars = new();

        for (int y = 0; y < grid.Length; y++)
        {
            char[] row = grid[y];
            for (int x = 0; x < row.Length; x++)
            {
                if (row[x] == '*')
                {
                    stars.Add(new Star(x, y));
                }
            }
        }

        return stars;
    }
}