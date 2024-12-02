namespace AOC._2024;

[Puzzle(1, 2024, "Historian Hysteria")]
public class Day1 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
    {
        var (left, right) = Split(inputLines);

        left = [.. left.Order()];
        right = [.. right.Order()];

        return
            left.Select(
                (l, i) => l >= right[i]
                ? l - right[i]
                : right[i] - l)
            .Sum();
    }
    public object SolvePart2(string[] inputLines)
    {
        var (left, right) = Split(inputLines);

        return
            left.Select(l => l * right.Count(r => r == l))
            .Sum();
    }
    static (long[] left, long[] right) Split(string[] inputLines)
    {
        var lines = inputLines.Select(x => (x.Split("  ")[0], x.Split("  ")[1]));

        long[] left = [.. lines.Select(x => long.Parse(x.Item1))];
        long[] right = [.. lines.Select(x => long.Parse(x.Item2))];

        return (left, right);
    }
}
