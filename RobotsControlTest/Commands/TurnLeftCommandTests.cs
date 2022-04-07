using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class TurnLeftCommandTests
{
    [TestMethod]
    public void TestExecute()
    {
        TurnLeftCommand command = new TurnLeftCommand();
        Position start;
        Position expected;
        Position actual;

        // North -> West
        start = new Position(0, 0, Direction.North);
        expected = new Position(0, 0, Direction.West);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // West -> South
        start = new Position(0, 0, Direction.West);
        expected = new Position(0, 0, Direction.South);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // South -> East
        start = new Position(0, 0, Direction.South);
        expected = new Position(0, 0, Direction.East);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // East -> North
        start = new Position(0, 0, Direction.East);
        expected = new Position(0, 0, Direction.North);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }
}