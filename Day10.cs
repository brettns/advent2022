class Day10 {
  readonly string[] input = File.ReadAllLines(@"data\day10.txt");

  class Display {
    private readonly int w;
    public Display(int width) => w = width;
    public int Index { get; private set; }
    public int PosX => Index % w;

    public void DrawNextPixel(char c) {
      Thread.Sleep(10);
      Console.Write(c);

      if (++Index % w == 0) {
        Console.WriteLine();
      }
    }
  }

  public void Run() {
    int answer = 0;
    int cycle = 0;
    int x = 1;
    Display display = new(40);

    foreach (string line in input) {
      int cyclesRemaining = 0;
      Action? op = null;

      if (line.StartsWith("noop")) {
        cyclesRemaining = 1;
      } else if (line.StartsWith("addx")) {
        cyclesRemaining = 2;
        int value = int.Parse(line[5..]);
        op = () => x += value;
      }

      while (cyclesRemaining-- > 0) {
        char pixel = display.PosX >= x - 1 && display.PosX <= x + 1 ? '█' : ' ';
        display.DrawNextPixel(pixel);

        if ((++cycle - 20) % 40 == 0) {
          answer += x * cycle;
        }
      }

      op?.Invoke();
    }

    Console.WriteLine("\nAnswer: " + answer);
  }
}