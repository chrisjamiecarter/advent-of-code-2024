using AoCHelper;

namespace AdventOfCode2024.Console.Solutions;

public class Day01 : BaseDay
{
    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = "TODO";

        List<int> left = [];
        List<int> right = [];

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            left.Add(Convert.ToInt32(numbers.First()));
            right.Add(Convert.ToInt32(numbers.Last()));
        }

        left.Sort();
        right.Sort();

        var differences = left.Zip(right, (x, y) => Math.Abs(x - y));

        answer = differences.Sum().ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = "TODO";

        List<int> left = [];
        List<int> right = [];

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            left.Add(Convert.ToInt32(numbers.First()));
            right.Add(Convert.ToInt32(numbers.Last()));
        }

        var similarityScores = left.Select(x => x * right.Count(y => x == y));

        answer = similarityScores.Sum().ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
