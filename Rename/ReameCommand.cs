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
        Command.SetOptions((Action<ParseResult>)(parsedResult =>
        {
            var path = parsedResult.GetValue(CommonOptions.Path);
            var with = parsedResult.GetValue(RenameOptions.With);
            var to = parsedResult.GetValue(RenameOptions.To);
            var removeParams = parsedResult.GetValue(RenameOptions.RemoveParams);

            if (string.IsNullOrWhiteSpace(path))
            {
                path = Directory.GetCurrentDirectory();
            }
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"[ERROR] Couldn't find the path {path}");
                return;
            }
            Console.WriteLine($"======== Renaming files at {path} ============");

            if (string.IsNullOrEmpty(with))
            {
                with = "y2save.com";
            }

            if (string.IsNullOrWhiteSpace(removeParams))
            {
                removeParams = with;
            }

            if (string.IsNullOrWhiteSpace(to)) to = " ";

            Console.WriteLine($"[StartWith] {with}");
            Console.WriteLine($"[REMOVING] {removeParams}");

            Rename(path, with, to, removeParams.Split(","));
        }), CommonOptions.Path, RenameOptions.RemoveParams, RenameOptions.With, RenameOptions.To);

        return Command;
    }

    public void Rename(string path, string with, string to, params string[] removeParams)
    {
        string[] files = Directory.GetFiles(path);
        IEnumerable<string> selectedFiles = files.Where(x => Path.GetFileName(x).Contains(with));

        if (Path.Exists(path))
        {
            if (selectedFiles.Any() == false)
            {
                Console.WriteLine("[WARNING] No files detected with specified params");
                return;
            }
            foreach (var item in selectedFiles)
            {
                string extension = Path.GetExtension(item);
                string fileName = Path.GetFileName(item);
                string targetName = fileName;

                foreach (var param in removeParams)
                {
                    targetName = targetName.Replace(param, to);
                }

                string targetPath = Path.Combine(path, $"{targetName.Trim()}");

                Console.WriteLine($"[RENAME] {fileName} -> {targetName}");
                File.Move(item, targetPath);
            }
            return;
        }

        Console.WriteLine($"[ERROR] Couldn't find the target path {path}");
    }
}