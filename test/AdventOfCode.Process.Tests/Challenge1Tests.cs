namespace AdventOfCode.Process.Tests;

public class Challenge1Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Challenge1Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day1();
        _dayNumber = 1;
    }

    [Theory]
    [InlineData("351", "test")]
    [InlineData("55386", "patrick")]
    [InlineData("53386", "brian")]
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
    [InlineData("423", "test")]
    [InlineData("54824", "patrick")]
    [InlineData("53312", "brian")]
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