namespace LCDController;

public class SegmentDisplay
{
    private readonly IDisplayDriver displayDriver;

    public SegmentDisplay(IDisplayDriver displayDriver)
    {
        this.displayDriver = displayDriver ?? throw new ArgumentNullException(nameof(displayDriver));
    }

    public void Display(string input)
    {
        var segments = new List<Segment>();

        foreach (var item in input)
        {
            var found = Encoding.TryGetValue(item, out var segment);

            if(!found) throw new DisplayException(item);

            if (item == '.' && segments.Count > 0 && (segments.Last() & Segment.Decimal) == 0) {
                segments[^1] = segments.Last() | segment;
            } else {
                segments.Add(segment);
            }
        }

        displayDriver.Draw(segments.ToArray());
    }

    public void Clear() => displayDriver.Clear();

    public bool CanDisplay(char c) => Encoding.ContainsKey(char.ToUpper(c));

    //   _  _     _  _  _  _  _  _
    // | _| _||_||_ |_   ||_||_||_  _
    // ||_  _|  | _||_|  ||_| _||_ |
    private static readonly Dictionary<char, Segment> Encoding = new()
    {
        {' ', Segment.None},
        {'.', Segment.Decimal},
        {'1', Segment.TopRightPipe | Segment.BottomRightPipe},
        {'2', Segment.TopBar | Segment.TopRightPipe | Segment.MiddleBar | Segment.BottomLeftPipe | Segment.BottomBar},
        {'3', Segment.TopBar | Segment.MiddleBar | Segment.BottomBar | Segment.TopRightPipe | Segment.BottomRightPipe},
        {'4', Segment.TopLeftPipe | Segment.TopRightPipe | Segment.MiddleBar | Segment.BottomRightPipe},
        {'5', Segment.TopBar | Segment.TopLeftPipe | Segment.MiddleBar | Segment.BottomRightPipe | Segment.BottomBar},
        {'6', Segment.TopBar | Segment.MiddleBar | Segment.BottomBar | Segment.TopLeftPipe | Segment.BottomLeftPipe | Segment.BottomRightPipe},
        {'7', Segment.TopBar | Segment.TopRightPipe | Segment.BottomRightPipe},
        {'8', Segment.TopBar | Segment.MiddleBar | Segment.BottomBar | Segment.LeftPipe | Segment.RightPipe},
        {'9', Segment.TopBar | Segment.MiddleBar | Segment.BottomBar | Segment.TopLeftPipe | Segment.RightPipe},
        {'0', Segment.TopBar | Segment.BottomBar | Segment.TopLeftPipe | Segment.BottomLeftPipe | Segment.TopRightPipe | Segment.BottomRightPipe},
        {'E', Segment.TopBar | Segment.MiddleBar | Segment.BottomBar | Segment.LeftPipe},
        {'R', Segment.BottomLeftPipe | Segment.MiddleBar},
    };
}
