namespace AOC._2024;

[Puzzle(2, 2024, "Red-Nosed Reports")]
internal class Day02 : IPuzzleSolver
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

    record Pair(int First, int Second);
    static int SimpleSolvingPart1(List<List<int>> reports)
    {
        int safeReports = 0;
        foreach (var report in reports)
        {
            var pairs = new List<Pair>();
            for (int i = 0; i < report.Count; i++)
            {
                if (i < report.Count - 1)
                    pairs.Add(new(report[i], report[i + 1]));
            }
            if (ValidateRule(pairs))
                safeReports++;
        }
        return safeReports;
    }
    static bool ValidateRule(List<Pair> pairs)
    {
        bool
            increasing = false,
            decreasing = false;

        //increasing
        foreach (var pair in pairs)
        {
            increasing = pair.Second - pair.First >= 1 && pair.Second - pair.First <= 3;
            if (!increasing)
                break;
        }
        //decreasing
        foreach (var pair in pairs)
        {
            decreasing = pair.First - pair.Second >= 1 && pair.First - pair.Second <= 3;
            if (!decreasing)
                break;
        }

        return increasing || decreasing;
    }
    static int SimpleSovlingPart2(List<List<int>> reports)
    {
        int safeReports = 0;

        foreach (var report in reports)
        {
            var pairs = new List<Pair>();
            for (int i = 0; i < report.Count; i++)
            {
                if (i < report.Count - 1)
                    pairs.Add(new(report[i], report[i + 1]));
            }
            if (ValidateRule(pairs))
                safeReports++;
            else
            {
                var possiblilites = MakePossibilities(report);
                var possibleSafeReports = SimpleSolvingPart1(possiblilites);
                if (possibleSafeReports > 0)
                    safeReports++;
            }
        }

        return safeReports;
    }
}
