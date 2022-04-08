using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class TurnLeftCommandTests
{
    [TestMethod]
    public void TestExecute_NorthToWest()
    {
        TurnLeftCommand command = new TurnLeftCommand();
        Position start = new Position(0, 0, Direction.North);
        Position expected = new Position(0, 0, Direction.West);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_WestToSouth()
    {
        TurnLeftCommand command = new TurnLeftCommand();
        Position start = new Position(0, 0, Direction.West);
        Position expected = new Position(0, 0, Direction.South);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_SouthToEast()
    {
        TurnLeftCommand command = new TurnLeftCommand();
        Position start = new Position(0, 0, Direction.South);
        Position expected = new Position(0, 0, Direction.East);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_EastToNorth()
    {
        TurnLeftCommand command = new TurnLeftCommand();
        Position start = new Position(0, 0, Direction.East);
        Position expected = new Position(0, 0, Direction.North);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }
}