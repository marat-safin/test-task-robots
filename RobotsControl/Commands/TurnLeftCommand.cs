using System;

namespace RobotsControl;

public class TurnLeftCommand : Command
{
    public override Position Execute(Position current_position)
    {
        Direction new_direction;
        switch (current_position.Direction)
        {
            case Direction.North:
                new_direction = Direction.West;
                break;
            case Direction.West:
                new_direction = Direction.South;
                break;
            case Direction.South:
                new_direction = Direction.East;
                break;
            case Direction.East:
                new_direction = Direction.North;
                break;
            default:
                throw new ArgumentException("Unsupported Direction: " + current_position.Direction);
        }
        return new Position(current_position.X, current_position.Y, new_direction);
    }
}