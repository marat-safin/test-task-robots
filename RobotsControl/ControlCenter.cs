using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace RobotsControl;

public class ControlCenter
{
    internal const int MAX_GRID_DIMENSION = 50;
    public ControlCenter(Grid grid)
    {
        if (grid.MaxX > MAX_GRID_DIMENSION || grid.MaxY > MAX_GRID_DIMENSION)
        {
            throw new ArgumentException("Unsupported grid size");
        }
        SurfaceGrid = grid;
        DangerousPositions = new HashSet<Position>();
        ProcessedRobots = new List<Robot>();
    }

    private Grid SurfaceGrid { get; }
    private HashSet<Position> DangerousPositions { get; }
    private List<Robot> ProcessedRobots { get; }

    public ImmutableList<Robot> GetProcessedRobots()
    {
        return ImmutableList.Create<Robot>(ProcessedRobots.ToArray());
    }

    public void ManipulateRobot(Robot robot, List<Command> commands)
    {
        if (!SurfaceGrid.CheckPositionIsInside(robot.Position))
        {
            throw new ArgumentException("Incorrect initial robot position");
        }

        foreach (Command command in commands)
        {
            if (robot.Lost)
            {
                break;
            }

            if (DangerousPositions.Contains(robot.Position) && command is MoveForwardCommand)
            {
                continue;
            }

            Position new_position = command.Execute(robot.Position);
            if (SurfaceGrid.CheckPositionIsInside(new_position))
            {
                robot.Position = new_position;
            }
            else
            {
                robot.Lost = true;
                DangerousPositions.Add(robot.Position);
            }
        }

        ProcessedRobots.Add(robot);
    }
}