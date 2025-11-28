using fh.Contracts;
using System.CommandLine;
using fh.Utils.Commands;
using fh.CommandOptions;
using System.Text;

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
        var currentDir = Directory.GetCurrentDirectory();

        Command.SetOptions(parsedResult =>
        {
            var name = parsedResult.GetValue(CommonOptions.Name);
            var type = parsedResult.GetValue(CommonOptions.Type);

            ArgumentNullException.ThrowIfNull(name);

            var requestPath = name.Split('/');
            var folders = requestPath.Length > 1 ? requestPath.SkipLast(1) : new string[] { };
            var file = requestPath[^1];

            StringBuilder sb = new(currentDir);

            foreach (var f in folders)
            {
                sb.Append($"/{f}");
                CreateFolder(name: f, path: sb.ToString());
            }

            var fileType = string.IsNullOrEmpty(type) ? "" : $".{type}";
            File.WriteAllText(Path.Combine(sb.ToString(), $"{file}{fileType}"), "");
        }, CommonOptions.Name, CommonOptions.Type);

        return Command;
    }

    private void CreateFolder(string name, string path)
    {
        if (!Directory.Exists(path))
        {
            var file = Directory.CreateDirectory(path);
        }
    }
}