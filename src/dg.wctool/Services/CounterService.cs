using System.Text;
using System.Text.RegularExpressions;
using dg.wctool.Models;

namespace dg.wctool.Services;

public static class CounterService
{
    public static Dictionary<Command, int> GetValuesForOptions(Dictionary<Command, bool> options, string text)
    {
        var commandValues = new Dictionary<Command, int>();
        var enabledOptions = options.Where(o => o.Value).Select(o => o.Key).ToList();
        if (!enabledOptions.Any())
            enabledOptions = GetDefaultOptions(options);

        foreach (var option in enabledOptions)
        {
            switch (option)
            {
                case Command.CountLines:
                    commandValues.Add(Command.CountLines, CountLines(text));
                    break;
                case Command.CountWords:
                    commandValues.Add(Command.CountWords, CountWords(text));
                    break;
                case Command.CountBytes:
                    commandValues.Add(Command.CountBytes, CountBytes(text));
                    break;
                case Command.CountCharacters:
                    commandValues.Add(Command.CountCharacters, CountCharacters(text));
                    break;
            }
        }

        return commandValues;
    }

    private static int CountLines(string text)
    {
        const string lineEndingsPattern = "\r\n|\n|\r";
        var matches = Regex.Matches(text, lineEndingsPattern);
        var lineCount = !text.EndsWith("\n") ? matches.Count + 1 : matches.Count;
        return lineCount;
    }

    private static int CountBytes(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return bytes.Length;
    }

    private static int CountWords(string text)
    {
        const string wordPattern = @"\S+";
        var matches = Regex.Matches(text, wordPattern);
        return matches.Count;
    }

    private static int CountCharacters(string text)
    {
        return text.Length;
    }


    private static List<Command> GetDefaultOptions(Dictionary<Command, bool> options)
    {
        return options.Where(o => o.Key != Command.CountCharacters).Select(o => o.Key).ToList();
    }
}