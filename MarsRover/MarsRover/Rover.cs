using MarsRover.Exceptions;

namespace MarsRover;

public partial class Rover
{
    private readonly Surface surface;
    private readonly ObstacleDetector obstacleDetector;
    private readonly ILocationTracker locationTracker;
    private readonly IWheelDriver wheelDriver;

    public Position Location => locationTracker.GetLocation();

    public Direction Heading => locationTracker.GetHeading();

    public Rover(Surface surface, ObstacleDetector obstacleDetector, ILocationTracker locationTracker, IWheelDriver wheelDriver)
    {
        this.surface = surface;
        this.obstacleDetector = obstacleDetector;
        this.locationTracker = locationTracker;
        this.wheelDriver = wheelDriver;
    }

    public void Follow(Instruction instruction)
    {
        foreach (Command command in Translated(instruction))
        {
            var safe = command.CheckSafe(this);

            if (!safe)    
                return;
            
            command.Execute(this);
        }
    }

    private static IEnumerable<Command> Translated(Instruction instruction)
    {
        foreach (char command in instruction.instructions) {
            yield return command switch
            {
                'f' => new MoveCommand(1),
                'b' => new MoveCommand(-1),
                'l' => new TurnCommand(RotationDirection.AntiClockwise),
                'r' => new TurnCommand(RotationDirection.Clockwise),
                _ => throw new UnknownCommandException(command),
            };
        }
    }
}
