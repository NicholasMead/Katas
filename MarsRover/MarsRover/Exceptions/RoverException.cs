namespace MarsRover.Exceptions
{
    public class RoverException : Exception
    {
        public RoverException() { }
        public RoverException(string message) : base(message) { }
        public RoverException(string message, Exception inner) : base(message, inner) { }
    }
}
