namespace Turnstile;

public partial class TurnstileStateMachine
{
    private class NoEntry : StateBase
    {
        public NoEntry(TurnstileStateMachine machine, string message = "") : base(machine, message)
        { }

        public override TurnstileState State => TurnstileState.NoEntry;

        public override StateBase Coin()
        {
            return new NoEntry(machine, "");
        }

        public override StateBase Pass()
        {
            return new NoEntry(machine, "Alarm");
        }

        public override StateBase Disable()
        {
            return new NoEntry(machine, "");
        }

        public override StateBase Enable()
        {
            return new Locked(machine, "Lights On");
        }
    }
}
