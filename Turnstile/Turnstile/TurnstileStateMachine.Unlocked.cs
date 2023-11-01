namespace Turnstile;

public partial class TurnstileStateMachine
{
    private class Unlocked : StateBase {

        public Unlocked(TurnstileStateMachine machine, string message = "") : base(machine, message)
        { }

        public override TurnstileState State => TurnstileState.Unlocked;

        public override StateBase Coin()
        {
            return new Unlocked(machine, "Thanks");
        }

        public override StateBase Disable()
        {
            return new NoEntry(machine, "Lights Off");
        }

        public override StateBase Enable()
        {
            return new Unlocked(machine, "");
        }

        public override StateBase Pass()
        {
            return new Locked(machine, "Lock");
        }
    }
}
