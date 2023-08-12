namespace dg.wctool.Services;

public static class FileService
{
    public static string GetTextFromFile(string path)
    {
        var text = File.ReadAllText(path);
        return text;
    }
}