namespace AdventOfCode.Foundation;

public interface IDataAccess {
    public string[] GetData(string year, string user, string number);
}