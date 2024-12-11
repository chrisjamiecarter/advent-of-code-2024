namespace AdventOfCode.DailyAssistant;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            string day = DateTime.Now.Day.ToString("D2");

            Console.WriteLine($"Processing day: {day}");

            string sourceDirectoryPath = "C:\\DEV\\personal\\advent-of-code-2024\\src\\AdventOfCode2024";

            string sourceInputFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Template", "00.txt"));
            string sourceProblemFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Template", "00.md"));
            string sourceSolutionFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Template", "Day00.txt"));

            string targetInputFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Inputs", $"{day}.txt"));
            string targetProblemFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Problems", $"{day}.md"));
            string targetSolutionFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Solutions", $"Day{day}.cs"));

            if (!File.Exists(targetInputFilePath))
            {
                File.Copy(sourceInputFilePath, targetInputFilePath, true);
                Console.WriteLine($"Created: {Path.GetFileName(targetInputFilePath)}");
            }

            if (!File.Exists(targetProblemFilePath))
            {
                File.Copy(sourceProblemFilePath, targetProblemFilePath, true);
                Console.WriteLine($"Created: {Path.GetFileName(targetProblemFilePath)}");
            }

            if (!File.Exists(targetSolutionFilePath))
            {
                string solutionContent = File.ReadAllText(sourceSolutionFilePath);
                solutionContent = solutionContent.Replace("Day00", $"Day{day}");
                File.WriteAllText(targetSolutionFilePath, solutionContent);
                Console.WriteLine($"Created: {Path.GetFileName(targetSolutionFilePath)}");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
