using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day09 : BaseDay
{
    private readonly string _input;

    private List<int?> _fileMap = [];

    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);

        var index = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var length = int.Parse(_input[i].ToString());
            int? value = null;
            if(i % 2 == 0)
            {
                value = index;
                index++;
            }

            for (int j = 0; j < length; j++)
            {
                _fileMap.Add(value);
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        // SOLVE.
        var nulls = _fileMap.Count(x => !x.HasValue);
        for (int i = 0; i < nulls; i++)
        {
            var firstIndex = _fileMap.FindIndex(x => !x.HasValue);
            var lastIndex = _fileMap.FindLastIndex(x => x.HasValue);
            int? firstValue = _fileMap[firstIndex];
            int? lastValue = _fileMap[lastIndex];
            _fileMap[firstIndex] = lastValue;
            _fileMap[lastIndex] = firstValue;
        }

        ulong filesystemChecksum = 0;
        for (int i = 0; i < _fileMap.Count; i++)
        {
            if (_fileMap[i].HasValue)
            {
                filesystemChecksum += (ulong)(i * _fileMap[i]!.Value);
            }
            else
            {
                break;
            }
        }

        answer = filesystemChecksum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        // SOLVE.

        answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
