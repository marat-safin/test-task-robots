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
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_GridMaxXOutOfRange()
    {
        Application app = new Application();
        app.Execute(new string[] { "2000000000 10" });
        Assert.Fail("ArgumentException was expected");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExecute_GridMaxYOutOfRange()
    {
        Application app = new Application();
        app.Execute(new string[] { "10 2000000000" });
        Assert.Fail("ArgumentException was expected");
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
    public void TestExecute()
    {
        Application app = new Application();
        string[] input;
        string[] expected;
        string[] actual;

        // Sample case from programming problem statement
        input = new string[] { "5 3", "1 1 E", "RFRFRFRF", "3 2 N", "FRRFLLFFRRFLL", "0 3 W", "LLFFFLFLFL"};
        expected = new string[] { "1 1 E", "3 3 N LOST", "2 3 S" };
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // Lowercase is also accepted
        input = new string[] { "5 3", "1 1 e", "rfrfrfrf", "3 2 n", "frrfllffrrfll", "0 3 w", "llffflflfl"};
        expected = new string[] { "1 1 E", "3 3 N LOST", "2 3 S" };
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // Has robot position but no command, so ignore
        input = new string[] { "5 5", "2 2 N"};
        expected = new string[0];
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // Zigzag travel across all grid points, then fall off
        input = new string[] { "4 4", "0 0 N", "RFFFFLFLFFFFRFRFFFFLFLFFFFRFRFFFFF"};
        expected = new string[] { "4 4 E LOST"};
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // First robot falls from corner point, second falls from same point but in different direction
        input = new string[] { "1 1", "1 0 W", "FF", "0 1 S", "FF"};
        expected = new string[] { "0 0 W LOST", "0 0 S LOST"};
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // If robot fell off, it ignores further commands
        input = new string[] { "1 1", "1 0 W", "FFFFLLFF"};
        expected = new string[] { "0 0 W LOST"};
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // First robot falls off, second robot follows him, but refuse to fall and returns back
        input = new string[] { "2 0", "1 0 W", "FF", "2 0 W", "FFFFFFRRFF"};
        expected = new string[] { "0 0 W LOST", "2 0 E"};
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);

        // Two robots, one moves through another
        input = new string[] { "2 0", "0 0 E", "F", "2 0 W", "FF"};
        expected = new string[] { "1 0 E", "0 0 W"};
        actual = app.Execute(input);
        CollectionAssert.AreEqual(expected, actual);
    }
}