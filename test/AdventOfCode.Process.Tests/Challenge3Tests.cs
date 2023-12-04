namespace AdventOfCode.Process.Tests;

public class Challenge3Tests
{
    private readonly int _day;
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge3Tests()
    {
        _day = 3;
        _challenge = new Challenge3();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("4361", "test", "2023")]
    [InlineData("528799", "patrick", "2023")]
    [InlineData("527144", "brian", "2023")]
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
    [InlineData("467835", "test", "2023")]
    [InlineData("84907174", "patrick", "2023")]
    [InlineData("81463996", "brian", "2023")]
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