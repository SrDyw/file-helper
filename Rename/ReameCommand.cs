using System.CommandLine;
using fh.CommandOptions;
using fh.Contracts;
using fh.Utils.Commands;

[Command]
public class RenameCommand : ICommand
{
    public Command Command { get; set; }

    public RenameCommand()
    {
        Command = new Command("rename", "Rename files using flags config");
    }
    public Command Setup()
    {
        Command.SetOptions(parsedResult =>
        {
            var targetFile = parsedResult.GetValue(RenameOptions.File);
            var template = parsedResult.GetValue(RenameOptions.Template);
            var startWith = parsedResult.GetValue(RenameOptions.StartWith);

            Console.WriteLine($"[TEMPLATE] {template}");
            Console.WriteLine($"Renaming file {targetFile}");

            if (string.IsNullOrWhiteSpace(targetFile))
            {
                targetFile = Directory.GetCurrentDirectory();
            }

            if (string.IsNullOrEmpty(startWith))
            {
                startWith = "y2save.com";
            }

            Console.WriteLine($"[StartWith] {startWith}");

            if (template == "y2save")
            {
                Y2TemplateRename(targetFile, startWith);
            }


        }, RenameOptions.File, RenameOptions.Template, RenameOptions.StartWith);

        return Command;
    }

    public void Rename(string? path, string? template)
    {

    }

    public void Y2TemplateRename(string path, string startWith)
    {
        string[] files = Directory.GetFiles(path);
        IEnumerable<string> y2files = files.Where(x => x.Split("\\").Any(x => x.StartsWith(startWith)));

        if (Path.Exists(path))
        {
            foreach (var item in y2files)
            {
                string extension = Path.GetExtension(item);
                string fileName = Path.GetFileName(item);
                string targetName = fileName
                    .Replace("-", " ")
                    .Replace(startWith, "")
                    .Trim();

                string targetPath = Path.Combine(path, $"{targetName}");

                Console.WriteLine($"[RENAME] {fileName} -> {targetName}");
                File.Move(item, targetPath);
            }
            return;
        }

        Console.WriteLine($"[ERROR] Couldn't find the target path {path}");
    }
}