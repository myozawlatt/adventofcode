namespace AOC._2024;

[Puzzle(2, 2024, "Red-Nosed Reports")]
internal class Day2 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
        => ParseReports(inputLines)
            .Count(Rule);   
    public object SolvePart2(string[] inputLines)
        => ParseReports(inputLines)
            .Count(r => MakePossibilities(r).Any(Rule));
    static bool Rule(List<int> report)
    {
        var zip = report.Zip(report.Skip(1));

        return
         zip.All(x => x.First - x.Second >= 1 && x.First - x.Second <= 3) || //decreasing
         zip.All(x => x.Second - x.First >= 1 && x.Second - x.First <= 3);   //increasing
    }
    static List<List<int>> ParseReports(string[] inputLines)
        => inputLines.Select(x => x.Split(" ").Select(int.Parse).ToList())
            .ToList();
    static List<List<int>> MakePossibilities(List<int> report)
    {
        var root = new List<List<int>>();
        for (int i = 0; i < report.Count; i++)
            root.Add(report.Where((_, index) => index != i).ToList());

        return root;
    }
}
