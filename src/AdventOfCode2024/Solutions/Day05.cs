using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day05 : BaseDay
{
    private readonly string _input;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        var middlePageNumbersSum = 0;

        var pageOrderRules = _input.Split($"{Environment.NewLine}{Environment.NewLine}").First();
        var pageUpdates = _input.Split($"{Environment.NewLine}{Environment.NewLine}").Last();

        foreach (var update in pageUpdates.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            if (CheckRules(update, pageOrderRules))
            {
                var pageNumbers = update.Split(',');
                var middleIndex = (pageNumbers.Length / 2);
                var middleNumber = pageNumbers[middleIndex];
                middlePageNumbersSum += Convert.ToInt32(middleNumber);
            }            
        }

        answer = middlePageNumbersSum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    private static bool CheckRules(string pageUpdate, string pageOrderRules)
    {
        foreach (var rule in pageOrderRules.Split(Environment.NewLine))
        {
            var x = rule.Split('|').First();
            var y = rule.Split('|').Last();
            var xIndex = pageUpdate.IndexOf(x);
            var yIndex = pageUpdate.IndexOf(y);

            if ((xIndex != -1 && yIndex != -1) && xIndex >= yIndex)
            {
                return false;
            }
        }

        return true;
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
