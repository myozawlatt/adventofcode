namespace AOC._2024;

using System.Linq;

[Puzzle(7, 2024, "Bridge Repair")]
internal class Day07 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
        => ParseInputs(inputLines)
            .Where(x => IsAvalidEquation(x.Key, x.Value))
            .Sum(x => x.Key);
    public object SolvePart2(string[] inputLines)
        => ParseInputs(inputLines)
             .Where(x => IsAvalidEquation(x.Key, x.Value, true))
             .Sum(x => x.Key);

    static Dictionary<long, long[]> ParseInputs(string[] inputLines)
        => inputLines.Select(x =>
        {
            var splitions = x.Split([": ", " "], StringSplitOptions.None).Select(long.Parse);
            return new KeyValuePair<long, long[]>(splitions.First(), splitions.Skip(1).ToArray());
        }).ToDictionary();

    static bool IsAvalidEquation(
        long carlibration, 
        long[] testValues,
        bool isConcated = false)
    {
        List<long> possibilities = [];
        long primary = testValues[0];
        var nexts = testValues[1..];
        possibilities = GeneratePossibilities(primary, nexts, possibilities, isConcated);
        return possibilities.Contains(carlibration);
    }
    static List<long> GeneratePossibilities(
        long primary, 
        long[] nexts, 
        List<long> possibilities, 
        bool isConcated)
    {
        possibilities.Add(primary);
        if (nexts.Length > 0) //break recursive
        {
            if (isConcated)
                GeneratePossibilities(long.Parse($"{primary}{nexts[0]}"), nexts[1..], possibilities,isConcated); //add concernation for each
            GeneratePossibilities(primary + nexts[0], nexts[1..], possibilities, isConcated);
            GeneratePossibilities(primary * nexts[0], nexts[1..], possibilities, isConcated);
        }
        return possibilities;
    }
}
