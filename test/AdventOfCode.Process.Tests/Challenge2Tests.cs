namespace AdventOfCode.Process.Tests;

public class Challenge2Tests
{
    private readonly int _day;
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge2Tests()
    {
        _day = 2;
        _challenge = new Challenge2();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("8", "test")]
    [InlineData("2156", "patrick")]
    [InlineData("2439", "brian")]
    public void PartATests(string expected, string user)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{_day}.txt");

        // Act
        string actual = _challenge.PartA(data);

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
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{_day}.txt");

        // Act
        string actual = _challenge.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}