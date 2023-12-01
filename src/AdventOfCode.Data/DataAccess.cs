using AdventOfCode.Foundation;

namespace AdventOfCode.Data;

public class DataAccess : IDataAccess
{
    public string[] GetData(string user, string year, string day)
    {
        return File.ReadAllLines("../AdventOfCode.Data/data/" + user + "/" + year + "-" + day + ".txt");
    }
}