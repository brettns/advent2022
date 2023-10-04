namespace advent;

class Day02 {
  const string scoreChoiceArray = "XYZ";

  readonly string[] input = File.ReadAllLines(@"data\day02.txt");
  
  readonly Dictionary<char, Dictionary<char, int>> scoreMap = new() {
    { 
      'A', new Dictionary<char, int> {
        { 'X', 3 },
        { 'Y', 6 },
        { 'Z', 0 },
      }
    }, {
      'B', new Dictionary<char, int> {
        { 'X', 0 },
        { 'Y', 3 },
        { 'Z', 6 },
      }
    }, {
      'C', new Dictionary<char, int> {
        { 'X', 6 },
        { 'Y', 0 },
        { 'Z', 3 },
      }
    }
  };

  public void Run() {
    int total = 0;

    foreach (string line in input) {
      char chosenStrat = ResolveStrategy(line[2], line[0]);
      int choiceScore = scoreChoiceArray.IndexOf(chosenStrat) + 1;
      int playScore = scoreMap[line[0]][chosenStrat];
      int playTotal = playScore + choiceScore;
      total += playTotal;
      Console.WriteLine($"{playTotal} + {choiceScore} + {playScore}");
    }

    Console.WriteLine("Total: " + total);
  }

  private char ResolveStrategy(char outcome, char opponentMove)
  {
    return outcome switch {
    //'Y' => (char)(opponentMove + ('X' - 'A')), // also works
      'Y' => scoreMap[opponentMove].OrderBy(x => x.Value).Skip(1).First().Key,
      'X' => scoreMap[opponentMove].OrderBy(x => x.Value).First().Key,
      'Z' => scoreMap[opponentMove].OrderBy(x => x.Value).Last().Key,
       _  => throw new Exception("Unknown move?")
    };
  }
}