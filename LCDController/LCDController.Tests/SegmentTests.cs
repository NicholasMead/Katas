namespace LCDController.Tests;

public class SegmentTests
{
    [Theory]
    [InlineData(Segment.TopBar,          0b00000001)]
    [InlineData(Segment.MiddleBar,       0b00000010)]
    [InlineData(Segment.BottomBar,       0b00000100)]
    [InlineData(Segment.TopLeftPipe,     0b00001000)]
    [InlineData(Segment.BottomLeftPipe,  0b00010000)]
    [InlineData(Segment.TopRightPipe,    0b00100000)]
    [InlineData(Segment.BottomRightPipe, 0b01000000)]
    [InlineData(Segment.Decimal,         0b10000000)]
    public void SegmentsZonesAreFlags(Segment zone, byte value)
    {
        Assert.Equal(value, (byte)zone);
    }
}