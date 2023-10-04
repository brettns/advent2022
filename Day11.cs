namespace advent;

class Day11 {
    Dictionary<Monkey, int> counts = new Dictionary<Monkey, int>();

    public void Run() {
        int numRounds = 20;
        Monkey[] monkeys = ParseInput();
        //PrintMonkeys(monkeys);

        foreach (var monke in monkeys) {
            counts.Add(monke, 0);
        }

        for (int i = 0; i < numRounds; i++) {
            for (int j = 0; j < monkeys.Length; j++) {
                //Console.WriteLine("Monkey: " + j);
                Monkey monke = monkeys[j];
                ProcessMonkey(monkeys, monke);
            }
        }

        PrintMonkeys(monkeys);
        var result = counts.Values.OrderByDescending(x => x).ToArray();

        Console.WriteLine(string.Join(Environment.NewLine, counts.Values));

        Console.WriteLine("Total monkey business: " + result[0] * (long)result[1]);
    }

    void ProcessMonkey(Monkey[] monkeys, Monkey monke) {
        while (monke.Items.Count > 0) {
            counts[monke]++;
            long worryLevel = monke.Items.Dequeue();
            long nextValue = RunOperation(monke.Operation, worryLevel);

            if (nextValue % monke.Divisor == 0) {
                // am worried
                monkeys[monke.Success].Items.Enqueue(nextValue);
            }
            else {
                //Console.WriteLine($"    Monkey gets bored with item. Worry level is divided by 3 to {nextValue}.");
                //Console.WriteLine($"    Current worry level is not divisible by {monke.Divisor}.");
                //Console.WriteLine($"    Item with worry level {nextValue} is thrown to monkey {monke.Fail}.");
                monkeys[monke.Fail].Items.Enqueue(nextValue);
            }
        }
    }

    void PrintMonkeys(Monkey[] monkeys) {
        foreach (var monke in monkeys) {
            var queue = new Queue<long>(monke.Items);
            Console.Write("Monkey Items: ");
            while (queue.Count > 0) {
                Console.Write(queue.Dequeue() + " ");
            }

            Console.WriteLine();
        }
    }

    private long RunOperation(string expression, long oldValue) {
        string[] parts = expression
          .Replace("new = ", "")
          .Replace("old", oldValue.ToString())
          .Split(' ');

        long a = long.Parse(parts[0]);
        long b = long.Parse(parts[2]);
        //Console.WriteLine($"    Exp: {a} {parts[1]} {b}");
        return parts[1] switch {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => a / b,
            _ => throw new Exception("What is this: " + parts[1])
        };
    }

    Monkey[] ParseInput() {
        string text = File.ReadAllText(@"data\day11_sample.txt");

        string[] rawMonkeys = System.Text.RegularExpressions.Regex.Split(text, @"Monkey \d+:")
          .Select(s => s.Trim())
          .Where(s => !string.IsNullOrEmpty(s))
          .ToArray();

        Monkey[] monkeys = new Monkey[rawMonkeys.Length];
        int i = 0;

        foreach (var raw in rawMonkeys) {
            var lines = raw.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
              .Select(s => s.Substring(s.IndexOf(": ") + 2))
              .ToArray();


            var items = lines[0].Split(", ").Select(s => long.Parse(s));

            int divisor = int.Parse(lines[2].Substring(lines[2].LastIndexOf(' ')));
            int success = int.Parse(lines[3].Substring(lines[3].LastIndexOf(' ')));
            int fail = int.Parse(lines[4].Substring(lines[4].LastIndexOf(' ')));

            monkeys[i++] = new Monkey {
                Items = new Queue<long>(items),
                Operation = lines[1],
                Divisor = divisor,
                Success = success,
                Fail = fail,
            };
        }

        return monkeys;
    }

    class Monkey {
        public Queue<long> Items { get; set; } = new Queue<long>();
        public string Operation { get; set; } = string.Empty;
        public int Divisor { get; set; }
        public int Success { get; set; }
        public int Fail { get; set; }
    }
}