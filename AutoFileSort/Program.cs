namespace AutoFileSort;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        
        //Get the target Folder
        var target = CLI.GetTargetFolderFromUser();
        FileSorter.TargetFolder = target;
        
        //Set up the target folder
        FileSorter.SetupFolders();
        FileSorter.Sort("");
        
        //listen for changes
        var watcher = new FileSystemWatcher(target);

        watcher.Created += FileSorter.OnFileCreated;
        watcher.Renamed += FileSorter.OnFileRenamed;
    }
}