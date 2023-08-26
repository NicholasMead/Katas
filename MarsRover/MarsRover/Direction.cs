namespace MarsRover;

public readonly struct Direction
{
    public int Heading { get; }

    public Direction(int heading)
    {
        Heading = heading;
    }

    public Direction Add(int deg)
    {
        deg %= 360;
        return new((360 + Heading + deg) % 360);
    }

    public override string ToString() => $"({Heading})";

    public static readonly Direction North = new(0);
    public static readonly Direction East = new(90);
    public static readonly Direction South = new(180);
    public static readonly Direction West = new(270);
}
