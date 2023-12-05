namespace AdventOfCode.Process.Tests;

public class Day5Tests
{
    private readonly IDataAccess _dataAccess;
    private readonly IDay _day;
    private readonly int _dayNumber;

    public Day5Tests()
    {
        _dataAccess = new DataAccess();
        _day = new Day5();
        _dayNumber = 5;
    }

    [Theory]
    [InlineData("35", "test")]
    [InlineData("322500873", "patrick")]
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
    [InlineData("46", "test")]
    //[InlineData("108956227", "patrick")]
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