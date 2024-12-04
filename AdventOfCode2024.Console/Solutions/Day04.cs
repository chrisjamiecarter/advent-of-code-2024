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

        string word = "XMAS";
        int wordCount = 0;

        var grid = GetGrid();

        int rowCount = grid.GetLength(0);
        int colCount = grid.GetLength(1);

        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                if (grid[row, col] == word[0])
                {
                    foreach (var direction in Directions)
                    {
                        if (CheckWord(word, grid, row, col, direction))
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

        // NOTE: specialised to find XMAS, but MAS in an X shape, i.e.,
        // M . S
        // . A .
        // M . S

        int wordCount = 0;
        
        var grid = GetGrid();

        int rowCount = grid.GetLength(0);
        int colCount = grid.GetLength(1);

        // NOTE: As we are searching for the middle letter, we do not care for the edges/boundary.
        for (int row = 1; row < rowCount - 1; row++)
        {
            for (int col = 1; col < colCount - 1; col++)
            {
                if (grid[row, col] is 'A')
                {
                    char ne = grid[row - 1, col + 1];
                    char se = grid[row + 1, col + 1];
                    char nw = grid[row - 1, col - 1];
                    char sw = grid[row + 1, col - 1];
                    
                    if (((ne is 'M' && sw is 'S') || (ne is 'S' && sw is 'M')) && ((nw is 'M' && se is 'S') || (nw is 'S' && se is 'M')))
                    {
                        wordCount++;
                    }
                }
            }
        }

        answer = wordCount.ToString();

        return new($"Solution to {ClassPrefix} {CalculateIndex()}, part 2 = '{answer}'");
    }

    private static bool CheckWord(string word, char[,] grid, int row, int col, (int Row, int Col) direction)
    {
        int rowCount = grid.GetLength(0);
        int colCount = grid.GetLength(1);

        for (int i = 0; i < word.Length; i++)
        {
            int checkRow = row + i * direction.Row;
            int checkCol = col + i * direction.Col;

            if (checkRow < 0 || checkRow >= rowCount || checkCol < 0 || checkCol >= colCount || grid[checkRow, checkCol] != word[i])
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
