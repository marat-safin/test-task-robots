namespace RobotsControl;
public interface ICommand
{
    Position Execute(Position current_position);

    bool FallOffPossible { get; }
}