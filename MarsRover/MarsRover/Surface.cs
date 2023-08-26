namespace MarsRover;

public readonly record struct Surface
{
    public int Width { get; } = 0;
    public int Height { get; } = 0;

    public Surface(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Position Move(Position from, int distance, Direction direction)
    {
        var rad = Convert.ToDouble(direction.Heading) / 180 * Math.PI;
        var dx = Math.Sin(rad) * distance;
        var dy = Math.Cos(rad) * distance;

        var x = from.x + (int)dx;
        var y = from.y + (int)dy;

        return new Position(Scale(x, Width), Scale(y, Height));
    }

    private static int Scale(int input, int scale)
    {
        input += scale;
        input %= scale;
        return input;
    }
}
