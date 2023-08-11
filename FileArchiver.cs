using System;
using System.IO;

public class FileArchiver
{
    private readonly string inputFolderPath;
    private readonly string archiveFolderPath;

    public FileArchiver(string inputFolderPath, string archiveFolderPath)
    {
        this.inputFolderPath = inputFolderPath;
        this.archiveFolderPath = archiveFolderPath;
    }

    public void ArchiveFiles()
    {
        string[] filesToArchive = Directory.GetFiles(inputFolderPath, "*.txt", SearchOption.AllDirectories);

        foreach (string file in filesToArchive)
        {
            string destinationPath = file.Replace(inputFolderPath, archiveFolderPath);
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
            File.Move(file, destinationPath);
        }
    }
}
