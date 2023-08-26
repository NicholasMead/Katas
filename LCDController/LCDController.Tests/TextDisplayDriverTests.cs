namespace LCDController.Tests;

public class TextDisplayDriverTests
{
    [Theory]
    [InlineData(Segment.TopBar, " _  ", 0)]
    [InlineData(Segment.MiddleBar, " _  ", 1)]
    [InlineData(Segment.BottomBar, " _  ", 2)]
    [InlineData(Segment.TopLeftPipe, "|   ", 1)]
    [InlineData(Segment.BottomLeftPipe, "|   ", 2)]
    [InlineData(Segment.TopRightPipe, "  | ", 1)]
    [InlineData(Segment.BottomRightPipe, "  | ", 2)]
    [InlineData(Segment.Decimal, "   .", 2)]
    [InlineData(Segment.None, "    ", 0)]
    [InlineData(Segment.None, "    ", 1)]
    [InlineData(Segment.None, "    ", 2)]
    [InlineData(Segment.TopLeftPipe | Segment.MiddleBar | Segment.TopRightPipe, "|_| ", 1)]
    [InlineData(Segment.BottomLeftPipe | Segment.BottomBar | Segment.BottomRightPipe, "|_| ", 2)]
    [InlineData(Segment.BottomLeftPipe | Segment.BottomBar | Segment.BottomRightPipe | Segment.Decimal, "|_|.", 2)]
    public void CanDrawPartial(Segment input, string output, int line)
    {
        var driver = new TextDisplayDriver();

        driver.Draw(input);

        var selectedLine = driver.Text.Split('\n')[line];
        Assert.Equal(output, selectedLine);
    }

    [Fact]
    public void CanDrawMultiplePartial()
    {
        var driver = new TextDisplayDriver();

        driver.Draw(Segment.TopBar, Segment.TopBar);

        var selectedLine = driver.Text.Split('\n')[0];
        Assert.Equal(" _   _  ", selectedLine);
    }

    [Theory]
    [InlineData(1, "_")]
    [InlineData(2, "__")]
    [InlineData(3, "___")]
    [InlineData(9, "_________")]
    public void CanDrawWidthBar(int width, string expected)
    {
        var options = new TextDisplayOptions { Width = width };
        var driver = new TextDisplayDriver(options);

        driver.Draw(Segment.TopBar | Segment.MiddleBar | Segment.BottomBar);

        var lines = driver.Text.Split('\n');
        foreach (var line in lines)
        {
            Assert.Equal($" {expected}  ", line);
        }
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(9)]
    public void CanDrawWidthSpace(int width)
    {
        var options = new TextDisplayOptions { Width = width };
        var driver = new TextDisplayDriver(options);

        driver.Draw(Segment.None);

        var lines = driver.Text.Split('\n');
        foreach (var line in lines)
        {
            Assert.Matches(@"^\s{" + $"{width + 3}" + "}$", line);
        }
    }

    [Fact]
    public void CanDrawHeight()
    {
        var options = new TextDisplayOptions { Height = 3 };
        var driver = new TextDisplayDriver(options);
        var input = (Segment)byte.MaxValue;
        var expected = 
            " _  " + '\n' +
            "| | " + '\n' +
            "| | " + '\n' +
            "|_| " + '\n' +
            "| | " + '\n' +
            "| | " + '\n' +
            "|_|.";

        driver.Draw(input);

        Assert.Equal(expected, driver.Text);
    }

    [Fact]
    public void CanDrawHeightWidth()
    {
        var options = new TextDisplayOptions { Height = 2, Width = 3 };
        var driver = new TextDisplayDriver(options);
        var input = (Segment)byte.MaxValue;
        var expected = 
            " ___  " + '\n' +
            "|   | " + '\n' +
            "|___| " + '\n' +
            "|   | " + '\n' +
            "|___|.";

        driver.Draw(input);

        Assert.Equal(expected, driver.Text);
    }

    [Fact]
    public void CanDrawSpacing()
    {
        var options = new TextDisplayOptions { Height = 2, Width = 2, Padding = 3};
        var driver = new TextDisplayDriver(options);
        var input = (Segment)byte.MaxValue;
        var expected = 
            " __  " + "   " + " __  " + '\n' +
            "|  | " + "   " + "|  | " + '\n' +
            "|__| " + "   " + "|__| " + '\n' +
            "|  | " + "   " + "|  | " + '\n' +
            "|__|." + "   " + "|__|.";

        driver.Draw(input, input);

        Assert.Equal(expected, driver.Text);
    }

    [Fact]
    public void CanClear()
    {
        var options = new TextDisplayOptions { Height = 2 };
        var driver = new TextDisplayDriver(options);

        driver.Clear();

        Assert.Equal(5, driver.Text.Split('\n').Length);
        Assert.All(driver.Text.Split('\n'), Assert.Empty);
    }

    [Fact]
    public void CanDrawEmptyString()
    {
        var options = new TextDisplayOptions { Height = 2 };
        var driver = new TextDisplayDriver(options);

        driver.Draw();
        var empty = driver.Text;

        driver.Draw(Segment.None);
        var none = driver.Text.Replace(" ", "");

        Assert.Equal(none, empty);
    }
}