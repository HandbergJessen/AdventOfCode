using AdventOfCode.Foundation;

namespace AdventOfCode.Data;

public class DataAccess : IDataAccess {   
    public string[] GetData(string year, string user, string number) {
        return File.ReadAllLines("../AdventOfCode.Data/data/" + user + "/" + year + "-" + number + ".txt");        
    }
}