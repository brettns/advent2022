namespace advent;

public class Day08 {
    readonly string[] lines;
    readonly int gridSize;
    readonly int[,] treeHeights;
    readonly bool[,] treeVisible;

    public Day08() {
        lines = File.ReadAllLines(@"input.txt");
        treeHeights = new int[lines[0].Length, lines.Length];
        treeVisible = new bool[lines[0].Length, lines.Length];
        gridSize = treeHeights.GetLength(0);

        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                treeHeights[x, y] = int.Parse(lines[y][x].ToString());
            }
        }
    }

    public void Run() {
        for (int i = 0; i < gridSize; i++) {
            ScanDirection(1, 0, 0, i);             // Look Right
            ScanDirection(-1, 0, gridSize - 1, i); // Look Left
            ScanDirection(0, 1, i, 0);             // Look Down
            ScanDirection(0, -1, i, gridSize - 1); // Look Up
        }

        PrintGrid();
        Console.WriteLine();
        Console.WriteLine("Trees visible: " + CountVisible());
    }

    public void RunPart2() {
        int maxValue = -1;
        int maxX = 0;
        int maxY = 0;

        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                int treeValue = ScanCardinalDirections(x, y);

                if (treeValue > maxValue) {
                    maxValue = treeValue;
                    maxX = x;
                    maxY = y;
                }
            }
        }

        Console.WriteLine($"Highest Value: {maxValue} {maxX}, {maxY}");
    }

    int ScanCardinalDirections(int x, int y) {
        int e = ScanOutFrom(1, 0, x, y); // Look Right
        int w = ScanOutFrom(-1, 0, x, y); // Look Left
        int s = ScanOutFrom(0, 1, x, y); // Look Down
        int n = ScanOutFrom(0, -1, x, y); // Look Up
        int treeValue = n * e * s * w;

        Console.WriteLine($"{x},{y}: {treeValue} ({n} * {w} * {s} * {e})");
        return treeValue;
    }

    int ScanOutFrom(int dirX, int dirY, int startX, int startY) {
        int treeHeight = treeHeights[startX, startY];
        int x = startX + dirX;
        int y = startY + dirY;
        int numVisible = 0;

        while (x >= 0 && x < gridSize && y >= 0 && y < gridSize) {
            numVisible++;

            if (treeHeights[x, y] >= treeHeight) {
                break;
            }

            x += dirX;
            y += dirY;
        }

        return numVisible;
    }

    int ScanDirection(int dirX, int dirY, int startX, int startY) {
        // First tree visible
        int tallestTree = -1;
        int x = startX;
        int y = startY;
        int numVisible = 0;

        while (x >= 0 && x < gridSize && y >= 0 && y < gridSize) {
            if (treeHeights[x, y] > tallestTree) {
                treeVisible[x, y] = true;
                tallestTree = treeHeights[x, y];
                numVisible++;
            }

            x += dirX;
            y += dirY;
        }

        return numVisible;
    }

    int CountVisible() {
        int numVisible = 0;

        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                if (treeVisible[x, y]) {
                    numVisible++;
                }
            }
        }

        return numVisible;
    }

    void PrintGrid() {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                if (treeVisible[x, y]) {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                Console.Write(treeHeights[x, y]);
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.Gray;
    }
}