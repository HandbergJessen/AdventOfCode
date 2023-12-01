namespace AdventOfCode.Data;

public class DataAccess : IDataAccess
{
    public string[] GetData(string path)
    {
        return File.ReadAllLines(path);
    }
}