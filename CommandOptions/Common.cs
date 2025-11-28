using System.CommandLine;

namespace fh.CommandOptions;
public static class CommonOptions
{
    public static Option<string> Name = new("--name")
    {
        Description = "Name to display"
    };
}