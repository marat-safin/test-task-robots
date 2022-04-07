using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        int i = 1;
        while (i < args.Length - 1)
        {
            string robot_position_str = args[i];
            string commands_str = args[i + 1];

            Robot robot = CreateRobot(robot_position_str);
            List<Command> commands = ParseCommands(commands_str);
            control_center.ManipulateRobot(robot, commands);

            i += 2;
        }

        ImmutableList<Robot> robots = control_center.GetRobots();
        foreach (Robot robot in robots)
        {
            Console.WriteLine(GetRobotStatusString(robot));
        }
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

    private Robot CreateRobot(string robot_position_str)
    {
        string[] parameters = robot_position_str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if (parameters.Length < 3)
        {
            throw new ArgumentException("Incorrect robot position parameters");
        }
        
        int x;
        int y;
        if (!Int32.TryParse(parameters[0], out x) || !Int32.TryParse(parameters[1], out y))
        {
            throw new ArgumentException("Can't parse robot's coordinates");
        }

        Direction direction;
        switch (parameters[2].ToUpper())
        {
            case "N":
                direction = Direction.North;
                break;
            case "S":
                direction = Direction.South;
                break;
            case "W":
                direction = Direction.West;
                break;
            case "E":
                direction = Direction.East;
                break;
            default:
                throw new ArgumentException("Can't parse robot's direction");
        }

        Position robot_position = new Position(x, y, direction);
        return new Robot(robot_position);
    }

    private List<Command> ParseCommands(string commands_str)
    {
        List<Command> commands = new List<Command>();
        String normalized = commands_str.ToUpper().Trim();
        foreach (char c in normalized.ToCharArray())
        {
            switch (c)
            {
                case 'L':
                    commands.Add(new TurnLeftCommand());
                    break;
                case 'R':
                    commands.Add(new TurnRightCommand());
                    break;
                case 'F':
                    commands.Add(new MoveForwardCommand());
                    break;
                default:
                    throw new ArgumentException("Can't parse command");
            }
        }

        return commands;
    }
    
    private string GetRobotStatusString(Robot robot)
    {
        string direction;
        switch (robot.Position.Direction)
        {
            case Direction.North:
                direction = "N";
                break;
            case Direction.South:
                direction = "S";
                break;
            case Direction.West:
                direction = "W";
                break;
            case Direction.East:
                direction = "E";
                break;
            default:
                throw new ArgumentException("Unknown direction");
        }

        string status = $"{robot.Position.X} {robot.Position.Y} {direction}";
        if (robot.Lost)
        {
            status += " LOST";
        }

        return status;
    }
}