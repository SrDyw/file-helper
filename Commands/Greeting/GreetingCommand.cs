using fh.Contracts;
using System.CommandLine;
using fh.CommandOptions;
using fh.Utils.Commands;

namespace fh.Commands.Greeting;

[Command]
class GreetingCommand : ICommand
{
    protected Command Command { get; set; }
    public GreetingCommand()
    {
        Command = new Command("greetings", "Command to greet the user");
    }

    public Command Setup()
    {
        Command.AddNameOption(name => {
            Console.WriteLine("Hello " + name);
        });
        return Command;
    }
}