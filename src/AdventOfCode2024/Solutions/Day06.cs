using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day06 : BaseDay
{
    private readonly string _input;

    private readonly int[,] _grid;

    private readonly int _maxX;

    private readonly int _maxY;

    private (int X, int Y) _startPosition;

    private (int X, int Y) _startDirection;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);

        var rows = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var grid = new int[rows.Length, rows.Length];

        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                switch (rows[y][x])
                {
                    case '.':
                        grid[x, y] = 0;
                        break;
                    case '#':
                        grid[x, y] = -1;
                        break;
                    default:
                        _startPosition = (x, y);
                        _startDirection = rows[y][x] switch
                        {
                            '^' => (0, -1),
                            '>' => (1, 0),
                            'v' => (0, 1),
                            _ => (-1, 0),
                        };
                        grid[x, y] = 1;
                        break;
                }
            }
        }

        _grid = grid;
        _maxX = _grid.GetLength(0);
        _maxY = _grid.GetLength(1);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        var currentPosition = _startPosition;
        var currentDirection = _startDirection;
        while (true)
        {
            (int X, int Y) nextPosition = MovePosition(currentPosition, currentDirection);
            if (nextPosition.X >= 0 && nextPosition.X < _maxX && nextPosition.Y >= 0 && nextPosition.Y < _maxY)
            {
                if (_grid[nextPosition.X, nextPosition.Y] == -1)
                {
                    currentDirection = TurnToTheRight(currentDirection);
                }
                else
                {
                    currentPosition = nextPosition;
                    _grid[currentPosition.X, currentPosition.Y]++;
                }
            }
            else
            {
                break;
            }
        }

        var distinctPositions = 0;
        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxY; x++)
            {
                if (_grid[x, y] > 0)
                {
                    distinctPositions++;
                }
            }
        }

        answer = distinctPositions.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        List<(int X, int Y)> distinctPositions = [];
        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                if (_grid[x, y] > 0)
                {
                    if (_startPosition.X == x && _startPosition.Y == y)
                    {
                        _grid[x, y] = 1;
                    }
                    else
                    {
                        _grid[x, y] = 0;
                        distinctPositions.Add((x, y));
                    }
                }
            }
        }

        var infiniteLoops = 0;
        foreach (var obstacle in distinctPositions)
        {
            var gridCopy = _grid.Clone() as int[,];

            gridCopy[obstacle.X, obstacle.Y] = -1;

            var currentPosition = _startPosition;
            var currentDirection = _startDirection;
            while (true)
            {
                (int X, int Y) nextPosition = MovePosition(currentPosition, currentDirection);
                if (nextPosition.X >= 0 && nextPosition.X < _maxX && nextPosition.Y >= 0 && nextPosition.Y < _maxY)
                {
                    if (gridCopy[nextPosition.X, nextPosition.Y] == -1)
                    {
                        currentDirection = TurnToTheRight(currentDirection);
                    }
                    else
                    {
                        currentPosition = nextPosition;
                        gridCopy[currentPosition.X, currentPosition.Y]++;
                    }

                    if (gridCopy[currentPosition.X, currentPosition.Y] > 4)
                    {
                        infiniteLoops++;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

        }

        answer = infiniteLoops.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private (int X, int Y) MovePosition((int X, int Y) position, (int X, int Y) direction)
    {
        return (position.X + direction.X, position.Y + direction.Y);
    }

    private (int X, int Y) TurnToTheRight((int X, int Y) direction)
    {
        return direction switch
        {
            (0, -1) => (1, 0),
            (1, 0) => (0, 1),
            (0, 1) => (-1, 0),
            _ => (0, -1),
        };
    }
}
