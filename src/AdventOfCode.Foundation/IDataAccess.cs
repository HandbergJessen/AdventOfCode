namespace AdventOfCode.Foundation;

public interface IDataAccess
{
    public string[] GetData(string user, string year, string number);
}