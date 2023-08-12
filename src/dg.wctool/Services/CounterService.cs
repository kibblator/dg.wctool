using System.Text;
using dg.wctool.Models;

namespace dg.wctool.Services;

public static class CounterService
{
    public static string GetCounterOutput(string text, Dictionary<Command, bool> options)
    {
        var output = "";
        var enabledOptions = options.Where(o => o.Value).Select(o => o.Key);
        foreach (var option in enabledOptions)
        {
            switch (option)
            {
                case Command.CountBytes:
                default:
                    output += $"{CountBytes(text)}";
                    break;
            }
        }

        return output;
    }
    
    private static int CountBytes(string text)
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return bytes.Length;
    }
}