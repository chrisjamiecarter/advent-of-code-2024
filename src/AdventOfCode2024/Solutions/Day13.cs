using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day13 : BaseDay
{
    private readonly string _input;

    public Day13()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        long answer = 0;
        for (int i = 0; i < lines.Length; i += 3)
        {
            var a = ParseLine(lines[i], 12, " Y+");
            var b = ParseLine(lines[i + 1], 12, " Y+");
            var prize = ParseLine(lines[i + 2], 9, " Y=");
            
            var outcomesA = CalculateOutcomes(a, prize);
            var outcomesB = CalculateOutcomes(b, prize);

            foreach (var (aX, aY) in outcomesA)
            {
                foreach (var (bX, bY) in outcomesB)
                {
                    if (aX + bX == prize[0] && aY + bY == prize[1])
                    {
                        answer += outcomesA.IndexOf((aX, aY)) * 3 + outcomesB.IndexOf((bX, bY));
                    }
                }
            }
        }
                
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static List<(int, int)> CalculateOutcomes(int[] values, int[] prize)
    {
        var outcomes = new List<(int, int)>();

        for (int push = 0; push <= 100; push++)
        {
            int x = values[0] * push;
            int y = values[1] * push;
            if (x <= prize[0] && y <= prize[1])
            {
                outcomes.Add((x, y));
            }
        }

        return outcomes;
    }

    private static int[] ParseLine(string line, int index, string replace)
    {
        return line[index..].Replace(replace, string.Empty).Split(',').Select(double.Parse).Select(d => (int)d).ToArray();
    }
}
