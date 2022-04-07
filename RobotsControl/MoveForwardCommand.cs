using System;

namespace RobotsControl;

public class MoveForwardCommand : Command
{
    public override Position Execute(Position current_position)
    {
        int delta_x = 0;
        int delta_y = 0;
        switch (current_position.Direction)
        {
            case Direction.North:
                delta_y = 1;
                break;
            case Direction.South:
                delta_y = -1;
                break;
            case Direction.East:
                delta_x = 1;
                break;
            case Direction.West:
                delta_x = -1;
                break;
            default:
                throw new ArgumentException("Unsupported Direction: " + current_position.Direction);
        }
        return new Position(current_position.X + delta_x, current_position.Y + delta_y, current_position.Direction);
    }
}