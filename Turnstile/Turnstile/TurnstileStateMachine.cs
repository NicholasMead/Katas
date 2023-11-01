namespace Turnstile;

public class TurnstileStateMachine
{
    public TurnstileStateMachine(TurnstileState initialState = TurnstileState.Locked)
    {
        State = initialState;
    }

    public TurnstileState State {get; private set;}

    public string Coin() {
        switch(State){
            case TurnstileState.Locked:
                State = TurnstileState.Unlocked;
                return "Unlock";
            case TurnstileState.Unlocked:
                return "Thanks";
            
            default:
                throw new InvalidOperationException();
        }
    }

    public string Pass() 
    {
        switch(State){
            case TurnstileState.Locked:
                return "Alarm";
            case TurnstileState.Unlocked:
                State = TurnstileState.Locked;
                return "Lock";
            
            default:
                throw new InvalidOperationException();
        }
    }
}
