using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day09 : BaseDay
{
    private readonly string _input;

    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private List<int?> GetFileMap()
    {
        List<int?> fileMap = [];

        var index = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            var length = int.Parse(_input[i].ToString());
            int? value = null;
            if (i % 2 == 0)
            {
                value = index;
                index++;
            }

            for (int j = 0; j < length; j++)
            {
                fileMap.Add(value);
            }
        }

        return fileMap;
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = string.Empty;

        var fileMap = GetFileMap();

        // SOLVE.
        var nulls = fileMap.Count(x => !x.HasValue);
        for (int i = 0; i < nulls; i++)
        {
            var firstIndex = fileMap.FindIndex(x => !x.HasValue);
            var lastIndex = fileMap.FindLastIndex(x => x.HasValue);
            int? firstValue = fileMap[firstIndex];
            int? lastValue = fileMap[lastIndex];
            fileMap[firstIndex] = lastValue;
            fileMap[lastIndex] = firstValue;
        }

        ulong filesystemChecksum = 0;
        for (int i = 0; i < fileMap.Count; i++)
        {
            if (fileMap[i].HasValue)
            {
                filesystemChecksum += (ulong)(i * fileMap[i]!.Value);
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

        var fileMap = GetFileMap();

        // SOLVE.
        for (int i = fileMap.Count - 1; i >= 0; i--)
        {
            if (fileMap[i].HasValue)
            {
                int compare = fileMap[i]!.Value;
                List<int> firstIndexes = [];
                List<int> lastIndexes = [];
                int tempIndex = i;
                while (tempIndex >= 0 && fileMap[tempIndex].HasValue && fileMap[tempIndex]!.Value == compare)
                {
                    lastIndexes.Add(tempIndex);
                    tempIndex--;
                    i = tempIndex + 1;
                }

                if (!lastIndexes.Any())
                {
                    continue;
                }

                List<int> compareIndexes = [];
                for (int j = 0; j < fileMap.Count; j++)
                {
                    if (j > i)
                    {
                        compareIndexes = [];
                        break;
                    }

                    if (!fileMap[j].HasValue)
                    {
                        compareIndexes.Add(j);
                    }
                    else
                    {
                        if (lastIndexes.Count <= compareIndexes.Count)
                        {
                            firstIndexes = compareIndexes;
                            break;
                        }
                        else
                        {
                            compareIndexes = [];
                        }    
                    }
                }

                if (!firstIndexes.Any())
                {
                    continue;
                }

                for (int k = 0; k < lastIndexes.Count; k++)
                {
                    var firstIndex = firstIndexes[k];
                    var lastIndex = lastIndexes[k];
                    int? firstValue = fileMap[firstIndex];
                    int? lastValue = fileMap[lastIndex];
                    fileMap[firstIndex] = lastValue;
                    fileMap[lastIndex] = firstValue;
                }
            }
        }

        ulong filesystemChecksum = 0;
        for (int i = 0; i < fileMap.Count; i++)
        {
            if (fileMap[i].HasValue)
            {
                filesystemChecksum += (ulong)(i * fileMap[i]!.Value);
            }
        }

        answer = filesystemChecksum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }
}
