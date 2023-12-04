namespace AdventOfCode.Process.Tests;

public class Day4Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Day4Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day4();
        _dayNumber = 4;
    }

    [Theory]
    [InlineData("13", "test")]
    [InlineData("15205", "patrick")]
    public void PartATests(string expected, string user)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{_dayNumber}.txt");

        // Act
        string actual = _day.PartA(data);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("30", "test")]
    public void PartBTests(string expected, string user)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{_dayNumber}.txt");

        // Act
        string actual = _day.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}