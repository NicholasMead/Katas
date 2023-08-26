namespace LCDController;

public interface IDisplayDriver
{
    void Draw(params Segment[] segments);

    void Clear();
}
