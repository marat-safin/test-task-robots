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

    internal bool CheckPositionIsInside(Position position)
    {
        return position.X >= 0 && position.X <= MaxX && position.Y >= 0 && position.Y <= MaxY;
    }
}