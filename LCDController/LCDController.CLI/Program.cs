using System.Runtime.Serialization.Json;
using LCDController;
using LCDController.CLI;

internal class Program
{
    private static string text = "";

    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        var display = new SegmentDisplay(new ConsoleDriver(
            new TextDisplayOptions
            {
                Width = int.Parse(args.ElementAtOrDefault(0) ?? "1"),
                Height = int.Parse(args.ElementAtOrDefault(1) ?? "1"),
                Padding = int.Parse(args.ElementAtOrDefault(2) ?? "0"),
            }
        ));
        
        while (true)
        {
            try
            {
                display.Clear();
                display.Display(text);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                return;
            }

            var input = Console.ReadKey(true);

            switch(input.Key)
            {
                case ConsoleKey.Escape:
                    return;
                case ConsoleKey.Backspace:
                    text = text.Length > 0 ? text[..^1] : "";
                    continue;
                case ConsoleKey.Enter:
                    text = "";
                    continue;
            };
            
            if(text.Replace(".", "").Length >= 8)
                continue;

            text += char.ToUpper(input.KeyChar) switch
            {
                '.' => !text.Contains('.') ? "." : "",
                'E' => "",
                'R' => "",
                _ => display.CanDisplay(input.KeyChar) ? input.KeyChar : "",
            };
        }
    }
}