namespace Turnstile;

public partial class TurnstileStateMachine
{
    private abstract class StateBase {
        protected readonly TurnstileStateMachine machine;
        
        public StateBase(TurnstileStateMachine machine, string message = "")
        {
            this.machine = machine;
            Message = message;
        }

        public string Message { get; }
        
        public abstract TurnstileState State { get; }

        public abstract StateBase Coin();        
        public abstract StateBase Pass();
    }
}
