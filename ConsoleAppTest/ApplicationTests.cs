using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp;

namespace ConsoleAppTest;

[TestClass]
public class ApplicationTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_EmptyArgs()
    {
        Application app = new Application();
        app.Execute(new string[0]);
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_MissingGridParameters()
    {
        Application app = new Application();
        app.Execute(new string[] { "1" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_CantParseGridMaxXParameter()
    {
        Application app = new Application();
        app.Execute(new string[] { "A 1" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_CantParseGridMaxYParameter()
    {
        Application app = new Application();
        app.Execute(new string[] { "2 +" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestExecute_GridMaxXOutOfRange()
    {
        Application app = new Application();
        app.Execute(new string[] { "2000000000 10" });
        Assert.Fail("ArgumentOutOfRangeException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestExecute_GridMaxYOutOfRange()
    {
        Application app = new Application();
        app.Execute(new string[] { "10 2000000000" });
        Assert.Fail("ArgumentOutOfRangeException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestExecute_CommandStringLengthOutOfRange()
    {
        Application app = new Application();
        app.Execute(new string[] { "5 7", "1 1 W", "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF" });
        Assert.Fail("ArgumentOutOfRangeException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_CantParseRobotPositionX()
    {
        Application app = new Application();
        app.Execute(new string[] { "5 5", "& 3 N", "F" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_CantParseRobotPositionY()
    {
        Application app = new Application();
        app.Execute(new string[] { "5 5", "3 ? N", "F" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_CantParseRobotDirection()
    {
        Application app = new Application();
        app.Execute(new string[] { "5 5", "1 3 X", "F" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    public void TestExecute_SampleCase()
    {
        // Sample case from programming problem statement
        string[] input = new string[] { "5 3", "1 1 E", "RFRFRFRF", "3 2 N", "FRRFLLFFRRFLL", "0 3 W", "LLFFFLFLFL"};
        string[] expected = new string[] { "1 1 E", "3 3 N LOST", "2 3 S" };
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_LowerCaseInput()
    {
        // Lowercase is also accepted
        string[] input = new string[] { "5 3", "1 1 e", "rfrfrfrf", "3 2 n", "frrfllffrrfll", "0 3 w", "llffflflfl"};
        string[] expected = new string[] { "1 1 E", "3 3 N LOST", "2 3 S" };
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_MissingCommandsParameter()
    {
        // Has robot position but no command, so ignore
        string[] input = new string[] { "5 5", "2 2 N"};
        string[] expected = new string[0];
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_ZigzagAndFallOff()
    {
        // Zigzag travel across all grid points, then fall off
        string[] input = new string[] { "4 4", "0 0 N", "RFFFFLFLFFFFRFRFFFFLFLFFFFRFRFFFFF"};
        string[] expected = new string[] { "4 4 E LOST"};
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_TwoRobotsFallOffCornerPoint()
    {
        // First robot falls from corner point, second falls from same point but in different direction
        string[] input = new string[] { "1 1", "1 0 W", "FF", "0 1 S", "FF"};
        string[] expected = new string[] { "0 0 W LOST", "0 0 S LOST"};
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_FallenRobotIgnoresFurtherCommands()
    {
        // If robot fell off, it ignores further commands
        string[] input = new string[] { "1 1", "1 0 W", "FFFFLLFF"};
        string[] expected = new string[] { "0 0 W LOST"};
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_RobotRefuseToFallAndReturnsBack()
    {
        // First robot falls off, second robot follows him, but refuse to fall and returns back
        string[] input = new string[] { "2 0", "1 0 W", "FF", "2 0 W", "FFFFFFRRFF"};
        string[] expected = new string[] { "0 0 W LOST", "2 0 E"};
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestExecute_RobotMovesThroughAnotherRobot()
    {
        // Two robots, one moves through another
        string[] input = new string[] { "2 0", "0 0 E", "F", "2 0 W", "FF"};
        string[] expected = new string[] { "1 0 E", "0 0 W"};
        string[] actual = new Application().Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }
}