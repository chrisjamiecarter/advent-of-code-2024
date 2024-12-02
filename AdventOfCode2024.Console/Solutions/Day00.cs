using AoCHelper;

namespace AdventOfCode2024.Console.Solutions;

public class Day00 : BaseDay
{
    private readonly string _input;

    public Day00()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = "TODO";
                
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
