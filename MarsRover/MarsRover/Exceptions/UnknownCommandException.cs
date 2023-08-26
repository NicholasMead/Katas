namespace MarsRover.Exceptions;

public class UnknownCommandException : RoverException
{
    public char Command { get; }

    public UnknownCommandException(char command) : base($"Unkown Command: {command}")
    {
        Command = command;
    }
}