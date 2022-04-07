namespace RobotsControl;

public class Position
{
    public Position(int x, int y, Direction direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }
    public Direction Direction { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}