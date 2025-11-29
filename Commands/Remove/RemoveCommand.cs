using fh.Contracts;
using System.CommandLine;
using fh.Utils.Commands;
using fh.CommandOptions;

[Command]
public class RemoveCommand : ICommand
{
    public Command Command { get; set; }

    public RemoveCommand()
    {
        Command = new Command("remove", "Remove a directory");
    }

    public Command Setup()
    {
        var currentDir = Directory.GetCurrentDirectory();

        Command.SetOptions(pr =>
        {
            var name = pr.GetValue(CommonOptions.Name);
            var recursive = pr.GetValue(CommonOptions.Recursive);

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Directory name can be empty");
                return;
            }
            try
            {
                var isFile = name.Split('.').Length > 1;

                if (isFile)
                {
                    File.Delete(name);
                    Console.WriteLine($"File {name} removed successfully");
                }
                else
                {
                    Directory.Delete(Path.Combine(currentDir, name), recursive);
                    Console.WriteLine($"Directory {name} removed successfully");
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory named {name} does not exists");
            }
        }, CommonOptions.Name, CommonOptions.Recursive);
        return Command;
    }
}