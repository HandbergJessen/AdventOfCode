namespace AdventOfCode.Process;

public class Day10 : IDay
{
    public string PartA(string[] input)
    {
        Pipe[,] grid = GenerateGrid(input);
        Pipe startPipe = FindStartPipe(grid);

        SetStartPipeDirections(grid, startPipe);

        List<Pipe> pipes = GetLoop(grid, startPipe);

        return (pipes.Count % 2 == 0 ? pipes.Count / 2 : pipes.Count / 2 + 1).ToString();
    }

    public string PartB(string[] input)
    {
        Pipe[,] grid = GenerateGrid(input);
        Pipe startPipe = FindStartPipe(grid);

        SetStartPipeDirections(grid, startPipe);

        List<Pipe> pipes = GetLoop(grid, startPipe);

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                grid[y, x] = GetNewPipe(x, y, pipes);
            }
        }

        foreach (Pipe pipe in grid)
        {
            if (pipe.Visual != '.')
            {
                FindOutsideAndInside(grid, pipe);
                break;
            }
        }

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if ((x == 0 || y == 0 || x == grid.GetLength(1) - 1 || y == grid.GetLength(0) - 1) && grid[y, x].Visual == '.')
                {
                    grid[y, x].Visual = 'O';
                    continue;
                }

                if (grid[y, x].Visual == '.')
                {
                    if (grid[y, x - 1].Visual == 'O' || grid[y, x + 1].Visual == 'O' || grid[y - 1, x].Visual == 'O' || grid[y + 1, x].Visual == 'O')
                    {
                        grid[y, x].Visual = 'O';
                        continue;
                    }

                    if (grid[y, x - 1].Visual == 'I' || grid[y, x + 1].Visual == 'I' || grid[y - 1, x].Visual == 'I' || grid[y + 1, x].Visual == 'I')
                    {
                        grid[y, x].Visual = 'I';
                    }
                }
            }
        }
        int countInside = 0;
        foreach (Pipe pipe in grid)
        {
            if (pipe.Visual == 'I')
            {
                countInside++;
            }
        }

        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                char visual = grid[y, x].Visual;

                if (visual == 'I' || visual == 'O' || visual == '.' || visual == 'S')
                {
                    Console.Write(visual);

                }
                else
                {
                    Console.Write(' ');

                }
            }

            Console.WriteLine();
        }

        return countInside.ToString();
    }

    private static void CheckOutside(Pipe[,] grid, Pipe currentPipe, Direction outsideDirection)
    {
        int x = currentPipe.X;
        int y = currentPipe.Y;

        switch (outsideDirection)
        {
            case Direction.North: y--; break;
            case Direction.South: y++; break;
            case Direction.East: x++; break;
            case Direction.West: x--; break;
        }

        if (y < 0 | x < 0 | y > grid.GetLength(0) - 1 | x > grid.GetLength(1) - 1)
        {
            return;
        }

        if (grid[y, x].Visual == '.')
        {
            grid[y, x].Visual = 'O';
        }
    }

    private static void CheckInside(Pipe[,] grid, Pipe currentPipe, Direction outsideDirection)
    {
        int x = currentPipe.X;
        int y = currentPipe.Y;

        switch (outsideDirection)
        {
            case Direction.North: y--; break;
            case Direction.South: y++; break;
            case Direction.East: x++; break;
            case Direction.West: x--; break;
        }

        if (y < 0 | x < 0 | y > grid.GetLength(0) - 1 | x > grid.GetLength(1) - 1)
        {
            return;
        }

        if (grid[y, x].Visual == '.')
        {
            grid[y, x].Visual = 'I';
        }
    }

    private static void FindOutsideAndInside(Pipe[,] grid, Pipe outsidePipe)
    {
        Pipe currentPipe = outsidePipe;
        Direction currentDirection = Direction.East;
        Direction outsideDirection = Direction.West;
        Direction insideDirection = Direction.East;

        int startX = currentPipe.X;
        int startY = currentPipe.Y;

        do
        {
            CheckOutside(grid, currentPipe, outsideDirection);
            CheckInside(grid, currentPipe, insideDirection);

            switch (currentDirection)
            {
                case Direction.North: outsideDirection = Direction.West; break;
                case Direction.South: outsideDirection = Direction.East; break;
                case Direction.East: outsideDirection = Direction.North; break;
                case Direction.West: outsideDirection = Direction.South; break;
            }

            insideDirection = ReverseDirection(outsideDirection);

            CheckOutside(grid, currentPipe, outsideDirection);
            CheckInside(grid, currentPipe, insideDirection);

            int x = currentPipe.X;
            int y = currentPipe.Y;

            switch (currentDirection)
            {
                case Direction.North: y--; break;
                case Direction.South: y++; break;
                case Direction.East: x++; break;
                case Direction.West: x--; break;
            }

            currentPipe = grid[y, x];
            currentDirection = currentPipe.GetOutputDirection(currentDirection);
        } while (currentPipe.X != startX || currentPipe.Y != startY);
    }

    private static List<Pipe> GetLoop(Pipe[,] grid, Pipe startPipe)
    {
        Pipe currentPipe = startPipe;
        Direction currentDirection = currentPipe.Direction1;
        List<Pipe> pipes = new();

        do
        {
            pipes.Add(currentPipe);

            int x = currentPipe.X;
            int y = currentPipe.Y;

            switch (currentDirection)
            {
                case Direction.North: y--; break;
                case Direction.South: y++; break;
                case Direction.East: x++; break;
                case Direction.West: x--; break;
            }

            currentPipe = grid[y, x];
            currentDirection = currentPipe.GetOutputDirection(currentDirection);
        } while (currentPipe.Visual != 'S');

        return pipes;
    }

    private static Pipe GetNewPipe(int x, int y, List<Pipe> pipes)
    {
        foreach (Pipe pipe in pipes)
        {
            if (x == pipe.X && y == pipe.Y)
            {
                return pipe;
            }
        }

        return new Pipe(x, y, '.');
    }

    private static Pipe[,] GenerateGrid(string[] input)
    {
        Pipe[,] grid = new Pipe[input[0].Length, input.Length];

        for (int y = 0; y < input.Length; y++)
        {
            string row = input[y];

            for (int x = 0; x < row.Length; x++)
            {
                char pipe = row[x];

                grid[y, x] = pipe switch
                {
                    '|' => new Pipe(x, y, pipe, Direction.North, Direction.South),
                    '-' => new Pipe(x, y, pipe, Direction.East, Direction.West),
                    'F' => new Pipe(x, y, pipe, Direction.South, Direction.East),
                    '7' => new Pipe(x, y, pipe, Direction.South, Direction.West),
                    'J' => new Pipe(x, y, pipe, Direction.North, Direction.West),
                    'L' => new Pipe(x, y, pipe, Direction.North, Direction.East),
                    _ => new Pipe(x, y, pipe),
                };
            }
        }

        return grid;
    }

    private static Pipe FindStartPipe(Pipe[,] grid)
    {
        foreach (Pipe pipe in grid)
        {
            if (pipe.Visual == 'S')
            {
                return pipe;
            }
        }

        throw new Exception();
    }

    private static void SetStartPipeDirections(Pipe[,] grid, Pipe startPipe)
    {
        int x = startPipe.X;
        int y = startPipe.Y;

        Pipe northPipe = grid[y - 1, x];
        if (northPipe.DirectionMatch(Direction.North))
        {
            startPipe.Direction1 = Direction.North;
        }

        Pipe southPipe = grid[y + 1, x];
        if (southPipe.DirectionMatch(Direction.South))
        {
            if (startPipe.Direction1 == 0)
            {
                startPipe.Direction1 = Direction.South;
            }
            else
            {
                startPipe.Direction2 = Direction.South;
            }
        }

        Pipe eastPipe = grid[y, x + 1];
        if (eastPipe.DirectionMatch(Direction.East))
        {
            if (startPipe.Direction1 == 0)
            {
                startPipe.Direction1 = Direction.East;
            }
            else
            {
                startPipe.Direction2 = Direction.East;
            }
        }

        Pipe westPipe = grid[y, x - 1];
        if (westPipe.DirectionMatch(Direction.West))
        {
            startPipe.Direction2 = Direction.West;
        }
    }

    private enum Direction
    {
        North = 1,
        South = -1,
        East = 2,
        West = -2
    }

    private static Direction ReverseDirection(Direction direction)
    {
        return (Direction)((int)direction * -1);
    }

    private class Pipe
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Visual { get; set; }
        public Direction Direction1 { get; set; }
        public Direction Direction2 { get; set; }

        public Pipe(int x, int y, char visual)
        {
            X = x;
            Y = y;
            Visual = visual;
        }

        public Pipe(int x, int y, char visual, Direction direction1, Direction direction2)
        {
            X = x;
            Y = y;
            Visual = visual;
            Direction1 = direction1;
            Direction2 = direction2;
        }

        public bool DirectionMatch(Direction inputDirection)
        {
            Direction reverseDirection = ReverseDirection(inputDirection);
            return reverseDirection == Direction1 || reverseDirection == Direction2;
        }

        public Direction GetOutputDirection(Direction inputDirection)
        {
            Direction reverseDirection = ReverseDirection(inputDirection);

            if (reverseDirection == Direction1)
            {
                return Direction2;
            }
            else
            {
                return Direction1;
            }
        }
    }
}