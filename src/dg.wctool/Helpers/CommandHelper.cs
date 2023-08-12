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
                default:
                    options[Command.CountBytes] = true;
                    break;
            }
        }
    }
}