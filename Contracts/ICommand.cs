using System.CommandLine;

namespace fh.Contracts;

public interface ICommand
{
    Command Setup();
}