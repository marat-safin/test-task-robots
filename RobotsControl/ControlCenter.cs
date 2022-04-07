using System;

namespace RobotsControl;

public class ControlCenter
{
    private const int MAX_GRID_DIMENSION = 50;
    public ControlCenter(Grid grid)
    {
        if (grid.MaxX > MAX_GRID_DIMENSION || grid.MaxY > MAX_GRID_DIMENSION)
        {
            throw new ArgumentException("Unsupported grid size");
        }
        SurfaceGrid = grid;
    }

    private Grid SurfaceGrid { get; set; }

    public void AddRobot(Robot robot)
    {
        if (!SurfaceGrid.ValidatePosition(robot.Position))
        {
            throw new ArgumentException("Incorrect robot position");
        }
    }
    public void ExecuteCommandSequence()
    {

    }
}