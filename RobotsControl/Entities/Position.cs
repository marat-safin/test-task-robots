using System;

namespace RobotsControl;

public class Position
{
    public Position(int x, int y, Direction direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }
    public Direction Direction { get; }
    public int X { get; }
    public int Y { get; }

    public override bool Equals(object? obj)
    {
        if (obj is Position)
        {
            return Equals((Position)obj);
        }
        return false;
    }
    public bool Equals(Position other)
    {
        return other != null && 
                X == other.X && 
                Y == other.Y && 
                Direction == other.Direction;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Direction);
    }

    public override string ToString()
    {
        return $"{X} {Y} {Direction}";
    }
}