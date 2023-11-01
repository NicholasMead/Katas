namespace Turnstile.Tests;

public class TurnstileStateMachineTests
{
    [Fact]
    public void OnConstruct_StateIsLocked()
    {
        var machine = new TurnstileStateMachine();

        Assert.Equal(TurnstileState.Locked, machine.State);
    }

    [Theory]
    [InlineData(TurnstileState.Locked, TurnstileState.Unlocked, "Unlock")]
    [InlineData(TurnstileState.Unlocked, TurnstileState.Unlocked, "Thanks")]
    [InlineData(TurnstileState.NoEntry, TurnstileState.NoEntry, "")]
    public void Coin(TurnstileState initialState, TurnstileState endState, string message)
    {
        var machine = new TurnstileStateMachine(initialState);

        var result = machine.Coin();

        Assert.Equal(message, result);
        Assert.Equal(endState, machine.State);
    }

    [Theory]
    [InlineData(TurnstileState.Locked, TurnstileState.Locked, "Alarm")]
    [InlineData(TurnstileState.Unlocked, TurnstileState.Locked, "Lock")]
    [InlineData(TurnstileState.NoEntry, TurnstileState.NoEntry, "Alarm")]
    public void Pass(TurnstileState initialState, TurnstileState endState, string message)
    {
        var machine = new TurnstileStateMachine(initialState);

        var result = machine.Pass();

        Assert.Equal(message, result);
        Assert.Equal(endState, machine.State);
    }


    [Theory]
    [InlineData(TurnstileState.Locked, TurnstileState.NoEntry, "Lights Off")]
    [InlineData(TurnstileState.Unlocked, TurnstileState.NoEntry, "Lights Off")]
    [InlineData(TurnstileState.NoEntry, TurnstileState.NoEntry, "")]
    public void Disable(TurnstileState initialState, TurnstileState endState, string message)
    {
        var machine = new TurnstileStateMachine(initialState);

        var result = machine.Disable();

        Assert.Equal(message, result);
        Assert.Equal(endState, machine.State);
    }

    [Theory]
    [InlineData(TurnstileState.Locked, TurnstileState.Locked, "")]
    [InlineData(TurnstileState.Unlocked, TurnstileState.Unlocked, "")]
    [InlineData(TurnstileState.NoEntry, TurnstileState.Locked, "Lights On")]
    public void Enable(TurnstileState initialState, TurnstileState endState, string message)
    {
        var machine = new TurnstileStateMachine(initialState);

        var result = machine.Enable();

        Assert.Equal(message, result);
        Assert.Equal(endState, machine.State);
    }
}