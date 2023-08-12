using dg.wctool.Models;

namespace dg.wctool.Helpers;

public static class CommandHelper
{
    public static bool IsCommand(string arg)
    {
        return arg.StartsWith("-");
    }

    public static void SetCommands(string arg, Dictionary<Command, bool> options)
    {
        var commands = arg.TrimStart('-');
        foreach (var command in commands.Select(c => c))
        {
            switch (command)
            {
                case 'c':
                    options[Command.CountBytes] = true;
                    break;
                case 'l':
                    options[Command.CountLines] = true;
                    break;
                case 'w':
                    options[Command.CountWords] = true;
                    break;
                case 'm':
                    options[Command.CountCharacters] = true;
                    break;
            }
        }
    }
}