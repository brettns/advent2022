class Day12 {
    readonly record struct Tile(int X, int Y, int Value, char Sprite);
    readonly Tile[,] grid;
    readonly Tile startTile;
    readonly Tile endTile;

    public Day12() {
        string[] lines = File.ReadAllLines(@"data\day12.txt");
        grid = new Tile[lines[0].Length, lines.Length];

        for (int y = 0; y < grid.GetLength(1); y++) {
            for (int x = 0; x < grid.GetLength(0); x++) {
                if (lines[y][x] == 'S') {
                    grid[x, y] = startTile = new Tile(x, y, 'a' - 'a', 'S');
                }
                else if (lines[y][x] == 'E') {
                    grid[x, y] = endTile = new Tile(x, y, 'z' - 'a', 'E');
                }
                else {
                    grid[x, y] = new Tile(x, y, lines[y][x] - 'a', lines[y][x]);
                }
            }
        }
    }

    public void RunPart1() {
        IList<Tile> path = BFS(startTile, endTile);
        Console.WriteLine("Path Length: " + path.Count);
    }

    public void RunPart2() {
        var lenShortestPath = grid
          .Cast<Tile>()
          .Where(tile => tile.Value == 0)
          .Select(start => BFS(start, endTile).Count)
          .Where(len => len > 0)
          .Min();

        Console.WriteLine("Shortest path: " + lenShortestPath);
    }

    IEnumerable<Tile> GetValidNeighbors(Tile t) {
        if (t.Y - 1 >= 0 && grid[t.X, t.Y - 1].Value <= t.Value + 1)
            yield return grid[t.X, t.Y - 1];
        if (t.X + 1 < grid.GetLength(0) && grid[t.X + 1, t.Y].Value <= t.Value + 1)
            yield return grid[t.X + 1, t.Y];
        if (t.Y + 1 < grid.GetLength(1) && grid[t.X, t.Y + 1].Value <= t.Value + 1)
            yield return grid[t.X, t.Y + 1];
        if (t.X - 1 >= 0 && grid[t.X - 1, t.Y].Value <= t.Value + 1)
            yield return grid[t.X - 1, t.Y];
    }

    IList<Tile> BFS(Tile start, Tile dest) {
        Dictionary<Tile, Tile> parent = new();
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];
        Queue<Tile> queue = new(new[] { start });

        while (queue.Count > 0) {
            Tile node = queue.Dequeue();

            if (node == dest) {
                break; // found
            }

            foreach (Tile n in GetValidNeighbors(node)) {
                if (!visited[n.X, n.Y]) {
                    visited[n.X, n.Y] = true;
                    parent[n] = node;
                    queue.Enqueue(n);
                }
            }
        }

        if (parent.ContainsKey(dest) == false) {
            return new List<Tile>();
        }

        // backtrace our path and reverse it
        Tile par = dest;
        List<Tile> path = new();
        while (par != start) {
            path.Add(par);
            par = parent[par];
        }

        path.Reverse();
        return path;
    }
}