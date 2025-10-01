namespace AutoFileSort;

public static class CLI
{
    public static string GetTargetFolderFromUser()
    {
        Console.WriteLine("What folder would you like to sort?");
        var input = Console.ReadLine() ?? "";
        return input.Trim();
    }
}