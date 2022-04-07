namespace RobotsControl;
public class Robot
{
    public Robot(Position position)
    {
        Position = position;
        Lost = false;
    }
    public Position Position { get; internal set; }
    public bool Lost { get; internal set; }
}