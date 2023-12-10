namespace AdventOfCode.Process;

public class Day10 : IDay
{
    public string PartA(string[] input)
    {
        char[][] grid = Utilities.GenerateGrid(input);
        var (y, x, pipe, direction) = GetStartDirection(grid);
        int moves = 1;

        while (pipe != 'S')
        {

            switch (direction)
            {
                case 'N': y--; break;
                case 'S': y++; break;
                case 'W': x--; break;
                case 'E': x++; break;
                default: break;

            }
            pipe = grid[y][x];

            direction = GetDirection(pipe, direction);
            moves++;
        }
        if (moves % 2 == 0) moves /= 2;
        else moves = moves / 2 + 1;

        return moves.ToString();

    }
    public string PartB(string[] input)
    {
        return "Not finished!";
    }


    private static (int y, int x, char pipe, char direction) GetStartDirection(char[][] grid)
    {
        for (int y = 0; y < grid.Length; y++)
        {
            char[] row = grid[y];

            for (int x = 0; x < row.Length; x++)
            {
                if (grid[y][x] == 'S')  // Start pipe
                {
                    char nextPipe = grid[y - 1][x];
                    char direction = GetDirection(nextPipe, 'N');
                    if (direction != 'G') return (y - 1, x, nextPipe, direction); // North is valid

                    nextPipe = grid[y + 1][x];
                    direction = GetDirection(nextPipe, 'S');
                    if (direction != 'G') return (y + 1, x, nextPipe, direction); // South is valid

                    nextPipe = grid[y][x - 1];
                    direction = GetDirection(nextPipe, 'W');
                    if (direction != 'G') return (y, x - 1, nextPipe, direction); // West is valid

                    nextPipe = grid[y][x + 1];
                    direction = GetDirection(nextPipe, 'E');
                    if (direction != 'G') return (y, x + 1, nextPipe, direction); // East is valid

                }
            }
        }
        return (0, 0, ' ', ' ');
    }


    private static char GetDirection(char pipe, char direction)
    {
        char outDirection;
        if (pipe == 'S')
        {
            outDirection = '0';
        }
        else
        {
            if (direction == 'N') direction = 'S';
            else if (direction == 'S') direction = 'N';
            else if (direction == 'E') direction = 'W';
            else if (direction == 'W') direction = 'E';

            var outDir = (pipe, direction)
            switch
            {
                ('|', 'S') => 'N',
                ('|', 'N') => 'S',
                ('-', 'W') => 'E',
                ('-', 'E') => 'W',
                ('F', 'S') => 'E',
                ('F', 'E') => 'S',
                ('7', 'S') => 'W',
                ('7', 'W') => 'S',
                ('J', 'N') => 'W',
                ('J', 'W') => 'N',
                ('L', 'N') => 'E',
                ('L', 'E') => 'N',
                _ => 'G',
            };


            outDirection = outDir;
        }

        return outDirection;
    }
}