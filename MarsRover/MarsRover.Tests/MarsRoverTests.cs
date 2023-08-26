using MarsRover.Exceptions;

namespace MarsRover.Tests;

public class RoverTests
{
    class MockUniverse : ILocationTracker, IWheelDriver
    {
        private readonly Surface surface;

        public Position Location { get; private set; }

        public Direction Heading { get; private set; }

        public MockUniverse(Surface surface, Position location, Direction heading)
        {
            this.surface = surface;
            Location = location;
            Heading = heading;
        }

        public void Drive(int distance)
        {
            Location = surface.Move(Location, distance, Heading);
        }

        public void Turn(RotationDirection rotationDirection)
        {
            Heading = Heading.Add(rotationDirection == RotationDirection.Clockwise ? 90 : -90);
        }

        public Position GetLocation() => Location;

        public Direction GetHeading() => Heading;
    }

    static readonly Surface _surface = new(5, 5);
    static readonly ObstacleDetector _detector = new ObstacleDetector(new Position[]{
        new Position(1,1)
    });

    [Fact]
    public void CanConstruct()
    {
        var position = new Position(2, 3);
        var direction = Direction.North;
        var MockUniverse = new MockUniverse(_surface, position, direction);

        var rover = new Rover(_surface, _detector, MockUniverse, MockUniverse);

        Assert.Equal(position, rover.Location);
        Assert.Equal(direction, rover.Heading);
    }

    [Theory]
    [InlineData("f", 2, 4, 0)]
    [InlineData("b", 2, 2, 0)]
    [InlineData("fb", 2, 3, 0)]
    [InlineData("l", 2, 3, 270)]
    [InlineData("r", 2, 3, 90)]
    [InlineData("frfrf", 3, 3, 180)]
    [InlineData("frfrfrf", 2, 3, 270)]
    [InlineData("frfrfrfr", 2, 3, 0)]
    [InlineData("lflfff", 1, 2, 180)]
    public void CanMoveSafely(string instruction, int endX, int endY, int endHeadingValue)
    {
        var endLocation = new Position(endX, endY);
        var endHeading = new Direction(endHeadingValue);
        var position = new Position(2, 3);
        var direction = Direction.North;
        var MockUniverse = new MockUniverse(_surface, position, direction);
        var rover = new Rover(_surface, _detector, MockUniverse, MockUniverse);

        rover.Follow(new Instruction(instruction));

        Assert.Equal(endLocation, rover.Location);
        Assert.Equal(endHeading, rover.Heading);
    }

    [Theory]
    [InlineData("a", 'a')]
    [InlineData("d", 'd')]
    [InlineData("x", 'x')]
    [InlineData("fbbfub", 'u')]
    public void UnknownCommandsThrowException(string instruction, char failedCommand)
    {
        var position = new Position(2, 3);
        var direction = Direction.North;
        var MockUniverse = new MockUniverse(_surface, position, direction);
        var rover = new Rover(_surface, _detector, MockUniverse, MockUniverse);

        var ex = Assert.Throws<UnknownCommandException>(() => 
            rover.Follow(new Instruction(instruction))
        );

        Assert.Equal(failedCommand, ex.Command);
    }
}
