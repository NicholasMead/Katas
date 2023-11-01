namespace Turnstile.Tests;

public class TurnstileStateMachineTests
{
    [Fact]
    public void OnConstruct_StateIsLocked()
    {
        var machine = new TurnstileStateMachine();

        Assert.Equal(TurnstileState.Locked, machine.State);
    }

    [Fact]
    public void OnCoin_FromLocked_Unlock()
    {
        var machine = new TurnstileStateMachine();

        var result = machine.Coin();

        Assert.Equal("Unlock", result);
        Assert.Equal(TurnstileState.Unlocked, machine.State);
    }

    [Fact]
    public void OnCoin_FromUnlock_Thanks()
    {
        var machine = new TurnstileStateMachine(TurnstileState.Unlocked);

        var result = machine.Coin();

        Assert.Equal("Thanks", result);
        Assert.Equal(TurnstileState.Unlocked, machine.State);
    }

    [Fact]
    public void OnPass_FromUnlocked_Locks()
    {
        var machine = new TurnstileStateMachine(TurnstileState.Unlocked);

        var result = machine.Pass();
        
        Assert.Equal("Lock", result);
        Assert.Equal(TurnstileState.Locked, machine.State);
    }

        [Fact]
    public void OnPass_FromUnlock_Alarm()
    {
        var machine = new TurnstileStateMachine(TurnstileState.Locked);

        var result = machine.Pass();
        
        Assert.Equal("Alarm", result);
        Assert.Equal(TurnstileState.Locked, machine.State);
    }
}