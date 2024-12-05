using AoCHelper;

namespace AdventOfCode2024.Solutions;

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

            var isAllIncreasing = IsAllIncreasing(numbers);

            var isAllDecreasing = IsAllDecreasing(numbers);

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

        int safeReports = 0;

        foreach (var line in _input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(x => Convert.ToInt32(x)).ToList();

            var isAllIncreasing = IsAllIncreasingWithProblemDampening(numbers);

            var isAllDecreasing = IsAllDecreasingWithProblemDampening(numbers);

            if (isAllIncreasing || isAllDecreasing)
            {
                safeReports++;
            }
        }

        answer = safeReports.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static bool IsAllDecreasing(List<int> numbers)
    {
        for (int j = 0; j < numbers.Count; j++)
        {
            if (j != 0)
            {
                int difference = numbers[j - 1] - numbers[j];
                if (difference < 1 || difference > 3)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsAllDecreasingWithProblemDampening(List<int> numbers)
    {
        List<bool> outcomes = [];
        for (int i = 0; i < numbers.Count; i++)
        {
            var dampenedNumbers = numbers.ToList();
            dampenedNumbers.RemoveAt(i);
            outcomes.Add(IsAllDecreasing(dampenedNumbers));
        }

        return outcomes.Any(x => x);
    }

    private static bool IsAllIncreasing(List<int> numbers)
    {
        for (int j = 0; j < numbers.Count; j++)
        {
            if (j != 0)
            {
                int difference = numbers[j] - numbers[j - 1];
                if (difference < 1 || difference > 3)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsAllIncreasingWithProblemDampening(List<int> numbers)
    {
        List<bool> outcomes = [];
        for (int i = 0; i < numbers.Count; i++)
        {
            var dampenedNumbers = numbers.ToList();
            dampenedNumbers.RemoveAt(i);
            outcomes.Add(IsAllIncreasing(dampenedNumbers));
        }

        return outcomes.Any(x => x);
    }
}
