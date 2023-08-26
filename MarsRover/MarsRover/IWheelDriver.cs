namespace MarsRover;

public interface IWheelDriver
{
    void Drive(int distance);
    void Turn(RotationDirection rotationDirection);
}
