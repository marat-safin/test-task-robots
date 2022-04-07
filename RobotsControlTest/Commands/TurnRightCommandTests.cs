using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class TurnRightCommandTests
{
    [TestMethod]
    public void TestExecute()
    {
        TurnRightCommand command = new TurnRightCommand();
        Position start;
        Position expected;
        Position actual;

        // North -> East
        start = new Position(0, 0, Direction.North);
        expected = new Position(0, 0, Direction.East);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // East -> South
        start = new Position(0, 0, Direction.East);
        expected = new Position(0, 0, Direction.South);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // South -> West
        start = new Position(0, 0, Direction.South);
        expected = new Position(0, 0, Direction.West);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // West -> North
        start = new Position(0, 0, Direction.West);
        expected = new Position(0, 0, Direction.North);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }
}