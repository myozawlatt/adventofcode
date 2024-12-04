namespace AOC._2024;

[Puzzle(4, 2024, "Ceres Search")]
internal class Day04 : IPuzzleSolver
{
    static string target = "XMAS";
    public object SolvePart1(string[] inputLines)
        => HorizontalSearch(inputLines)
           + HorizontalSearch(TransposeVertical(inputLines))
           + DiagonalSearch(inputLines);
    public object SolvePart2(string[] inputLines)
        => CrossSearch(inputLines);
    static int HorizontalSearch(string[] inputLines)
        => inputLines.Sum(x => Regex.Matches(x, "(?=(XMAS))|(?=(SAMX))").Count);
    static string[] TransposeVertical(string[] inputLines)
        => Enumerable.Range(0, inputLines[0].Length)
                .Select(x => new string(inputLines.Select(l => l[x]).ToArray()))
                .ToArray();
    static int DiagonalSearch(string[] inputLines)
    {
        int found = 0;
        for (int row = 0; row < inputLines.Length; row++)
        {
            var current = inputLines[row];

            //forward
            for (int col = 0; col < current.Length; col++)
            {
                List<char> chars = [];
                chars.Add(current[col]);

                int next = col + 1;
                for (int i = 1; i < target.Length; i++)
                {
                    if (next < current.Length && (row + target.Length) <= inputLines.Length)
                        chars.Add(inputLines[row + i][next++]);
                    else
                        break;
                }

                if (Found(chars, target))
                    found++;
            }

            //backward
            for (int col = current.Length - 1; col >= 0; col--)
            {
                List<char> chars = [];
                chars.Add(current[col]);

                int next = col - 1;
                for (int i = 1; i < target.Length; i++)
                {
                    if (next >= 0 && (row + target.Length) <= inputLines.Length)
                        chars.Add(inputLines[row + i][next--]);
                    else
                        break;
                }
                if (Found(chars, target))
                    found++;
            }
        }
        return found;
    }
    static bool Found(IEnumerable<char> chars, string rule)
        => Enumerable.SequenceEqual(chars, rule)
        || Enumerable.SequenceEqual(chars, rule.Reverse());
    static int CrossSearch(string[] inputLines)
    {
        target = "MAS";
        int found = 0;
        for (int row = 0; row < inputLines.Length; row++)
        {
            var current = inputLines[row];
            for (int col = 0; col < current.Length; col++)
            {
                char a = current[col];
                if (a != 'A')
                    continue;

                if (row - 1 >= 0 && row + 1 < inputLines.Length && col - 1 >= 0 && col + 1 < current.Length)
                {
                    char upperLeft = inputLines[row - 1][col - 1];
                    char upperRight = inputLines[row - 1][col + 1];
                    char lowerLeft = inputLines[row + 1][col - 1];
                    char lowerRight = inputLines[row + 1][col + 1];

                    if (Found([upperLeft, a, lowerRight], target) && Found([upperRight, a, lowerLeft], target))
                        found++;
                }
            }
        }

        return found;
    }
}
