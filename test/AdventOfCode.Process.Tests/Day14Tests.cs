namespace AdventOfCode.Process.Tests;

public class Day14Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Day14Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day14();
        _dayNumber = 14;
    }

    [Fact]
    public void PartATests()
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/test/{_dayNumber}.txt");
        string expected = "136";

        // Act
        string actual = _day.PartA(data);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void PartBTests()
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/test/{_dayNumber}.txt");
        string expected = "64";

        // Act
        string actual = _day.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}