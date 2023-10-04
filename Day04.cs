namespace advent;

class Day04 {
  readonly string[] lines = File.ReadAllLines(@"data\day04.txt");

  public void Run() {
    int total = 0;

    foreach (string line in lines) {
      string[] pairs = line.Split(",");

      int[] hoursA = pairs[0]
        .Split('-')
        .Select(int.Parse)
        .ToArray();

      int[] hoursB = pairs[1]
        .Split('-')
        .Select(int.Parse)
        .ToArray();

      bool overlapsA = Overlaps(hoursA[0], hoursA[1], hoursB[0], hoursB[1]);
      bool overlapsB = Overlaps(hoursB[0], hoursB[1], hoursA[0], hoursA[1]);

      if (overlapsA || overlapsB) {
        total++;
      }
    }

    Console.WriteLine(total);
  }

  static bool Contains(int startA, int endA, int startB, int endB) {
    return startA <= startB && endA >= endB;
  } 

  static bool Intersects(int x1, int x2, int left, int right) {
    return x1 >= left && x1 <= right 
        || x2 >= left && x2 <= right;
  }

  static bool Overlaps(int startA, int endA, int startB, int endB) {
    return Contains(startA, endA, startB, endB) 
        || Intersects(startA, endA, startB, endB);
  }
}