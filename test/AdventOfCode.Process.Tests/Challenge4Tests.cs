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
    [InlineData("13", "test")]
    [InlineData("15205", "patrick")]
    public void PartATests(string expected, string user)
    {
        // Arrange
        string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{_day}.txt");

        // Act
        string actual = _challenge.PartA(data);

        // Assert
        Assert.Equal(expected, actual);
    }

    /*
        [Theory]
        [InlineData("30", "test")]
        public void PartBTests(string expected, string user)
        {
            // Arrange
            string[] data = _dataAccess.GetData($"../../../../../src/AdventOfCode.Data/data/{user}/{_day}.txt");

            // Act
            string actual = _challenge.PartB(data);

            // Assert
            Assert.Equal(expected, actual);
        }
    */
}