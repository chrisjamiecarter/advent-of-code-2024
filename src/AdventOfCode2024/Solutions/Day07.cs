using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day07 : BaseDay
{
    private readonly string _input;

    private readonly List<(long TargetValue, List<long> Operands)> _equations = [];

    private readonly string[] operators = ["*", "+"];

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);

        foreach (string equation in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            var parts = equation.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var targetValue = long.Parse(parts.First());
            var operands = new List<long>();

            foreach (string operand in parts.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                operands.Add(long.Parse(operand));
            }

            _equations.Add((targetValue, operands));
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        long totalCalibrationResult = 0;

        foreach (var equation in _equations)
        {
            if (CanMatchTarget(equation))
            {
                totalCalibrationResult += equation.TargetValue;
            }
        }


        answer = totalCalibrationResult.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static bool CanMatchTarget((long TargetValue, List<long> Operands) equation)
    {
        return Evaluate(equation.TargetValue, equation.Operands, equation.Operands[0], 1);
    }

    private static bool Evaluate(long targetValue, List<long> operands, long currentValue, int index)
    {
        if (index == operands.Count)
        {
            return targetValue == currentValue;
        }

        if (Evaluate(targetValue, operands, currentValue + operands[index], index + 1))
        {
            return true;
        }

        if (Evaluate(targetValue, operands, currentValue * operands[index], index + 1))
        {
            return true;
        }

        return false;
    }
}
