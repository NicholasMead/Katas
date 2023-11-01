namespace Turnstile;

public partial class TurnstileStateMachine
{
    private StateBase state;    

    public TurnstileStateMachine(TurnstileState initialState = TurnstileState.Locked)
    {
        state = initialState switch 
        {           
            TurnstileState.Locked => new Locked(this, ""),
            TurnstileState.Unlocked => new Unlocked(this, ""),
            _ => new Locked(this, "")
        };
    }

    public TurnstileState State => state.State;

    public string Coin() {
        state = state.Coin();
        return state.Message;
    }

    public string Pass() 
    {
        state = state.Pass();
        return state.Message;
    }
}
