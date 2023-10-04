namespace advent;

class Day01 {
    readonly string input = File.ReadAllText(@"data\day01.txt");

    public void RunPart1() {
        string[] elves = input.Split(Environment.NewLine + Environment.NewLine);

        int result = elves
            .Select(elfGroup => elfGroup.Split(Environment.NewLine).Sum(int.Parse))
            .OrderDescending()
            .First();

        Console.WriteLine(result);
    }

    public void RunPart2() {
        string[] elves = input.Split(Environment.NewLine + Environment.NewLine);

        int result = elves
            .Select(elfGroup => elfGroup.Split(Environment.NewLine).Sum(int.Parse))
            .OrderDescending()
            .Take(3)
            .Sum();

        Console.WriteLine(result);
    }
}
