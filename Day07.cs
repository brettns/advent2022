namespace advent; 

public class Day07 {
    const long maxDirSize = 100000;

    public class PathNode {
        public PathNode(string path, PathNode? parent) {
            Parent = parent;
            Path = path;
        }

        public string Path { get; set; }
        public long Size { get; set; }
        public PathNode? Parent { get; set; }
        public List<PathNode> Children { get; } = new List<PathNode>();
    }

    public void Run() {
        string[] lines = File.ReadAllLines(@"data\day07.txt");
        PathNode rootNode = new("/", null);
        PathNode currNode = rootNode;
        List<PathNode> listNodes = new() { rootNode };

        foreach (var line in lines) {
            if (line.StartsWith("$ cd ")) {
                string dirName = line["$ cd ".Length..];

                if (dirName == "..") {
                    currNode = currNode.Parent ?? throw new Exception("Cannot go beyond root");
                }
                else {
                    string path = Path.Combine(currNode.Path, dirName).Replace("\\", "/");
                    PathNode nextNode = new(path, currNode);
                    currNode.Children.Add(nextNode);
                    currNode = nextNode;
                    listNodes.Add(nextNode);
                }
            }
            else if (line.Length > 0 && char.IsNumber(line[0])) {
                var fileSize = long.Parse(line[..line.IndexOf(' ')]);
                currNode.Size += fileSize;
            }
        }

        // Part 1
        PrintNode(rootNode);
        PrintAnswer(listNodes);

        // Part 2
        long spaceUsed = SumNode(rootNode);
        long minToFree = GetSpaceToBeFreed(spaceUsed);
        long answer2 = listNodes
          .Select(SumNode)
          .Where(size => size >= minToFree)
          .Min();

        Console.WriteLine("Answer2: " + answer2);
    }

    private long GetSpaceToBeFreed(long spaceUsed) {
        long totalSpace = 70000000;
        long requiredSpace = 30000000;
        long freeSpace = totalSpace - spaceUsed;
        long minToFree = requiredSpace - freeSpace;
        return minToFree;
    }

    private void PrintAnswer(List<PathNode> listNodes) {
        long answer = 0;

        foreach (PathNode node in listNodes) {
            long size = SumNode(node);
            Console.WriteLine($"Total: {node.Path} -> {size}");

            if (size < maxDirSize) {
                answer += size;
            }
        }

        Console.WriteLine("Answer");
        Console.WriteLine(answer);
    }

    private void PrintNode(PathNode node) {
        Console.WriteLine($"Self: {node.Path} -> {node.Size}");

        foreach (var child in node.Children) {
            PrintNode(child);
        }
    }

    private long SumNode(PathNode node) {
        long total = 0;

        foreach (var child in node.Children) {
            total += SumNode(child);
        }

        return total + node.Size;
    }
}