using System.CommandLine;

namespace fh.CommandOptions;
public static class CommonOptions
{
    public static Option<string> Name = new("--name", "-n")
    {
        Description = "Name to display"
    };
    public static Option<string> Path = new("--path", "-p")
    {
        Description = "Target path"
    };

    public static Option<string> Type = new("--type", "-t")
    {
        Description = "Type of the target element"
    };

    public static Option<bool> Recursive = new("--recursive", "-r")
    {
        Description = "Execute the action for all elements tree"
    };

}