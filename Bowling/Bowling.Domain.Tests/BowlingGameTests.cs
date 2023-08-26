namespace Bowling.Domain.Tests;

public class BowlingGameTests
{
    [Fact]
    public void CanConstruct()
    {
        var player = "Nick";

        var game = new BowlingGame(player);

        Assert.Equal(player, game.Player);
        Assert.Equal("", game.History);
    }

    [Fact]
    public void CanBowlSingle() {
        var game = new BowlingGame("");

        game.Bowl(5);

        Assert.Equal("5", game.History);
    }

    [Fact]
    public void CanBowlStrike() {
        var game = new BowlingGame("");

        game.Bowl(10);

        Assert.Equal("X", game.History);
    }

    [Fact]
    public void CanBowlSpare() {
        var game = new BowlingGame("");

        game.Bowl(6);
        game.Bowl(4);

        Assert.Equal("6/", game.History);
    }

    [Theory]
    [InlineData(new int[]{10,5,4,6,0}, "X 54 60", 34)]
    [InlineData(new int[]{10,5,5,6,1}, "X 5/ 61", 43)]
    [InlineData(new int[]{10}, "X", 10)]
    [InlineData(new int[]{10,4,5}, "X 45", 28)]
    [InlineData(new int[]{10,4,5,4,6,3,2}, "X 45 4/ 32", 46)]
    public void CanBowlMultiple(int[] bowls, string history, int score)
    {
        var game = new BowlingGame("");

        foreach (var pins in bowls)
        {
            game.Bowl(pins);
        }

        Assert.Equal(history, game.History);
        Assert.Equal(score, game.Score);
    }
}