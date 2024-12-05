using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = "TODO";

        var sums = 0;

        var regex = new Regex(@"(?'operand'mul)\((?'numberone'\d+),(?'numbertwo'\d+)\)");

        var matches = regex.Matches(_input);
        foreach (Match match in matches)
        {
            var numberOne = Convert.ToInt32(match.Groups["numberone"].Value);
            var numberTwo = Convert.ToInt32(match.Groups["numbertwo"].Value);
            sums += numberOne * numberTwo;
        }

        answer = sums.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = "TODO";

        ulong sums = 0;

        var strings = _input.Split("do()");
        foreach (var s in strings)
        {
            var doString = s.Split("don't()").First();

            var regex = new Regex(@"(?'operand'mul)\((?'numberone'\d+),(?'numbertwo'\d+)\)");

            var matches = regex.Matches(doString);
            foreach (Match match in matches)
            {
                var numberOne = Convert.ToUInt64(match.Groups["numberone"].Value);
                var numberTwo = Convert.ToUInt64(match.Groups["numbertwo"].Value);
                sums += numberOne * numberTwo;
            }
        }

        answer = sums.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
