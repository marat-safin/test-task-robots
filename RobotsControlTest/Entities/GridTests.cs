using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotsControl;

namespace RobotsControlTest;

[TestClass]
public class GridTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestIncorrectXInConstructor()
    {
        new Grid(-1, 10);
        Assert.Fail("ArgumentOutOfRangeException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestIncorrectYInConstructor()
    {
        new Grid(15, -5);
        Assert.Fail("ArgumentOutOfRangeException was expected");
    }

    [TestMethod]
    public void TestConstructor()
    {
        Grid grid;

        grid = new Grid(10, 15);
        Assert.AreEqual(10, grid.MaxX);
        Assert.AreEqual(15, grid.MaxY);

        grid = new Grid(0, 0);
        Assert.AreEqual(0, grid.MaxX);
        Assert.AreEqual(0, grid.MaxY);
    }

    [TestMethod]
    public void TestCheckPositionIsInside()
    {
        Grid grid;
        Position position;

        grid = new Grid(10, 10);
        position = new Position(2, 3, Direction.North);
        Assert.IsTrue(grid.CheckPositionIsInside(position));

        grid = new Grid(10, 10);
        position = new Position(0, 0, Direction.West);
        Assert.IsTrue(grid.CheckPositionIsInside(position));

        grid = new Grid(10, 10);
        position = new Position(10, 10, Direction.South);
        Assert.IsTrue(grid.CheckPositionIsInside(position));

        grid = new Grid(0, 0);
        position = new Position(0, 0, Direction.East);
        Assert.IsTrue(grid.CheckPositionIsInside(position));


        grid = new Grid(5, 5);
        position = new Position(-1, 2, Direction.North);
        Assert.IsFalse(grid.CheckPositionIsInside(position));

        grid = new Grid(5, 5);
        position = new Position(10, 2, Direction.South);
        Assert.IsFalse(grid.CheckPositionIsInside(position));

        grid = new Grid(5, 5);
        position = new Position(3, -5, Direction.North);
        Assert.IsFalse(grid.CheckPositionIsInside(position));

        grid = new Grid(5, 5);
        position = new Position(3, 15, Direction.North);
        Assert.IsFalse(grid.CheckPositionIsInside(position));
    }
}