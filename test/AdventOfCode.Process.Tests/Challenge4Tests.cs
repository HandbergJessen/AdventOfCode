namespace AdventOfCode.Process.Tests;

public class Challenge4Tests
{
    private readonly int _day;
    private readonly IChallenge _challenge;
    private readonly IDataAccess _dataAccess;

    public Challenge4Tests()
    {
        _day = 4;
        _challenge = new Challenge4();
        _dataAccess = new DataAccess();
    }

    [Theory]
    [InlineData("13", "test", "2023")]
    [InlineData("15205", "patrick", "2023")]
    public void PartATests(string expected, string user, string year)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{year}-{_day}.txt");

        // Act
        string actual = _challenge.PartA(data);

        // Assert
        Assert.Equal(expected, actual);
    }

    /*
        [Theory]
        [InlineData("30", "test", "2023")]
        public void PartBTests(string expected, string user, string year)
        {
            // Arrange
            string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{year}-{_day}.txt");

            // Act
            string actual = _challenge.PartB(data);

            // Assert
            Assert.Equal(expected, actual);
        }
    */
}