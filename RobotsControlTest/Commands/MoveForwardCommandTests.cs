using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest.Commands;

[TestClass]
public class MoveForwardCommandTests
{
    [TestMethod]
    public void TestExecute()
    {
        MoveForwardCommand command = new MoveForwardCommand();
        Position start;
        Position expected;
        Position actual;
        
        // Move in North direction
        start = new Position(1, 1, Direction.North);
        expected = new Position(1, 2, Direction.North);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // Move in South direction
        start = new Position(1, 1, Direction.South);
        expected = new Position(1, 0, Direction.South);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);

        // Move in West direction
        start = new Position(1, 1, Direction.West);
        expected = new Position(0, 1, Direction.West);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
        
        // Move in East direction
        start = new Position(1, 1, Direction.East);
        expected = new Position(2, 1, Direction.East);
        actual = command.Execute(start);
        Assert.AreEqual(expected, actual);
    }
}