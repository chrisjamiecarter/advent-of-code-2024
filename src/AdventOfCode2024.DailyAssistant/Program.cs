using System.Runtime.CompilerServices;

namespace AdventOfCode.DailyAssistant;

internal class Program
{
    static void Main(string[] args)
    {
        string day = DateTime.Now.Day.ToString("D2");

        string sourceDirectoryPath = "..\\..\\..\\..\\AdventOfCode2024";

        string sourceInputFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Inputs", "00.txt"));
        string sourceProblemFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Problems", "00.md"));
        string sourceSolutionFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Solutions", "Day00.cs"));

        string targetInputFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Inputs", $"{day}.txt"));
        string targetProblemFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Problems", $"{day}.md"));
        string targetSolutionFilePath = Path.GetFullPath(Path.Combine(sourceDirectoryPath, "Solutions", $"Day{day}.cs"));

        File.Copy(sourceInputFilePath, targetInputFilePath, true);
        File.Copy(sourceProblemFilePath, targetProblemFilePath, true);

        string solutionContent = File.ReadAllText(sourceSolutionFilePath);
        solutionContent = solutionContent.Replace("Day00", $"Day{day}");
        File.WriteAllText(targetSolutionFilePath, solutionContent);
    }
}
