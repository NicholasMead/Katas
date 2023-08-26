namespace MarsRover;

public class ObstacleDetector
{
    public Position[] Obstacles { get; private set; }

    public ObstacleDetector(Position[] obstacles)
    {
        Obstacles = obstacles.ToArray();
    }

    public bool IsSafe(Position position) => !Obstacles.Contains(position);
}
