namespace AdventOfCode.Process.Tests;

public class Challenge3Tests
{
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge3Tests()
    {
        _challenge = new Challenge3();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("4361", "test", "2023", "3")]
    [InlineData("527144", "brian", "2023", "3")]
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
    [InlineData("467835", "test", "2023", "3")]
    [InlineData("81463996", "brian", "2023", "3")]
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