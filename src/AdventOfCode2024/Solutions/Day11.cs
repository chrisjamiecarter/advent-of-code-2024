using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day11 : BaseDay
{
    private readonly string _input;

    private readonly Dictionary<ulong, ulong> _stones = [];
    private readonly Dictionary<ulong, List<ulong>> _cache = [];

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);

        foreach(var stone in _input.Split(' ').Select(ulong.Parse))
        {
            _stones.Add(stone, 1UL);
        }
    }

    public override ValueTask<string> Solve_1()
    {
        _cache.Clear();
        var answer = PerformBlinks(25);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        _cache.Clear();
        var answer = PerformBlinks(75);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private string PerformBlinks(int blinks)
    {
        Dictionary<ulong, ulong> stones = _stones.ToDictionary();

        for (int i = 0; i < blinks; i++)
        {
            Dictionary<ulong, ulong> stonesSnapshot = [];
            foreach (var stone in stones.Keys)
            {
                var multiplier = stones[stone];
                foreach (var newStone in Blink(stone))
                {
                    if (!stonesSnapshot.TryAdd(newStone, multiplier))
                    {
                        stonesSnapshot[newStone] += multiplier;
                    }
                }
            }
            stones = stonesSnapshot;
        }

        var answer = 0UL;
        foreach (var stone in stones.Keys)
        {
            answer += stones[stone];
        }    

        return answer.ToString();
    }

    private List<ulong> Blink(ulong engraving)
    {
        if (_cache.TryGetValue(engraving, out List<ulong>? value))
        {
            return value;
        }

        if (engraving == 0)
        {
            return [1UL];
        }

        var numberOfDigits = Math.Floor(Math.Log10(engraving)) + 1;
        if (numberOfDigits % 2 == 0)
        {
            ulong divisor = (ulong)Math.Pow(10, numberOfDigits / 2);
            return [engraving / divisor, engraving % divisor];
        }

        return [engraving * 2024];
    }
}
