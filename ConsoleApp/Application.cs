using System;
using RobotsControl;


namespace ConsoleApp;

public class Application
{
    public void Execute(string[] args)
    {
        if (args.Length < 1)
        {
            throw new ArgumentException("Empty args");
        }
        Grid grid = CreateGrid(args[0]);
        ControlCenter control_center = new ControlCenter(grid);
    }

    private Grid CreateGrid(string grid_parameters)
    {
        string[] parameters = grid_parameters.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (parameters.Length < 2)
        {
            throw new ArgumentException("Incorrect grid dimension parameter");
        }
        int max_x;
        int max_y;
        if (!Int32.TryParse(parameters[0], out max_x) || !Int32.TryParse(parameters[1], out max_y))
        {
            throw new ArgumentException("Can't parse grid dimension parameters");
        }
        return new Grid(max_x, max_y);
    }
}