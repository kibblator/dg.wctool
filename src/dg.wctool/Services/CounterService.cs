using System.Text;
using dg.wctool.Models;

namespace dg.wctool.Services;

public static class CounterService
{
    public static string GetCounterOutput(string text, Dictionary<Command, bool> options)
    {
        var output = "";
        var enabledOptions = options.Where(o => o.Value).Select(o => o.Key).ToList();
        if (!enabledOptions.Any())
            enabledOptions = options.Select(o => o.Key).ToList();
        
        foreach (var option in enabledOptions)
        {
            switch (option)
            {
                case Command.CountBytes:
                    output += $" {CountBytes(text)}";
                    break;
                case Command.CountLines:
                    output += $" {CountLines(text)}";
                    break;
                default:
                    break;
            }
        }

        return output.Trim();
    }

    private static int CountLines(string text)
    {
        var lines = text.Split(Environment.NewLine).Length;
        return lines;
    }

    private static int CountBytes(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return bytes.Length;
    }
}