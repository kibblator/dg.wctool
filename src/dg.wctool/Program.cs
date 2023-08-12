using dg.wctool.Helpers;
using dg.wctool.Models;
using dg.wctool.Services;

var options = new Dictionary<Command, bool>
{
    
    { Command.CountLines , false},
    { Command.CountWords, false },
    { Command.CountBytes, false },
    { Command.CountCharacters, false}
};

var textSources = new List<string>();
var redirectedInput = "";

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

if (Console.IsInputRedirected)
{
    redirectedInput = Console.In.ReadToEnd();
}

if (textSources.Count == 0 && string.IsNullOrEmpty(redirectedInput))
{
    ConsoleHelper.ShowScrollableHelp();
}

if (!string.IsNullOrEmpty(redirectedInput))
{
    Console.WriteLine($"{CounterService.GetCounterOutput(redirectedInput, options)}");
}
else
{
    foreach (var textSource in textSources)
    {
        try
        {
            var text = FileService.GetTextFromFile(textSource);
            Console.WriteLine($"{CounterService.GetCounterOutput(text, options)} {textSource}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"ccwc: {textSource}: No such file or directory");
        }
    }
}