namespace AutoFileSort;

public static class FileSorter
{
    public static string? TargetFolder;

    private static void SetupFolders()
    {
        //see all file types
        var fileExtensions = GetAllFiletypesFromTarget();
        
        //create folder for each filetype
        foreach (var filetype in fileExtensions)
        {
            var dirPath = Path.Combine(TargetFolder!, filetype);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
        }
    }

    private static string[] GetAllFiletypesFromTarget()
    {
        var allFiles = GetAllFilesFromTarget();

        var listOfFiletypes = new List<string>();
        
        foreach (var curFile in allFiles)
        {
            var extension = Path.GetExtension(curFile)[1..];
            if(!listOfFiletypes.Contains(extension)) listOfFiletypes.Add(extension);
        }
        return listOfFiletypes.ToArray();
    }

    private static string[] GetAllFilesFromTarget()
    {
        return (TargetFolder != null ? Directory.GetFiles(TargetFolder, "*.*", SearchOption.TopDirectoryOnly) : null)!;
    }


    // Event Handlers
    public static void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"File {e.Name} created");
        Sort(e.FullPath);
    }

    public static void OnFileRenamed(object sender, RenamedEventArgs e)
    {
        Console.WriteLine($"File {e.Name} renamed");
        Sort(e.FullPath);
    }

    public static void Sort(string filename = "")
    {
        SetupFolders();
        
        //if filename == "" -> sort every file; else sort file filename
        if (filename == "")
        {
            var allFiles = GetAllFilesFromTarget();

            foreach (var curFile in allFiles)
            {
                MoveFile(curFile);
            }
        }
        
        //Move file to dedicated folder
        else MoveFile(filename);
    }

    private static void MoveFile(string source)
    {
        var destination = Path.Combine(TargetFolder, Path.GetExtension(source)[1..]);
        var destinationFile = Path.Combine(destination, Path.GetFileName(source));
        File.Move(source, destinationFile);
    }
}