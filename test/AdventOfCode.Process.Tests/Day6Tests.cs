namespace AdventOfCode.Process.Tests;

public class Day6Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Day6Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day6();
        _dayNumber = 6;
    }

    [Fact]
    public void PartATests()
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/test/{_dayNumber}.txt");
        string expected = "288";

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
        string expected = "71503";

        // Act
        string actual = _day.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}