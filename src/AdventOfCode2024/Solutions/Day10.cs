using System.Data;
using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day10 : BaseDay
{
    private readonly string _input;

    private static readonly (int X, int Y)[] _directions = [(0, -1), (1, 0), (0, 1), (-1, 0)];

    private static int _maxX = 0;

    private static int _maxY = 0;

    public Day10()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var grid = ParseInputToGrid(_input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

        var reachable9s = new HashSet<(int X, int Y)>();
        var score = 0;

        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                if (grid[x, y] == '0')
                {
                    score += Walk(grid, x, y, reachable9s, true);
                }
            }
        }

        var answer = score;
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = "TODO";
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private char[,] ParseInputToGrid(string[] lines)
    {
        _maxY = lines.Length;
        _maxX = lines.First().Length;

        var grid = new char[_maxX, _maxY];

        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                grid[x, y] = lines[y][x];
            }
        }

        return grid;
    }

    private int Walk(char[,] grid, int x, int y, HashSet<(int X, int Y)>? reachable9s, bool countReachable)
    {
        int counter = 0;
        var stack = new Stack<(int X, int Y)>();
        stack.Push((x, y));

        while (stack.Count > 0)
        {
            var (currentX, currentY) = stack.Pop();
            var height = grid[currentX, currentY] - '0';

            if (height == 9)
            {
                if (reachable9s != null)
                {
                    reachable9s.Add((currentY, currentX));
                }
                else
                {
                    counter++;
                }
                continue;
            }

            foreach (var (dX, dY) in _directions)
            {
                var newX = currentX + dX;
                var newY = currentY + dY;

                if (IsValidPosition(newX, newY) && grid[newX, newY] - '0' == height + 1)
                {
                    stack.Push((newX, newY));
                }
            }
        }

        if (countReachable && reachable9s != null)
        {
            counter += reachable9s.Count;
            reachable9s.Clear();
        }

        return counter;
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < _maxX && y >= 0 && y < _maxY;
    }
}
