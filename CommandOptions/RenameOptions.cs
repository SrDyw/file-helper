using System.CommandLine;

namespace fh.CommandOptions;
public static class RenameOptions
{
    public static Option<string> Template = new ("--template", "-tmp")
    {
        Description = "Use a template config"
    };

    public static Option<string> With = new ("--with", "-w")
    {
        Description = "Only select files that start with selected name"
    };

    public static Option<string> To = new ("--to", "-to"){
        Description = "Values to change"
    };

    public static Option<string> RemoveParams = new ("--remove-string", "-rstr")
    {
        Description = "Target values to remove"
    };

}