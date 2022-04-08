using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class ControCenterTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestManipulateRobot_RobotToTheWestOfGrid()
    {
        ControlCenter control_center = new ControlCenter(new Grid(10, 10));
        Robot robot = new Robot(new Position(-1, 5, Direction.North));
        control_center.ManipulateRobot(robot, new List<ICommand>());
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestManipulateRobot_RobotToTheEastOfGrid()
    {
        ControlCenter control_center = new ControlCenter(new Grid(10, 10));
        Robot robot = new Robot(new Position(15, 5, Direction.North));
        control_center.ManipulateRobot(robot, new List<ICommand>());
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestManipulateRobot_RobotToTheNorthOfGrid()
    {
        ControlCenter control_center = new ControlCenter(new Grid(10, 10));
        Robot robot = new Robot(new Position(5, 15, Direction.North));
        control_center.ManipulateRobot(robot, new List<ICommand>());
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestManipulateRobot_RobotToTheSouthOfGrid()
    {
        ControlCenter control_center = new ControlCenter(new Grid(10, 10));
        Robot robot = new Robot(new Position(5, -1, Direction.North));
        control_center.ManipulateRobot(robot, new List<ICommand>());
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    public void TestManipulateRobot()
    {
        ControlCenter control_center;
        Robot robot1;
        Robot robot2;
        List<ICommand> commands1;
        List<ICommand> commands2;
        ImmutableList<Robot> processed_robots;

        // One robot, empty commands list
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(2, 2, Direction.North));
        commands1 = new List<ICommand>(0);
        control_center.ManipulateRobot(robot1, commands1);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(1, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.AreEqual(new Position(2, 2, Direction.North), robot1.Position);
        Assert.IsFalse(robot1.Lost);

        // One robot, rotates 360 clockwise
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(2, 2, Direction.North));
        commands1 = new List<ICommand> {
            new TurnLeftCommand(),
            new TurnLeftCommand(),
            new TurnLeftCommand(),
            new TurnLeftCommand(),
        };
        control_center.ManipulateRobot(robot1, commands1);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(1, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.AreEqual(new Position(2, 2, Direction.North), robot1.Position);
        Assert.IsFalse(robot1.Lost);

        // One robot, rotates 360 counterclockwise
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(2, 2, Direction.North));
        commands1 = new List<ICommand> {
            new TurnRightCommand(),
            new TurnRightCommand(),
            new TurnRightCommand(),
            new TurnRightCommand(),
        };
        control_center.ManipulateRobot(robot1, commands1);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(1, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.AreEqual(new Position(2, 2, Direction.North), robot1.Position);
        Assert.IsFalse(robot1.Lost);

        // One robot rotates 180 and moves 1 time
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(0, 0, Direction.South));
        commands1 = new List<ICommand> {
            new TurnRightCommand(),
            new TurnRightCommand(),
            new MoveForwardCommand()
        };
        control_center.ManipulateRobot(robot1, commands1);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(1, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.AreEqual(new Position(0, 1, Direction.North), robot1.Position);
        Assert.IsFalse(robot1.Lost);

        // One robot falls off and ignore further commands
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(0, 2, Direction.West));
        commands1 = new List<ICommand> {
            new MoveForwardCommand(),
            new MoveForwardCommand(),
            new MoveForwardCommand()
        };
        control_center.ManipulateRobot(robot1, commands1);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(1, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.AreEqual(new Position(0, 2, Direction.West), robot1.Position);
        Assert.IsTrue(robot1.Lost);

        // Two robots spawned in the same point, no commands
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(1, 1, Direction.West));
        robot2 = new Robot(new Position(1, 1, Direction.West));
        commands1 = new List<ICommand>(0);
        commands2 = new List<ICommand>(0);
        control_center.ManipulateRobot(robot1, commands1);
        control_center.ManipulateRobot(robot2, commands2);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(2, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.ReferenceEquals(processed_robots[0], robot2);
        Assert.AreEqual(new Position(1, 1, Direction.West), robot1.Position);
        Assert.IsFalse(robot1.Lost);
        Assert.AreEqual(new Position(1, 1, Direction.West), robot2.Position);
        Assert.IsFalse(robot2.Lost);

        // Two robots, one moves through another
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(1, 1, Direction.South));
        robot2 = new Robot(new Position(1, 0, Direction.North));
        commands1 = new List<ICommand>(0);
        commands2 = new List<ICommand> {
            new MoveForwardCommand(),
            new MoveForwardCommand()
        };
        control_center.ManipulateRobot(robot1, commands1);
        control_center.ManipulateRobot(robot2, commands2);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(2, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.ReferenceEquals(processed_robots[0], robot2);
        Assert.AreEqual(new Position(1, 1, Direction.South), robot1.Position);
        Assert.IsFalse(robot1.Lost);
        Assert.AreEqual(new Position(1, 2, Direction.North), robot2.Position);
        Assert.IsFalse(robot2.Lost);

        // Two robots: one falls off, another ignores multiple commands to follow him, then turns and go away
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(5, 0, Direction.East));
        robot2 = new Robot(new Position(4, 0, Direction.East));
        commands1 = new List<ICommand> {
            new MoveForwardCommand()
        };
        commands2 = new List<ICommand> {
            new MoveForwardCommand(),
            new MoveForwardCommand(),
            new MoveForwardCommand(),
            new TurnLeftCommand(),
            new MoveForwardCommand(),
        };
        control_center.ManipulateRobot(robot1, commands1);
        control_center.ManipulateRobot(robot2, commands2);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(2, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.ReferenceEquals(processed_robots[0], robot2);
        Assert.AreEqual(new Position(5, 0, Direction.East), robot1.Position);
        Assert.IsTrue(robot1.Lost);
        Assert.AreEqual(new Position(5, 1, Direction.North), robot2.Position);
        Assert.IsFalse(robot2.Lost);

        // Two robots: one falls off, another passes dangerous point in parallel to the endge
        control_center = new ControlCenter(new Grid(5, 5));
        robot1 = new Robot(new Position(5, 2, Direction.East));
        robot2 = new Robot(new Position(5, 1, Direction.North));
        commands1 = new List<ICommand> {
            new MoveForwardCommand()
        };
        commands2 = new List<ICommand> {
            new MoveForwardCommand(),
            new MoveForwardCommand(),
        };
        control_center.ManipulateRobot(robot1, commands1);
        control_center.ManipulateRobot(robot2, commands2);
        processed_robots = control_center.GetProcessedRobots();
        Assert.AreEqual(2, processed_robots.Count);
        Assert.ReferenceEquals(processed_robots[0], robot1);
        Assert.ReferenceEquals(processed_robots[0], robot2);
        Assert.AreEqual(new Position(5, 2, Direction.East), robot1.Position);
        Assert.IsTrue(robot1.Lost);
        Assert.AreEqual(new Position(5, 3, Direction.North), robot2.Position);
        Assert.IsFalse(robot2.Lost);
    }
}