namespace AdventOfCode.Process.Tests;

public class Challenge2Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Challenge2Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day2();
        _dayNumber = 2;
    }

    [Theory]
    [InlineData("8", "test")]
    [InlineData("2156", "patrick")]
    [InlineData("2439", "brian")]
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
    [InlineData("2286", "test")]
    [InlineData("66909", "patrick")]
    [InlineData("63711", "brian")]
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