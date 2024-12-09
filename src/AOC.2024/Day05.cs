namespace AOC._2024;

[Puzzle(5, 2024, "Print Queue")]
internal class Day05 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
    {
        var (rules, updates) = ParseInputs(inputLines);

        return
            updates.Where(x => IsValidUpdate(x, rules))
            .Sum(x => x[x.Length / 2]);
    }
    public object SolvePart2(string[] inputLines)
    {
        var (rules, updates) = ParseInputs(inputLines);
        var cm = Comparer<int>.Create((left, righ) => rules.Any(rule => rule.Contains(left) && rule[0] == righ) ? 1 : -1);

        return
            updates.Where(x => !IsValidUpdate(x, rules))
            .Select(x => x.OrderBy(x => x, cm).ToArray())
            .Sum(x => x[x.Length / 2]);

    }
    static (List<int[]> rules, List<int[]> updates) ParseInputs(string[] inputLines)
    {
        var rules = inputLines.Where(x => x.Contains('|'))
            .Select(x => x.Split('|')
                .Select(int.Parse)
                .ToArray())
            .ToList();

        var updates = inputLines.Where(x => x.Contains(','))
            .Select(x => x.Split(',')
                .Select(int.Parse)
                .ToArray())
            .ToList();

        return (rules, updates);
    }
    static bool IsValidUpdate(int[] update, List<int[]> rules)
        => !update.Zip(update.Skip(1))
            .Any(pair => rules.Any(rule => rule.Contains(pair.First) && rule[0] == pair.Second));
}
