namespace MarsRover.Tests;

public class ObstacleDetectorTests
{
    [Theory]
    [InlineData(1,1,false)]
    [InlineData(2,2,false)]
    [InlineData(3,3,false)]
    [InlineData(1,0,true)]
    [InlineData(2,1,true)]
    public void CanCheckSave(int x, int y, bool result)
    {
        var position = new Position(x, y);
        var detector = new ObstacleDetector(new Position[] {
            new Position(1,1),
            new Position(2,2),
            new Position(3,3),
        });

        var safe = detector.IsSafe(position);

        Assert.Equal(result, safe);
    }
}