using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest.Commands;

[TestClass]
public class MoveForwardCommandTests
{
    [TestMethod]
    public void TestExecute_North()
    {
        MoveForwardCommand command = new MoveForwardCommand();
        Position start = new Position(1, 1, Direction.North);
        Position expected = new Position(1, 2, Direction.North);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_South()
    {
        MoveForwardCommand command = new MoveForwardCommand();
        Position start = new Position(1, 1, Direction.South);
        Position expected = new Position(1, 0, Direction.South);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_West()
    {
        MoveForwardCommand command = new MoveForwardCommand();
        Position start = new Position(1, 1, Direction.West);
        Position expected = new Position(0, 1, Direction.West);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_East()
    {
        MoveForwardCommand command = new MoveForwardCommand();
        Position start = new Position(1, 1, Direction.East);
        Position expected = new Position(2, 1, Direction.East);
        Position actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }
}