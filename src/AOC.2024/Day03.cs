namespace AOC._2024;

[Puzzle(3, 2024, "Mull It Over")]
public class Day03 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
    {
        var mixed = string.Join("", inputLines);
        return
            Regex.Matches(mixed, @"mul\((?<first>\d+),(?<last>\d+)\)")
            .Select(x => long.Parse(x.Groups["first"].Value) * long.Parse(x.Groups["last"].Value))
            .Sum();
    }

    public object SolvePart2(string[] inputLines)
    {
        var mixed = string.Join("", inputLines);
        var cleaned = Regex.Replace(mixed, @"don't\(\).+?(do\(\)|$)", "");
        return SolvePart1([cleaned]);
    }
}
