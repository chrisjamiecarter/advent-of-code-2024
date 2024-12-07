using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day07 : BaseDay
{
    private readonly string _input;

    private readonly List<(int TargetValue, List<int> Operands)> _equations = [];

    private readonly string[] operators = ["*", "+"];

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);

        foreach (string equation in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var parts = equation.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var targetValue = int.Parse(parts.First());
            var operands = new List<int>();

            foreach (string operand in parts.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                operands.Add(int.Parse(operand));
            }

            _equations.Add((targetValue, operands));
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        foreach (var equation in _equations)
        {
            var testValues = new List<int>();

            // just adds
            // just muliplications
            // al

        }


        answer = "TODO";

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
