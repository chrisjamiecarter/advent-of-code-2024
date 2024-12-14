using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day08 : BaseDay
{
    private readonly string _input = string.Empty;

    private int _maxX = 0;

    private int _maxY = 0;

    public Day08()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var antennas = ParseAntennas(_input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        var antinodes = new HashSet<(int x, int y)>();

        foreach (var (key, positions) in antennas)
        {
            foreach (var (x, y) in positions)
            {
                foreach (var (prevX, prevY) in positions)
                {
                    if ((x, y) == (prevX, prevY)) continue;

                    var dX = x - prevX;
                    var dY = y - prevY;

                    AddAntinode(antinodes, x + dX, y + dY);
                    AddAntinode(antinodes, prevX - dX, prevY - dY);
                }
            }
        }

        var answer = antinodes.Count;
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var antennas = ParseAntennas(_input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        var antinodes = new HashSet<(int x, int y)>();

        foreach (var (key, positions) in antennas)
        {
            foreach (var (x, y) in positions)
            {
                foreach (var (prevX, prevY) in positions)
                {
                    if ((x, y) == (prevX, prevY)) continue;

                    var dX = x - prevX;
                    var dY = y - prevY;

                    TraceAntinodes(antinodes, x,  y, dX, dY);
                    TraceAntinodes(antinodes, prevX, prevY, -dX, -dY);
                }
            }
        }

        var answer = antinodes.Count;
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private Dictionary<char, List<(int x, int y)>> ParseAntennas(string[] lines)
    {
        _maxY = lines.Length;
        _maxX = lines.First().Length;

        var antennas = new Dictionary<char, List<(int x, int y)>>();

        for (int y = 0; y < _maxY; y++)
        {
            for (int x = 0; x < _maxX; x++)
            {
                var location = lines[y][x];
                if (location == '.') continue;

                if (!antennas.ContainsKey(location))
                {
                    antennas[location] = [];
                }

                antennas[location].Add((x, y));
            }
        }

        return antennas;
    }

    private void AddAntinode(HashSet<(int x, int y)> antinodes, int x, int y)
    {
        if (IsWithinRange(x, y))
        {
            antinodes.Add((x, y));
        }
    }

    private void TraceAntinodes(HashSet<(int x, int y)> antinodes, int x, int y, int dX, int dY)
    {
        while (IsWithinRange(x, y))
        {
            antinodes.Add((x, y));
            x += dX;
            y += dY;
        }
    }

    private bool IsWithinRange(int x, int y)
    {
        return x >= 0 && x < _maxX && y >= 0 && y < _maxY;
    }
}
