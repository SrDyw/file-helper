using System.CommandLine;

var rootCommand = new RootCommand("Test");

Option<string> nameOption = new("--name")
{
    Description = "Name to display on console"
};


var greetingCommand = new Command("greeting", description: "Greet the user")
{
    nameOption
};

rootCommand.Subcommands.Add(greetingCommand);
ParseResult parseResult = rootCommand.Parse(args);

greetingCommand.SetAction(parseResult =>
{
    string? n = parseResult.GetValue(nameOption);
    Console.WriteLine("Hello " + n);
    return 0;
});

ParseResult pr = rootCommand.Parse(args);
return parseResult.Invoke();
