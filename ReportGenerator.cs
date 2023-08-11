using System;
using System.IO;

public class ReportGenerator
{
    private readonly string outputFolderPath;

    public ReportGenerator(string outputFolderPath)
    {
        this.outputFolderPath = outputFolderPath;
    }

    public void GenerateTextReport(int combinedLetterCount)
    {
        string reportContent = $"Processing Date: {DateTime.Now}\n";
        reportContent += $"Total Combined Letters: {combinedLetterCount}\n";

        string reportPath = Path.Combine(outputFolderPath, "report.txt");
        File.WriteAllText(reportPath, reportContent);
    }
}

