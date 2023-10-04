namespace advent;

public class Day03 {
  readonly string[] input = File.ReadAllLines(@"data\day03.txt");

  public void Run() {
    HashSet<char> set = new();
    int sum = 0;

    foreach (string line in input) {
      ReadOnlySpan<char> span = line.AsSpan();
      ReadOnlySpan<char> a = span[..(line.Length / 2)];
      ReadOnlySpan<char> b = span[(line.Length / 2)..];

      foreach (char c in a) {
        set.Add(c);
      }

      foreach (char c in b) {
        if (set.Add(c)) {
          set.Remove(c);
        } else {
          sum += GetValue(c);
          break;
        }
      }

      set.Clear();
    }

    Console.WriteLine(sum);
  }

  public void RunPart2() {
    HashSet<char> hash = new();
    int sum = 0;

    for (int i = 0; i < input.Length; i += 3) {
      string a = input[i];
      string b = input[i + 1];
      string c = input[i + 2];

      foreach (char chr in a) {
        hash.Add(chr);
      }

      // Same premise, but if in B, check if it's also in C
      foreach (char chr in b) {
        if (hash.Add(chr)) {
          hash.Remove(chr);
        } else if (c.Contains(chr)) {
          sum += GetValue(chr);
          break;
        } else {
          hash.Remove(chr);
        }
      }

      hash.Clear();
    }

    Console.WriteLine(sum);
  }

  static int GetValue(char c) {
    int value = char.ToLower(c) - 'a' + 1;

    if (char.IsUpper(c)) {
      value += 26;
    }

    return value;
  }
}