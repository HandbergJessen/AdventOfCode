namespace AdventOfCode.Process.Tests;

public class Challenge1Tests
{
    private readonly int _day;
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge1Tests()
    {
        _day = 1;
        _challenge = new Challenge1();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("351", "test", "2023")]
    [InlineData("55386", "patrick", "2023")]
    [InlineData("53386", "brian", "2023")]
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
    [InlineData("423", "test", "2023")]
    [InlineData("54824", "patrick", "2023")]
    [InlineData("53312", "brian", "2023")]
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