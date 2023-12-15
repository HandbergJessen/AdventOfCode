namespace AdventOfCode.Process.Tests;

public class Day15Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Day15Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day15();
        _dayNumber = 15;
    }

    [Fact]
    public void PartATests()
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/test/{_dayNumber}.txt");
        string expected = "1320";

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
        string expected = "145s";

        // Act
        string actual = _day.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}