class Day09 {
    struct Vec {
        public int X, Y;
        public Vec(int x, int y) => (X, Y) = (x, y);
        public static Vec operator +(Vec a, Vec b) => new(a.X + b.X, a.Y + b.Y);
        public static Vec operator -(Vec a, Vec b) => new(a.X - b.X, a.Y - b.Y);
        public static Vec Clamp(Vec v, int min, int max) => new(
          Math.Min(Math.Max(v.X, min), max),
          Math.Min(Math.Max(v.Y, min), max)
        );
    }

    public void RunPart1() => Simulate(2);
    public void RunPart2() => Simulate(10);

    void Simulate(int ropeLength) {
        HashSet<Vec> tailPoints = new();
        Vec[] rope = new Vec[ropeLength];

        foreach (Vec movement in GetNextMove()) {
            rope[0] += movement;

            for (int i = 1; i < rope.Length; i++) {
                Vec diff = rope[i - 1] - rope[i];

                if (Math.Abs(diff.X) > 1 || Math.Abs(diff.Y) > 1) {
                    rope[i] += Vec.Clamp(diff, -1, 1);
                }
            }

            tailPoints.Add(rope[^1]);
        }

        Console.WriteLine("ANSWER: " + tailPoints.Count);
    }

    IEnumerable<Vec> GetNextMove() {
        string[] lines = File.ReadAllLines(@"data\day09.txt");

        foreach (var line in lines) {
            int count = int.Parse(line[2..]);

            for (int i = 0; i < count; i++) {
                yield return line[0] switch {
                    'R' => new Vec(1, 0),
                    'L' => new Vec(-1, 0),
                    'D' => new Vec(0, -1),
                    'U' => new Vec(0, 1),
                    _ => throw new Exception("Where you goin?")
                };
            }
        }
    }

    void PrintGrid(HashSet<Vec> tailPoints, Vec head, Vec tail) {
        int left = tailPoints.Min(p => p.X);
        int right = Math.Max(tailPoints.Max(p => p.X), 5);
        int bottom = tailPoints.Min(p => p.Y);
        int top = Math.Max(tailPoints.Max(p => p.Y), 5);

        System.Text.StringBuilder sb = new("\n\n");

        for (int y = bottom; y <= top; y++) {
            int invY = top - y;

            for (int x = left; x <= right; x++) {
                Vec xy = new(x, invY);

                if (xy.Equals(head)) {
                    sb.Append('H');
                }
                else if (xy.Equals(tail)) {
                    sb.Append('T');
                }
                else if (tailPoints.Any(p => p.Equals(xy))) {
                    sb.Append('#');
                }
                else {
                    sb.Append('.');
                }
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }
}