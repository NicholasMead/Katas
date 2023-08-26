namespace LCDController;

public record class TextDisplayOptions
{
    private int height = 1;
    private int width = 1;
    private int padding = 0;

    public int Height
    {
        get => height;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));
            height = value;
        }
    }


    public int Width
    {
        get => width;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value));
            width = value;
        }
    }

    public int Padding { 
        get => padding;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            padding = value;
        }
     }
}
