namespace AdventOfCode.Process.Tests;

public class Challenge2Tests
{
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge2Tests()
    {
        _challenge = new Challenge2();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("8", "test", "2023", "2")]
    [InlineData("2156", "patrick", "2023", "2")]
    [InlineData("2439", "brian", "2023", "2")]
    public void PartATests(string expected, string user, string year, string day)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{year}-{day}.txt");

        // Act
        string actual = _challenge.PartA(data);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("2286", "test", "2023", "2")]
    [InlineData("66909", "patrick", "2023", "2")]
    [InlineData("63711", "brian", "2023", "2")]
    public void PartBTests(string expected, string user, string year, string day)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{year}-{day}.txt");

        // Act
        string actual = _challenge.PartB(data);

        // Assert
        Assert.Equal(expected, actual);
    }
}