namespace AdventOfCode.Process.Tests;

public class Challenge3Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Challenge3Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day3();
        _dayNumber = 3;
    }

    [Theory]
    [InlineData("4361", "test")]
    [InlineData("528799", "patrick")]
    [InlineData("527144", "brian")]
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
    [InlineData("467835", "test")]
    [InlineData("84907174", "patrick")]
    [InlineData("81463996", "brian")]
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