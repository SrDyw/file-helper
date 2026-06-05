using System.CommandLine;
using System.Runtime.InteropServices;
using fh.CommandOptions;
using fh.Contracts;
using fh.Utils.Commands;

[Command]
public class PinCommand : ICommand
{
    public Command Command { get; set; }

    public PinCommand()
    {
        Command = new Command("pin", "Create a shortcut in the Start Menu for the specified executable");
    }

    public Command Setup()
    {
        var startMenuPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Microsoft", "Windows", "Start Menu", "Programs"
        );

        Command.SetOption(CommonOptions.Path, path =>
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("[ERROR] Path is required");
                return;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine($"[ERROR] File not found: {path}");
                return;
            }

            var extension = Path.GetExtension(path);
            if (!string.Equals(extension, ".exe", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("[ERROR] Only .exe files are supported");
                return;
            }

            var shortcutName = Path.GetFileNameWithoutExtension(path);
            var shortcutPath = Path.Combine(startMenuPath, $"{shortcutName}.lnk");

            try
            {
                CreateShortcut(path, shortcutPath);
                Console.WriteLine($"[SUCCESS] Shortcut created at: {shortcutPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to create shortcut: {ex.Message}");
            }
        });

        return Command;
    }

    private void CreateShortcut(string targetPath, string shortcutPath)
    {
        if (!OperatingSystem.IsWindows()) return;

        var shellType = Type.GetTypeFromProgID("WScript.Shell");
        if (shellType == null) return;

        dynamic shell = Activator.CreateInstance(shellType)!;
        dynamic shortcut = shell.CreateShortcut(shortcutPath);
        shortcut.TargetPath = targetPath;
        shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
        shortcut.Description = Path.GetFileNameWithoutExtension(targetPath);
        shortcut.Save();

        Marshal.FinalReleaseComObject(shortcut);
        Marshal.FinalReleaseComObject(shell);
    }
}
