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
        var answer = string.Empty;

        // SOLVE.
        var numbers = _numbers.ToArray();

        for (int i = 0; i < 25; i++)
        {
            numbers = numbers.SelectMany(Blink).ToArray();
        }

        answer = numbers.Length.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static ulong[] Blink(ulong number)
    {
        if (number == 0)
        {
            return [1];
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
