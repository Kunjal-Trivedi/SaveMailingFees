using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LetterProcessor 
{
    private readonly string inputFolderPath;
    private readonly string outputFolderPath;
    public int CombinedLetterCount { get; private set; }

    public LetterProcessor(string inputFolderPath, string outputFolderPath)
    {
        this.inputFolderPath = inputFolderPath;
        this.outputFolderPath = outputFolderPath;
    }

    public void ProcessLetters()
    {
        string[] datedFolders = Directory.GetDirectories(inputFolderPath, "yyyyMMdd", SearchOption.AllDirectories);

        foreach (string datedFolder in datedFolders)
        {
            ProcessDatedFolder(datedFolder);
        }
    }

    private void ProcessDatedFolder(string datedFolder)
    {
        string[] admissionLetters = Directory.GetFiles(Path.Combine(datedFolder, "Admission"), "admission-*.txt");
        string[] scholarshipLetters = Directory.GetFiles(Path.Combine(datedFolder, "Scholarship"), "scholarship-*.txt");

        foreach (string admissionLetter in admissionLetters)
        {
            string studentId = Path.GetFileNameWithoutExtension(admissionLetter).Substring("admission-".Length);
            string matchingScholarshipLetter = scholarshipLetters.FirstOrDefault(s => s.EndsWith($"{studentId}.txt"));

            if (matchingScholarshipLetter != null)
            {
                CombineAndSaveLetters(admissionLetter, matchingScholarshipLetter, studentId);
                CombinedLetterCount++;
            }
        }
    }

    private void CombineAndSaveLetters(string admissionLetterPath, string scholarshipLetterPath, string studentId)
    {
        string admissionContent = File.ReadAllText(admissionLetterPath);
        string scholarshipContent = File.ReadAllText(scholarshipLetterPath);
        string combinedContent = admissionContent + Environment.NewLine + scholarshipContent;

        string combinedLetterPath = Path.Combine(outputFolderPath, $"{studentId}.txt");
        File.WriteAllText(combinedLetterPath, combinedContent);
    }
}
