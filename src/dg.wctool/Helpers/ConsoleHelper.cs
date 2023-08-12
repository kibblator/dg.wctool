namespace dg.wctool.Helpers;

public class ConsoleHelper
{
    public static void ShowScrollableHelp()
    {
        var helpContent = GenerateHelpContent();

        var consoleHeight = Console.WindowHeight;
        var topIndex = 0;

        var done = false;
        while (!done)
        {
            Console.Clear();

            for (var i = topIndex; i < Math.Min(topIndex + consoleHeight - 1, helpContent.Count); i++)
            {
                Console.WriteLine(helpContent[i]);
            }

            Console.WriteLine("Press UP/DOWN to scroll, or Q to quit.");

            var keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    topIndex = Math.Max(0, topIndex - 1);
                    break;
                case ConsoleKey.DownArrow:
                    topIndex = Math.Min(helpContent.Count - 1, topIndex + 1);
                    break;
                case ConsoleKey.Q:
                    done = true;
                    break;
            }
        }

        Console.Clear();
    }

    private static List<string> GenerateHelpContent()
    {
        var content = @"CCWC HELP

NAME
     ccwc -- word, line, character, and byte count

SYNOPSIS
     ccwc [-clmw] [file ...]

DESCRIPTION
     The ccwc utility displays the number of lines, words, and bytes contained
     in each input file, or standard input (if no file is specified) to the
     standard output.  A line is defined as a string of characters delimited
     by a <newline> character.  Characters beyond the final <newline> charac-
     ter will not be included in the line count.

     A word is defined as a string of characters delimited by white space
     characters. If more than one input file is
     specified, a line of cumulative counts for all the files is displayed on
     a separate line after the file output.  The counts are separated by a single space.

     The options are as follows:

     -c      The number of bytes in each input file is written to the standard
             output.  This will cancel out any prior usage of the -m option.

     -l      The number of lines in each input file is written to the standard
             output.

     -m      The number of characters in each input file is written to the
             standard output.  If the current locale does not support multi-
             byte characters, this is equivalent to the -c option.  This will
             cancel out any prior usage of the -c option.

     -w      The number of words in each input file is written to the standard
             output.

     When an option is specified, wc only reports the information requested by
     that option.  The order of output always takes the form of line, word,
     byte, and file name.  The default action is equivalent to specifying the
     -c, -l and -w options.

EXAMPLES
     Count the number of characters, words, and lines in a file:

           $ ccwc filename

     Count the number of characters, words, and lines in several files, and
     print a total:

           $ ccwc file1 file2 file3

     Count the number of bytes, characters, words, and lines in a file:

           $ ccwc -c -m -w -l filename
";
        return new List<string>(content.Split('\n'));
    }
}