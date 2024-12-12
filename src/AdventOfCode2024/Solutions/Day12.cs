using System.Security;
using AoCHelper;
using Spectre.Console;

namespace AdventOfCode2024.Solutions;

public class Day12 : BaseDay
{
    private readonly string _input;

    private readonly char[,] _grid;

    private readonly int _maxX;

    private readonly int _maxY;

    private static IEnumerable<(int X, int Y)> _directions =
    [
        (0, -1),
        (1, 0),
        (0, 1),
        (-1, 0)
    ];

    public Day12()
    {
        _input = File.ReadAllText(InputFilePath);

        var rows = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var grid = new char[rows.First().Length, rows.Length];
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                grid[x, y] = rows[y][x];
            }
        }

        _grid = grid;
        _maxX = _grid.GetLength(0);
        _maxY = _grid.GetLength(1);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        // SOLVE.
        var visitedPlots = new bool[_maxX, _maxY];
        var fencingCost = 0;

        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                if (!visitedPlots[x, y])
                {
                    fencingCost += ProcessRegion(x, y, visitedPlots);
                }
            }
        }

        answer = fencingCost.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private int ProcessRegion(int startX, int startY, bool[,] visitedPlots)
    {
        List<(int X, int Y)> regionPlots = [(startX, startY)];
        int perimeter = 0;

        visitedPlots[startX, startY] = true;
        perimeter = ExplorePerimeter(startX, startY, visitedPlots, regionPlots, perimeter);

        return regionPlots.Count * perimeter;
    }

    private int ExplorePerimeter(int x, int y, bool[,] visitedPlots, List<(int X, int Y)> regionPlots, int perimeter)
    {
        foreach (var direction in _directions)
        {
            int exploreX = x + direction.X;
            int exploreY = y + direction.Y;

            if (IsWithinBounds(exploreX, exploreY))
            {
                if (_grid[exploreX, exploreY] == _grid[x, y])
                {
                    if (!visitedPlots[exploreX, exploreY])
                    {
                        visitedPlots[exploreX, exploreY] = true;
                        regionPlots.Add((exploreX, exploreY));
                        perimeter = ExplorePerimeter(exploreX, exploreY, visitedPlots, regionPlots, perimeter);
                    }
                }
                else
                {
                    perimeter++;
                }
            }
            else
            {
                perimeter++;
            }
        }

        return perimeter;
    }

    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < _maxX && y >= 0 && y < _maxY;
    }

}
