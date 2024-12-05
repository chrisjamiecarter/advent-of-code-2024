using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day01 : BaseDay
{
    private readonly string _input;

    private readonly List<int> _left = [];
    private readonly List<int> _right = [];

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            _left.Add(int.Parse(numbers.First()));
            _right.Add(int.Parse(numbers.Last()));
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        _left.Sort();
        _right.Sort();

        var differences = _left.Zip(_right, (x, y) => Math.Abs(x - y));

        answer = differences.Sum().ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        var similarityScores = _left.Select(x => x * _right.Count(y => x == y));

        answer = similarityScores.Sum().ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
