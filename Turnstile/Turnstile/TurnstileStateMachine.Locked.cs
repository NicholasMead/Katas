namespace Turnstile;

public partial class TurnstileStateMachine
{
    private class Locked : StateBase {

        public Locked(TurnstileStateMachine machine, string message = "") : base(machine, message)
        { }

        public override TurnstileState State => TurnstileState.Locked;

        public override StateBase Coin()
        {
            return new Unlocked(machine, "Unlock");
        }

        public override StateBase Pass()
        {
            return new Locked(machine, "Alarm");
        }
    }
}
