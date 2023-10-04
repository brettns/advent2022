using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using System.Text.RegularExpressions;

namespace advent;

class Day05 {
    public void Run() {
        string[] lines = File.ReadAllLines(@"data\day05.txt");
        Regex exp = new(@"move (\d+) from (\d+) to (\d+)");

        Stack<char>[] stacks = ParseHeader(lines);

        for (int i = 10; i < lines.Length; i++) {
            string line = lines[i];
            Match m = exp.Match(line);
            int count = int.Parse(m.Groups[1].Value);
            int fromIndex = int.Parse(m.Groups[2].Value) - 1;
            int toIndex = int.Parse(m.Groups[3].Value) - 1;
            MoveMany(stacks, count, fromIndex, toIndex);
            //MoveOneByOne(stacks, count, fromIndex, toIndex);
        }

        //PrintStacks(stacks);
        PrintAnswer(stacks);
    }

    static void MoveMany(Stack<char>[] stacks, int count, int fromIndex, int toIndex) {
        char[] items = new char[count];
        for (int i = 0; i < count; i++) {
            items[i] = stacks[fromIndex].Pop();
            Console.WriteLine($"Moved {count} from {fromIndex}({stacks[fromIndex].Count}) to {toIndex}({stacks[toIndex].Count})");
        }

        foreach (char item in items.Reverse()) {
            stacks[toIndex].Push(item);
        }
    }

    static void MoveOneByOne(Stack<char>[] stacks, int count, int fromIndex, int toIndex) {
        for (int i = 0; i < count; i++) {
            char movedItem = stacks[fromIndex].Pop();
            stacks[toIndex].Push(movedItem);
        }
    }

    static void PrintAnswer(Stack<char>[] stacks) {
        Console.WriteLine("ANSWER");

        foreach (Stack<char> stack in stacks) {
            Console.Write(stack.Pop());
        }

        Console.WriteLine();
    }

    static void PrintStacks(Stack<char>[] stacks) {
        int longest = stacks.Max(x => x.Count);

        for (int len = longest; len > 0; len--) {
            for (int i = 0; i < stacks.Length; i++) {
                if (stacks[i].Count == len) {
                    char c = stacks[i].Pop();
                    Console.Write($"[{c}] ");
                }
                else {
                    Console.Write(string.Empty.PadLeft(4, ' '));
                }
            }

            Console.WriteLine();
        }
    }

    static Stack<char>[] ParseHeader(string[] lines) {
        // [Z] [Q] [F] [L] [G] [W] [H] [F] [M]
        const int stackCount = 9;
        const int stackHeight = 7;
        const int charsBetweenStacks = 4;

        var stacks = new Stack<char>[stackCount];

        for (int i = 0; i < stacks.Length; i++) {
            stacks[i] = new Stack<char>();
        }

        for (int y = stackHeight; y >= 0; y--) {
            for (int x = 1; x < lines[y].Length; x += charsBetweenStacks) {
                if (char.IsLetter(lines[y][x])) {
                    stacks[x / charsBetweenStacks].Push(lines[y][x]);
                }
            }
        }

        return stacks;
    }
}