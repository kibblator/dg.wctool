using System.Text;
using System.Text.RegularExpressions;
using dg.wctool.Models;

namespace dg.wctool.Services;

public static class CounterService
{
    public static string GetCounterOutput(string text, Dictionary<Command, bool> options)
    {
        var output = "";
        var enabledOptions = options.Where(o => o.Value).Select(o => o.Key).ToList();
        if (!enabledOptions.Any())
            enabledOptions = options.Where(o => o.Key != Command.CountCharacters).Select(o => o.Key).ToList();
        
        foreach (var option in enabledOptions)
        {
            switch (option)
            {
                case Command.CountLines:
                    output += $" {CountLines(text)}";
                    break;
                case Command.CountWords:
                    output += $" {CountWords(text)}";
                    break;
                case Command.CountBytes:
                    output += $" {CountBytes(text)}";
                    break;
                case Command.CountCharacters:
                    output += $" {CountCharacters(text)}";
                    break;
            }
        }

        return output.Trim();
    }

    private static int CountLines(string text)
    {
        const string lineEndingsPattern = "\r\n|\n|\r";
        var matches = Regex.Matches(text, lineEndingsPattern);
        return matches.Count;
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
}