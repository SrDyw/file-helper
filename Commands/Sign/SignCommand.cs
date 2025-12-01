using fh.Contracts;
using System.CommandLine;
using fh.Utils.Commands;
using fh.CommandOptions;
using System.Diagnostics;

[Command]
public class SignCommand : ICommand
{
    public Command Command { get; set; }

    public SignCommand()
    {
        Command = new Command("sign", "Call web app Digital Sign Helper");
    }

    public Command Setup()
    {
        var currentDir = Directory.GetCurrentDirectory();

        Command.SetOptions(pr =>
        {
            Console.WriteLine("Executing digital sign helper");
            Process.Start(Path.Combine(currentDir, "DigitalSignHelper.exe"));
        });
        return Command;
    }
}