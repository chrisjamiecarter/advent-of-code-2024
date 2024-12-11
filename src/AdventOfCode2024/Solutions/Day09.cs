using AoCHelper;

namespace AdventOfCode2024.Solutions;

public class Day09 : BaseDay
{
    private readonly string _input;

    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    private List<int> GetFileMap()
    {
        List<int> fileMap = [];

        for (int i = 0, id = 0; i < _input.Length; i++)
        {
            var length = int.Parse(_input[i].ToString());
            int value = -1;
            if (i % 2 == 0)
            {
                value = id;
                id++;
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
        var freeSpaceIndex = 0;

        for (int i = fileMap.Count - 1; i >= 0; i--)
        {
            if (fileMap[i] >= 0)
            {
                freeSpaceIndex = GetFirstFreeSpaceIndex(fileMap, freeSpaceIndex, i);

                if (freeSpaceIndex < 0)
                {
                    break;
                }

                fileMap[freeSpaceIndex] = fileMap[i];
                fileMap[i] = -1;
            }
        }

        ulong filesystemChecksum = 0;
        for (int i = 0; fileMap[i] >= 0; i++)
        {
            filesystemChecksum += (ulong)(i * fileMap[i]);
        }

        answer = filesystemChecksum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }

    public override ValueTask<string> Solve_2()
    {
        var answer = string.Empty;

        var fileMap = GetFileMap();
        var firstEmptyBlockIndex = 0;

        for (int i = fileMap.Count - 1, id = int.MaxValue; i >= 0; i--)
        {
            if (fileMap[i] >= 0 && fileMap[i] < id)
            {
                var fileStartIndex = i;
                for (var j = i; j >= 0 && fileMap[j] == fileMap[i]; j--)
                {
                    fileStartIndex = j;
                }

                var length = i - fileStartIndex + 1;
                id = fileMap[i];

                var (FreeSpaceIndex, FirstEmptyBlockIndex) = GetFirstFreeSpaceIndex(fileMap, firstEmptyBlockIndex, i, length);
                var freeSpaceIndex = FreeSpaceIndex;
                firstEmptyBlockIndex = FirstEmptyBlockIndex;
                if (freeSpaceIndex >= 0)
                {
                    for (var j = 0; j < length; j++)
                    {
                        fileMap[freeSpaceIndex + j] = fileMap[i];
                        fileMap[fileStartIndex + j] = -1;
                    }
                }

                i = fileStartIndex;
            }
        }

        ulong filesystemChecksum = 0;
        for (int i = 0; i < fileMap.Count; i++)
        {
            if (fileMap[i] >= 0)
            {
                filesystemChecksum += (ulong)(i * fileMap[i]);
            }
        }

        answer = filesystemChecksum.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static int GetFirstFreeSpaceIndex(List<int> fileMap, int freeSpaceIndex, int maxIndex)
    {
        for (int i = freeSpaceIndex; i < maxIndex; i++)
        {
            if (fileMap[i] == -1)
            {
                return i;
            }
        }

        return -1;
    }

    private static (int FirstFreeSpaceIndex, int FirstEmptyBlockIndex) GetFirstFreeSpaceIndex(List<int> fileMap, int firstEmptyBlockIndex, int maxIndex, int length)
    {
        var firstEmptyBlockFound = false;
        for (int i = firstEmptyBlockIndex; i < maxIndex; i++)
        {
            if(fileMap[i] == -1)
            {
                if (!firstEmptyBlockFound)
                {
                    firstEmptyBlockFound = true;
                    firstEmptyBlockIndex = i;
                }

                var doesFileFit = true;
                for (int j = 0; j < length; j++)
                {
                    if (fileMap[i + j] >= 0)
                    {
                        doesFileFit = false;
                    }
                }

                if (doesFileFit)
                {
                    return (i, firstEmptyBlockIndex);
                }
            }
        }

        return (-1, firstEmptyBlockIndex);
    }
}
