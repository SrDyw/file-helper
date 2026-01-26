using System.CommandLine;

namespace fh.CommandOptions;
public static class RenameOptions
{
    public static Option<string> File = new("--file", "-f")
    {
        Description = "Target file"
    };

    public static Option<string> Template = new ("--template", "-tmp")
    {
        Description = "Use a template config"
    };

    public static Option<string> StartWith = new ("--y2Start", "-y2s")
    {
        Description = "Only select y2 files that start with"
    };
}