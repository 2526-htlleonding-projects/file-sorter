using System;
using System.IO;
using System.Threading;

namespace AutoFileSort
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AutoFileSort.exe <target dir>");
                return;
            }
            Console.WriteLine("Welcome to the Auto File Sort!");
            
            // Get the target folder
            var target = args[1];
            FileSorter.TargetFolder = target;
            
            // Set up the target folder
            FileSorter.Sort("");
            
            // Listen for changes
            using var watcher = new FileSystemWatcher(target);
            watcher.IncludeSubdirectories = false; // or true if you want subfolders
            watcher.EnableRaisingEvents = true;

            watcher.Created += FileSorter.OnFileCreated;
            watcher.Renamed += FileSorter.OnFileRenamed;

            Console.WriteLine("Watching for changes in " + target);

            // Keep the main thread alive
            Thread.Sleep(Timeout.Infinite);
        }
    }
}