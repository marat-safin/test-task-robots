using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class PositionTests
{
    [TestMethod]
    public void TestEquals()
    {
        Position position = new Position(1, 2, Direction.South);
        Position other;

        Assert.IsFalse(position.Equals(this));
        Assert.IsFalse(position.Equals("Some string"));

        other = new Position(1, 2, Direction.South);
        Assert.IsTrue(position.Equals(other));

        other = new Position(0, 2, Direction.South);
        Assert.IsFalse(position.Equals(other));

        other = new Position(1, 0, Direction.South);
        Assert.IsFalse(position.Equals(other));

        other = new Position(1, 2, Direction.West);
        Assert.IsFalse(position.Equals(other));
    }

    [TestMethod]
    public void TestGetHashCode()
    {
        Position position = new Position(0, 1, Direction.North);
        Position other;

        other = new Position(0, 1, Direction.North);
        Assert.AreEqual(position.GetHashCode(), other.GetHashCode());

        other = new Position(100, 1, Direction.North);
        Assert.AreNotEqual(position.GetHashCode(), other.GetHashCode());
        
        other = new Position(0, -50, Direction.North);
        Assert.AreNotEqual(position.GetHashCode(), other.GetHashCode());

        other = new Position(0, 1, Direction.East);
        Assert.AreNotEqual(position.GetHashCode(), other.GetHashCode());
    }
}