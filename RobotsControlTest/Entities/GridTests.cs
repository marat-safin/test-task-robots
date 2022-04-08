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
    public void TestConstructor_RegularCase()
    {
        Grid grid = new Grid(10, 15);
        Assert.AreEqual(10, grid.MaxX);
        Assert.AreEqual(15, grid.MaxY);
    }

    [TestMethod]
    public void TestConstructor_SmallestPossibleGrid()
    {
        Grid grid = new Grid(0, 0);
        Assert.AreEqual(0, grid.MaxX);
        Assert.AreEqual(0, grid.MaxY);
    }

    [TestMethod]
    public void TestCheckPositionIsInside_RegularCase()
    {
        Grid grid = new Grid(10, 10);
        Position position = new Position(2, 3, Direction.North);
        Assert.IsTrue(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_MinimalPossibleCoordinatesInsideGrid()
    {
        Grid grid = new Grid(10, 10);
        Position position = new Position(0, 0, Direction.West);
        Assert.IsTrue(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_MaximalPossibleCoordinatesInsideGrid()
    {
        Grid grid = new Grid(10, 10);
        Position position = new Position(10, 10, Direction.South);
        Assert.IsTrue(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_SinglePointGrid()
    {
        Grid grid = new Grid(0, 0);
        Position position = new Position(0, 0, Direction.East);
        Assert.IsTrue(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_PositionOnTheWestOfGrid()
    {
        Grid grid = new Grid(5, 5);
        Position position = new Position(-1, 2, Direction.North);
        Assert.IsFalse(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_PositionOnTheEastOfGrid()
    {
        Grid grid = new Grid(5, 5);
        Position position = new Position(10, 2, Direction.South);
        Assert.IsFalse(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_PositionOnTheSouthOfGrid()
    {
        Grid grid = new Grid(5, 5);
        Position position = new Position(3, -5, Direction.North);
        Assert.IsFalse(grid.CheckPositionIsInside(position));
    }

    [TestMethod]
    public void TestCheckPositionIsInside_PositionOnTheNorthOfGrid()
    {
        Grid grid = new Grid(5, 5);
        Position position = new Position(3, 15, Direction.North);
        Assert.IsFalse(grid.CheckPositionIsInside(position));
    }
}