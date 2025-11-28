using System.CommandLine;
using fh.CommandOptions;

namespace fh.Utils.Commands;
public static class CommandUtils
{
    public static void AddNameOption(this Command command, Action<string> action)
    {
        command.Options.Add(CommonOptions.Name);
        command.SetAction(parsedResult =>
        {
            var name = parsedResult.GetValue(CommonOptions.Name) ?? "";
            action(name);
        });
    }

    public static void SetOption<T>(this Command command, Option<T> option, Action<T?> action)
    {
        command.Options.Add(option);
        command.SetAction(parsedResult =>
        {
            var name = parsedResult.GetValue<T>(option);
            action(name);
        });
    }

    public static void SetOptions(this Command command, Action<ParseResult> action, params Option[] options)
    {
        foreach (var option in options) command.Options.Add(option);

        command.SetAction(parsedResult => {
            action(parsedResult);
        });
    }

}