using AoCHelper;

namespace AdventOfCode2024.Console.Solutions;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = "TODO";

        int safeReports = 0;

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => Convert.ToInt32(x)).ToList();
            
            var isAllIncreasing = true;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i != 0)
                {
                    int difference = numbers[i] - numbers[i - 1];
                    if (difference < 1 || difference > 3)
                    {
                        isAllIncreasing = false;
                        break;
                    }
                }
            }

            var isAllDecreasing = true;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i != 0)
                {
                    int difference = numbers[i - 1] - numbers[i];
                    if (difference < 1 || difference > 3)
                    {
                        isAllDecreasing = false;
                        break;
                    }
                }
            }

            if (isAllIncreasing || isAllDecreasing)
            {
                safeReports++;
            }
        }

        answer = safeReports.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
