using System.Text;

namespace dg.wctool.Services;

public static class FileService
{
    public static string GetTextFromFile(string path)
    {
        var text = File.ReadAllText(path, Encoding.UTF8);
        return text;
    }
}