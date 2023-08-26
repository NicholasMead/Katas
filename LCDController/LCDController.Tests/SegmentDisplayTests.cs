namespace LCDController.Tests;

class MockDisplayDriver : IDisplayDriver
{
    public Segment[]? Latest {get; private set;} 

    public void Draw(Segment[] segments)
    {
        Latest = segments;
    }

    public void Clear()
    {
        Latest = null;
    }
}

public class SegmentDisplayTests
{
    [Fact]
    public void DoesWriteToDisplayDriver()
    {
        var mockDriver = new MockDisplayDriver();
        var display = new SegmentDisplay(mockDriver);

        display.Display("123");

        Assert.Equal(3, mockDriver.Latest!.Length);
    }

    [Fact]
    public void DoesEncodeToTextDriver()
    {
        var textDisplay = new TextDisplayDriver();
        var display = new SegmentDisplay(textDisplay);
        var input = "1234567890ER";
        var expected = 
            "    " + " _  " + " _  " + "    " + " _  " + " _  " + " _  " + " _  " + " _  " + " _  "+  " _  " + "    " + '\n' +
            "  | " + " _| " + " _| " + "|_| " + "|_  " + "|_  " + "  | " + "|_| " + "|_| " + "| | "+  "|_  " + " _  " + '\n' +
            "  | " + "|_  " + " _| " + "  | " + " _| " + "|_| " + "  | " + "|_| " + " _| " + "|_| "+  "|_  " + "|   ";

        display.Display(input);
        
        Assert.Equal(expected, textDisplay.Text);
    }

    [Fact]
    public void DoesEncodeDecimals()
    {
        var textDisplay = new TextDisplayDriver();
        var display = new SegmentDisplay(textDisplay);
        var input = "1.";
        var expected = 
            "    " + '\n' +
            "  | " + '\n' +
            "  |.";

        display.Display(input);
        
        Assert.Equal(expected, textDisplay.Text);
    }

    [Fact]
    public void DoesEncodeSeparateDecimals()
    {
        var textDisplay = new TextDisplayDriver();
        var display = new SegmentDisplay(textDisplay);
        var input = "..";
        var expected = 
            "    " + "    " + '\n' +
            "    " + "    " + '\n' +
            "   ." + "   .";

        display.Display(input);
        
        Assert.Equal(expected, textDisplay.Text);
    }

    [Theory]
    [InlineData("a", 'a')]
    [InlineData("ab", 'a')]
    [InlineData("123a", 'a')]
    [InlineData("123]9", ']')]
    [InlineData(",", ',')]
    public void DoesThrowDisplayError(string input, char error)
    {
        var textDisplay = new TextDisplayDriver();
        var display = new SegmentDisplay(textDisplay);

        var ex = Assert.Throws<DisplayException>(() => display.Display(input));
        
        Assert.Equal(error, ex.UnknownChar);
    }

    [Fact]
    public void DoesClear()
    {
        var driver = new MockDisplayDriver();
        var display = new SegmentDisplay(driver);
            
        display.Display("1");
        display.Clear();
        
        Assert.Null(driver.Latest);
    }
}