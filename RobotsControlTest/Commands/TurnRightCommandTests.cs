using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class TurnRightCommandTests
{
    [TestMethod]
    public void TestExecute_NorthToEast()
    {
        TurnRightCommand command = new TurnRightCommand();
        Position start = new Position(0, 0, Direction.North);
        Position expected = new Position(0, 0, Direction.East);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_EastToSouth()
    {
        TurnRightCommand command = new TurnRightCommand();
        Position start = new Position(0, 0, Direction.East);
        Position expected = new Position(0, 0, Direction.South);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_SouthToWest()
    {
        TurnRightCommand command = new TurnRightCommand();
        Position start = new Position(0, 0, Direction.South);
        Position expected = new Position(0, 0, Direction.West);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_WestToNorth()
    {
        TurnRightCommand command = new TurnRightCommand();
        Position start = new Position(0, 0, Direction.West);
        Position expected = new Position(0, 0, Direction.North);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }
}