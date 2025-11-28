using System.CommandLine;
using fh.Commands.Greeting;
using System.Reflection;

var rootCommand = new RootCommand("File Helper");

var types = Assembly.GetExecutingAssembly().GetTypes();
var commands = types
    .Where(x => x.GetCustomAttribute<CommandAttribute>() != null)
    .Select(x =>  System.Activator.CreateInstance(x) as fh.Contracts.ICommand);

foreach (var command in commands)
{
    if (command == null) continue;
    rootCommand.Subcommands.Add(command.Setup());
}

var parseResult = rootCommand.Parse(args);
return parseResult.Invoke();
