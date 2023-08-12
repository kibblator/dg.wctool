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
    var optionValues = CounterService.GetValuesForOptions(options, redirectedInput);
    ConsoleHelper.DisplayCounterOutput(optionValues.Select(ov => ov.Value).ToList());
}
else
{
    var commandTotals = new Dictionary<Command, int>();
    foreach (var textSource in textSources)
    {
        try
        {
            var text = FileService.GetTextFromFile(textSource);
            var optionValues = CounterService.GetValuesForOptions(options, text);
            foreach (var optionValue in optionValues)
            {
                commandTotals.TryAdd(optionValue.Key, 0);
                commandTotals[optionValue.Key] += optionValue.Value;
            }
            ConsoleHelper.DisplayCounterOutput(optionValues.Select(ov => ov.Value).ToList(), textSource);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"ccwc: {textSource}: No such file or directory");
        }
    }

    if (textSources.Count > 1)
    {
        ConsoleHelper.DisplayCounterOutput(commandTotals.Select(ov => ov.Value).ToList(), "total");
    }
}