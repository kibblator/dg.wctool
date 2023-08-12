using dg.wctool.Helpers;
using dg.wctool.Models;
using dg.wctool.Services;

var options = new Dictionary<Command, bool>
{
    {
        Command.CountBytes, false
    }
};

var textSources = new List<string>();

foreach (var arg in args)
{
    if (CommandHelper.IsCommand(arg))
    {
        CommandHelper.SetCommands(arg, options);
    }
    else
    {
        textSources.Add(arg);
    }
}

foreach (var textSource in textSources)
{
    try
    {
        var text = FileService.GetTextFromFile(textSource);
        Console.WriteLine($"{CounterService.GetCounterOutput(text, options)}\t{textSource}");
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine($"ccwc: {textSource}: No such file or directory");
    }
}