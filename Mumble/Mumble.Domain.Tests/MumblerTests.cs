namespace Mumble.Domain.Tests;

public class MumblerTests
{

    [Theory]
    [InlineData("", "")]
    [InlineData("a", "A")]
    [InlineData("aBC", "A-Bb-Ccc")]
    [InlineData("aBCd", "A-Bb-Ccc-Dddd")]
    [InlineData("QWERTY","Q-Ww-Eee-Rrrr-Ttttt-Yyyyyy")]
    [InlineData("RqaEzty", "R-Qq-Aaa-Eeee-Zzzzz-Tttttt-Yyyyyyy")]
    [InlineData("cwAt", "C-Ww-Aaa-Tttt")]
    public void DoesMumble(string input, string expected)
    {
        var mumbler = new Mumbler();

        var response = mumbler.Mumble(input);

        Assert.Equal(expected, response);
    }

    [Theory]
    [InlineData("-")]
    [InlineData("a-")]
    [InlineData("a.")]
    [InlineData("A]")]
    [InlineData("nasd\\asd")]
    public void DoesError(string input)
    {
        var mumbler = new Mumbler();

        var ex = Assert.Throws<ArgumentException>(() => mumbler.Mumble(input));

        Assert.Equal("input", ex.ParamName);
    }
}