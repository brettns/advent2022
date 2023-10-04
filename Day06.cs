namespace advent;

public class Day06 {
    public void Run() {
        const int msgLength = 14;
        ReadOnlySpan<char> input = File.ReadAllText(@"data\day06.txt").AsSpan();
        HashSet<char> set = new(msgLength);

        for (int i = 0; i < input.Length - msgLength; i++) {
            ReadOnlySpan<char> segment = input.Slice(i, msgLength);

            // boyer-moore style algo
            // scan backwards so we can jump past the furthest duplicate
            for (int j = segment.Length - 1; j >= 0; j--) {
                if (!set.Add(segment[j])) {
                    i += j + 1;
                    break;
                }
            }

            if (set.Count == msgLength) {
                Console.WriteLine("answer: " + (i - 1 + msgLength));
                break;
            }

            set.Clear();
        }
    }
}