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
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1");
    }

    public override ValueTask<string> Solve_2()
    {
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2");
    }
}
