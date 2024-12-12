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
        Dictionary<char, (int Area, int Perimeter)> plantRegions = [];

        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                char plant = _grid[x, y];
                int perimeter = 0;
                foreach (var direction in _directions)
                {
                    var checkX = x + direction.X;
                    var checkY = y + direction.Y;

                    if (checkX < 0 || checkX >= _maxX || checkY < 0 || checkY >= _maxY)
                    {
                        perimeter++;
                    }
                    else //else if (checkX > 0 && checkX < _maxX && checkY > 0 && checkY < _maxY)
                    {
                        if (_grid[checkX, checkY] != plant)
                        {
                            perimeter++;
                        }
                    }
                }

                if (!plantRegions.TryAdd(plant, (1, perimeter)))
                {
                    var region = plantRegions[plant];
                    region.Area++;
                    region.Perimeter += perimeter;
                    plantRegions[plant] = region;
                }

            }
        }

        var fencingCost = 0;
        foreach (var region in plantRegions.Values)
        {
            fencingCost += region.Area * region.Perimeter;

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
}
