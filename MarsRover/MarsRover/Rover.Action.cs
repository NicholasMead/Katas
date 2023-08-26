namespace MarsRover;

public partial class Rover
{
    public abstract class Command
    {
        public abstract bool CheckSafe(Rover rover);

        public abstract void Execute(Rover rover);
    }

    public sealed class MoveCommand : Command
    {
        public MoveCommand(int distance)
        {
            Distance = distance;
        }

        public int Distance { get; }

        public override bool CheckSafe(Rover rover)
        {
            var position = rover.surface.Move(rover.Location, Distance, rover.Heading);
            return rover.obstacleDetector.IsSafe(position);
        }

        public override void Execute(Rover rover) => rover.wheelDriver.Drive(Distance);
    }

    public sealed class TurnCommand : Command
    {
        public TurnCommand(RotationDirection direction)
        {
            Direction = direction;
        }

        public RotationDirection Direction { get; }

        public override bool CheckSafe(Rover rover) => true;

        public override void Execute(Rover rover) => rover.wheelDriver.Turn(Direction);
    }
}
