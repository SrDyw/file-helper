using fh.Contracts;
using System.CommandLine;
using fh.Utils.Commands;
using fh.CommandOptions;

[Command]
public class CreateCommand : ICommand
{
    public Command Command { get; set; }

    public CreateCommand()
    {
        Command = new Command("create", "Create a new directory");
    }

    public Command Setup()
    {
        Command.SetOption(CommonOptions.Name, name =>
        {
            Console.WriteLine("Creating new directory named " + name);
        });
        return Command;
    }
}