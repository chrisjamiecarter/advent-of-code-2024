using AoCHelper;

namespace AdventOfCode2024.Console.Solutions;

public class Day04 : BaseDay
{
    private readonly string _input;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var answer = "TODO";

        int wordCount = 0;

        var grid = GetGrid();

        int rowCount = grid.GetLength(0);
        int colCount = grid.GetLength(1);

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                if (grid[row, col] == Word[0])
                {
                    foreach (var direction in Directions)
                    {
                        if (CheckWord(grid, row, col, direction))
                        {
                            wordCount++;
                        }
                    }
                }
            }
        }

        answer = wordCount.ToString();
                
        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 1 = '{answer}'");
    }


    public override ValueTask<string> Solve_2()
    {
        var answer = "TODO";

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static bool CheckWord(char[,] grid, int row, int col, (int Row, int Col) direction)
    {
        int rowCount = grid.GetLength(0);
        int colCount = grid.GetLength(1);

        for (int i = 0; i < Word.Length; i++)
        {
            int checkRow = row + i * direction.Row;
            int checkCol = col + i * direction.Col;

            if (checkRow < 0 || checkRow >= rowCount || checkCol < 0 || checkCol >= colCount || grid[checkRow, checkCol] != Word[i])
            {
                return false;
            }
        }

        return true;
    }

    private static readonly (int Row, int Col)[] Directions =
    [
        (-1, 0),
        (-1, 1),
        (0, 1),
        (1, 1),
        (1, 0),
        (1, -1),
        (0, -1),
        (-1, -1),
    ];

    private static string Word => "XMAS";

    private char[,] GetGrid()
    {
        var rows = _input.Split(Environment.NewLine);

        var grid = new char[rows.Length, rows.Length];

        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[i].Length; j++)
            {
                grid[i, j] = rows[i][j];
            }
        }

        return grid;
    }
}
