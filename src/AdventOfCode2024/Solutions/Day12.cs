using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day12 : BaseDay
{
    private readonly string _input;
    private readonly char[,] _grid;
    private readonly int _maxX;
    private readonly int _maxY;

    private static readonly int[,] _deltaMap = new int[4, 2] { { -1, 0 }, { 0, 1 }, { 1, 0 }, { 0, -1 } };

    public Day12()
    {
        _input = File.ReadAllText(InputFilePath);

        var rows = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        _grid = new char[rows.First().Length, rows.Length];
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                _grid[x, y] = rows[y][x];
            }
        }

        _maxX = _grid.GetLength(0);
        _maxY = _grid.GetLength(1);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = Solve(ProcessRegionPerimeter);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = Solve(ProcessRegionPerimeterSections);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private string Solve(Func<int, int, bool[,], int> processRegion)
    {
        var visitedPlots = new bool[_maxX, _maxY];
        int fencingCost = 0;

        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                if (!visitedPlots[x, y])
                {
                    fencingCost += processRegion(x, y, visitedPlots);
                }
            }
        }

        return fencingCost.ToString();
    }

    private int ProcessRegionPerimeter(int startX, int startY, bool[,] visitedPlots)
    {
        int perimeter = 0;
        var regionPlots = new List<(int X, int Y)>() { (startX, startY) };

        visitedPlots[startX, startY] = true;
        
        perimeter = ExplorePerimeter(startX, startY, visitedPlots, regionPlots, perimeter);

        return regionPlots.Count * perimeter;
    }

    private int ProcessRegionPerimeterSections(int startX, int startY, bool[,] visitedPlots)
    {
        var regionPlots = new List<(int X, int Y)>() { (startX, startY) };
        var sections = new List<(int Orientation, int Axis, int Plot, int Vector)>();

        visitedPlots[startX, startY] = true;

        ExplorePerimeterSection(startX, startY, visitedPlots, regionPlots, sections);

        return regionPlots.Count * CountSides(sections);
    }

    private int ExplorePerimeter(int x, int y, bool[,] visitedPlots, List<(int X, int Y)> regionPlots, int perimeter)
    {
        for (int i = 0; i < 4; i++)
        {
            int exploreX = x + _deltaMap[i, 1];
            int exploreY = y + _deltaMap[i, 0];

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

    private void ExplorePerimeterSection(int x, int y, bool[,] visitedPlots, List<(int X, int Y)> regionPlots, List<(int Orientation, int Axis, int Plot, int Vector)> sections)
    {
        for (int i = 0; i < 4; i++)
        {
            int exploreX = x + _deltaMap[i, 1];
            int exploreY = y + _deltaMap[i, 0];

            if (IsWithinBounds(exploreX, exploreY))
            {
                if (_grid[exploreX, exploreY] == _grid[x, y])
                {
                    if (!visitedPlots[exploreX, exploreY])
                    {
                        visitedPlots[exploreX, exploreY] = true;
                        regionPlots.Add((exploreX, exploreY));
                        ExplorePerimeterSection(exploreX, exploreY, visitedPlots, regionPlots, sections);
                    }
                }
                else
                {
                    AddPerimeterSection(sections, i, exploreX, exploreY);
                }
            }
            else
            {
                AddPerimeterSection(sections, i, exploreX, exploreY);
            }
        }
    }

    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < _maxX && y >= 0 && y < _maxY;
    }

    private static void AddPerimeterSection(List<(int Orientation, int Axis, int Plot, int Vector)> sections, int i, int x, int y)
    {
        if (_deltaMap[i, 0] != 0)
        {
            sections.Add((i % 2, y * 2 - _deltaMap[i, 0], x, i));
        }
        else
        {
            sections.Add((i % 2, x * 2 - _deltaMap[i, 1], y, i));
        }
    }

    private static int CountSides(List<(int Orientation, int Axis, int Plot, int Vector)> sections)
    {
        var sortedSections = sections.OrderBy(section => section.Orientation)
                                     .ThenBy(section => section.Axis)
                                     .ThenBy(section => section.Plot)
                                     .ThenBy(section => section.Vector)
                                     .ToList();

        var sides = 0;
        int orientation = -1;
        int axis = -2;
        int plot = -2;
        int direction = -1;
        foreach (var section in sortedSections)
        {
            if (section.Orientation != orientation || section.Axis != axis || !(section.Plot == plot || section.Plot == plot + 1) || (section.Orientation == orientation && section.Vector != direction))
            {
                sides++;
            }

            orientation = section.Orientation;
            axis = section.Axis;
            plot = section.Plot;
            direction = section.Vector;
        }

        return sides;
    }
}
