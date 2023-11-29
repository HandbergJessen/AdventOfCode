using AdventOfCode.Foundation;

namespace AdventOfCode.Process;

public class Challenge1 : IChallenge {
    public string ChallengeA(string[] input) {
        foreach (string line in input) {
            Console.WriteLine(line);
        }

        return "";
    }
    public string ChallengeB(string[] input) {
        throw new NotImplementedException();
    }
}