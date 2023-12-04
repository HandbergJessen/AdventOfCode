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
    [InlineData("8", "test", "2023")]
    [InlineData("2156", "patrick", "2023")]
    [InlineData("2439", "brian", "2023")]
    public void PartATests(string expected, string user, string year)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{year}-{_day}.txt");

        // Act
        string actual = _challenge.PartA(data);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("2286", "test", "2023")]
    [InlineData("66909", "patrick", "2023")]
    [InlineData("63711", "brian", "2023")]
    public void PartBTests(string expected, string user, string year)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{year}-{_day}.txt");

        // Act
        string actual = _challenge.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}