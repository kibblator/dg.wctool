namespace dg.wctool.Services;

public static class FileService
{
    public static string GetTextFromFile(string path)
    {
        try
        {
            var text = File.ReadAllText(path);
            return text;
        }
        catch (Exception)
        {
            Console.WriteLine($"An error occurred try to read file {path}");
            throw;
        }
    }
}