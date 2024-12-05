using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day05 : BaseDay
{
    private readonly string _input;

    private readonly Dictionary<int, List<int>> _pageOrderRules;

    private readonly List<List<int>> _pageUpdates;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);

        _pageOrderRules = _input.Split($"{Environment.NewLine}{Environment.NewLine}")
            .First()
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => x.Split('|').Select(int.Parse).ToList())
            .Select(x => (x[0], x[1]))
            .GroupBy(x => x.Item1)
            .ToDictionary(x => x.Key, x => x.Select(x => x.Item2).Order().ToList());

        _pageUpdates = _input.Split($"{Environment.NewLine}{Environment.NewLine}")
            .Last()
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(x => x.Split(',').Select(int.Parse).ToList())
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        var middlePageNumbersSum = 0;

        foreach (var update in _pageUpdates)
        {
            if (IsUpdateValid(update, _pageOrderRules))
            {
                middlePageNumbersSum += update[update.Count / 2];
            }
        }

        answer = middlePageNumbersSum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        var middlePageNumbersSum = 0;

        foreach (var update in _pageUpdates)
        {
            if (!IsUpdateValid(update, _pageOrderRules))
            {
                while (!IsUpdateValid(update, _pageOrderRules))
                {
                    ReorderUpdate(update, _pageOrderRules);
                }
                middlePageNumbersSum += update[update.Count / 2];
            }
        }

        answer = middlePageNumbersSum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static bool IsPageOrderValid(Dictionary<int, List<int>> rules, int page, int comparisionPage)
    {
        return !rules.TryGetValue(comparisionPage, out var subsquentPages) || !subsquentPages.Contains(page);
    }

    private static bool IsUpdateValid(List<int> update, Dictionary<int, List<int>> rules)
    {
        for (int i = 0; i < update.Count; i++)
        {
            var page = update[i];

            for (var j = i + 1; j < update.Count; j++)
            {
                var comparisionPage = update[j];

                if (!IsPageOrderValid(rules, page, comparisionPage))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void ReorderUpdate(List<int> update, Dictionary<int, List<int>> rules)
    {
        for (var i = 0; i < update.Count; i++)
        {
            var page = update[i];

            for (var j = i + 1; j < update.Count; j++)
            {
                var comparisionPage = update[j];

                if (!IsPageOrderValid(rules, page, comparisionPage))
                {
                    update[i] = comparisionPage;
                    update[j] = page;
                    return;
                }
            }
        }
    }
}
