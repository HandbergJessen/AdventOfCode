namespace AdventOfCode.Process.Tests;

public class Challenge1Tests
{
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge1Tests()
    {
        _challenge = new Challenge1();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("351", "test", "2023", "1")]
    [InlineData("55386", "patrick", "2023", "1")]
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
    [InlineData("423", "test", "2023", "1")]
    [InlineData("54824", "patrick", "2023", "1")]
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