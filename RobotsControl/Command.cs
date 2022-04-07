namespace RobotsControl;
public abstract class Command
{
    public abstract Position Execute(Position current_position);
}