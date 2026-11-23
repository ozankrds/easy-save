namespace EasySave;

public sealed class BackupService
{
    public string CreateBackup(string[] sourceFilePaths, string destinationRoot)
    {
        // Check if there is no file selected
        if (sourceFilePaths.Length == 0)
            throw new ArgumentException("No file is selected");

        // Check if the destination directory is valid
        if (!Directory.Exists(destinationRoot))
            throw new DirectoryNotFoundException(destinationRoot);

        // Check if all the source files are valid 
        foreach(string source in sourceFilePaths)
        {
            if(!File.Exists(source))
                throw new FileNotFoundException(source);
        }

        int number = 1;
        string backupFolder;

        do
        {
            backupFolder = Path.Combine(destinationRoot, $"({number})");
            number++;

        } while (Directory.Exists(backupFolder));

        Directory.CreateDirectory(backupFolder);

        foreach(string source in sourceFilePaths)
        {
            string target = Path.Combine(backupFolder, Path.GetFileName(source));
            File.Copy(source, target);
        }

        return backupFolder;
    }
}