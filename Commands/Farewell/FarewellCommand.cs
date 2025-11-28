using fh.Contracts;
using System.CommandLine;
using fh.Utils.Commands;
using fh.CommandOptions;

[Command]
public class FarewellCommand : ICommand
{
    public Command Command { get; set; }

    public FarewellCommand()
    {
        Command = new Command("farewell", "Command to give a farewell to user");
    }

    public Command Setup()
    {
        Command.SetOption(CommonOptions.Name, name =>
        {
            Console.WriteLine("Goodbye " + name);
        });
        return Command;
    }
}