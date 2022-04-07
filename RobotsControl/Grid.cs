using System;

namespace RobotsControl;

public class Grid
{
    public Grid(int max_x, int max_y)
    {
        if (max_x < 0 || max_y < 0)
        {
            throw new ArgumentOutOfRangeException("Incorrect grid dimensions");
        }
        MaxX = max_x;
        MaxY = max_y;
    }

    public int MaxX { get; }
    public int MaxY { get; }

    internal bool ValidatePosition(Position position)
    {
        if (position.X < 0 || position.X > MaxX || position.Y < 0 || position.Y > MaxY)
        {
            return false;
        }
        switch (position.Direction)
        {
            case Direction.North:
            case Direction.South:
            case Direction.East:
            case Direction.West:
                break;
            default:
                return false;
        }
        return true;
    }
}