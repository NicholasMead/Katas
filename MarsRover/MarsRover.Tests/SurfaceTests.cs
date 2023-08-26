namespace MarsRover.Tests;

public class SurfaceTests
{
    [Theory]
    [InlineData(0, 0, 1, 0, 0, 1)]
    [InlineData(0, 0, 1, 90, 1, 0)]
    [InlineData(0, 0, 1, 180, 0, 4)]
    [InlineData(0, 0, 1, 270, 4, 0)]
    public void Move(int fromX, int fromY, int dist, int dir, int toX, int toY)
    {
        var from = new Position(fromX, fromY);
        var to = new Position(toX, toY);
        var direction = new Direction(dir);
        var surface = new Surface(5, 5);

        var result = surface.Move(from, dist, direction);

        Assert.Equal(to, result);
    }
}