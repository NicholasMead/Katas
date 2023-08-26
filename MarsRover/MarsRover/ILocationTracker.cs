namespace MarsRover;

public interface ILocationTracker
{
    Position GetLocation();
    Direction GetHeading();
}
