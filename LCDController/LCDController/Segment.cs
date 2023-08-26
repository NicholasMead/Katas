namespace LCDController;

public enum Segment : byte
{
    None = 0,
    TopBar = 1 << 0,
    MiddleBar = 1 << 1,
    BottomBar = 1 << 2,
    TopLeftPipe = 1 << 3,
    BottomLeftPipe = 1 << 4,
    TopRightPipe = 1 << 5,
    BottomRightPipe = 1 << 6,
    Decimal = 1 << 7,

    LeftPipe = TopLeftPipe | BottomLeftPipe,
    RightPipe = TopRightPipe | BottomRightPipe,
}
