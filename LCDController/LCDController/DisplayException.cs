namespace LCDController;

public class DisplayException : Exception
{
    public char UnknownChar { get; }

    public DisplayException(char c) : base($"Unable to display character: {c}")
    {
        UnknownChar = c;
    }
}
