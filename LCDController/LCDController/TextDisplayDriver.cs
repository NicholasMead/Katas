using System.ComponentModel.Design;

namespace LCDController;

public class TextDisplayDriver : IDisplayDriver
{
    public TextDisplayOptions Options { get; } = new TextDisplayOptions();

    public string Text { get; private set; } = "";

    public TextDisplayDriver()
    { }

    public TextDisplayDriver(TextDisplayOptions options)
    {
        Options = options;
    }

    public void Draw(params Segment[] segments)
    {
        // ** Display Sections **
        // (Width = 2, Height = 2)
        //  __   <- Section 1 : Top
        // |  |  <- Section 2 : Upper Verticals (height > 1)
        // |__|  <- Section 3 : Middle
        // |  |  <- Section 4 : Lower Verticals (height > 1)
        // |__|. <- Section 5 : Bottom

        var padding = MultipleChar(Options.Padding, ' '); // Between Segments
        var space = MultipleChar(Options.Width, ' ');     // Between Pipes
        var bar = MultipleChar(Options.Width, '_');

        var top = string.Join(padding, segments
            .Select(segment => (byte)(segment & Segment.TopBar) > 0 ? $" {bar}  " : $" {space}  "));

        var upperVerticals = MultipleLines(Options.Height - 1,
            string.Join(padding, segments
                .Select(segment =>
                    ((byte)(segment & Segment.TopLeftPipe) > 0 ? "|" : " ") +
                    space +
                    ((byte)(segment & Segment.TopRightPipe) > 0 ? "| " : "  ")
                )));

        var middle = string.Join(padding, segments
            .Select(segment =>
                ((byte)(segment & Segment.TopLeftPipe) > 0 ? "|" : " ") +
                ((byte)(segment & Segment.MiddleBar) > 0 ? bar : space) +
                ((byte)(segment & Segment.TopRightPipe) > 0 ? "| " : "  ")
            ));

        var lowerVerticals = MultipleLines(Options.Height - 1,
            string.Join(padding, segments
                .Select(segment =>
                    ((byte)(segment & Segment.BottomLeftPipe) > 0 ? "|" : " ") +
                    space +
                    ((byte)(segment & Segment.BottomRightPipe) > 0 ? "| " : "  ")
                )));

        var bottom = string.Join(padding, segments
            .Select(segment =>
                ((byte)(segment & Segment.BottomLeftPipe) > 0 ? "|" : " ") +
                ((byte)(segment & Segment.BottomBar) > 0 ? bar : space) +
                ((byte)(segment & Segment.BottomRightPipe) > 0 ? "|" : " ") +
                ((byte)(segment & Segment.Decimal) > 0 ? "." : " ")
            ));

        var lines = new string?[] {
            top,
            Options.Height > 1 ? upperVerticals : null,
            middle,
            Options.Height > 1 ? lowerVerticals : null,
            bottom
        }.Where(l => l != null);
        Text = string.Join('\n', lines);
    }

    public void Clear()
    {
        Text = MultipleLines(Options.Height * 2 + 1, "");
    }

    private static string MultipleChar(int count, char c)
    {
        var strings = Enumerable
            .Range(0, count)
            .Aggregate("", (s, _) => s + c);

        return strings;
    }

    private static string MultipleLines(int count, string line)
    {
        var lines = Enumerable
            .Range(0, count)
            .Select(_ => line);

        return string.Join('\n', lines);
    }
}
