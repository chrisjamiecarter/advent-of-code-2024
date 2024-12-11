using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day11 : BaseDay
{
    private readonly string _input;

    private readonly ulong[] _numbers;

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
        _numbers = _input.Split(' ').Select(ulong.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = PerformBlinks(25);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        // TODO: This takes far too long and is way too memory intensive. I may need to seek help.
        var answer = PerformBlinks(75);

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private string PerformBlinks(int blinks)
    {
        var numbers = new List<ulong>(_numbers);
        for (int i = 0; i < blinks; i++)
        {
            var nextNumbers = new List<ulong>(numbers.Count * 2);
            Parallel.ForEach(numbers, number =>
            {
                lock (nextNumbers) nextNumbers.AddRange(Blink(number));
            });
            //foreach (var number in numbers)
            //{
            //    nextNumbers.AddRange(Blink(number));
            //}
            numbers = nextNumbers;
        }
        return numbers.Count.ToString();
    }

    private static IEnumerable<ulong> Blink(ulong number)
    {
        if (number == 0)
        {
            return [1UL];
        }

        var numberOfDigits = Math.Floor(Math.Log10(number)) + 1;
        if (numberOfDigits % 2 == 0)
        {
            ulong divisor = (ulong)Math.Pow(10, numberOfDigits / 2);
            return [number / divisor, number % divisor];
        }

        return [number * 2024];
    }
}
