namespace AOC._2024;

[Puzzle(13, 2024, "Claw Contraption")]
internal class Day13 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
        => ParseInputs(inputLines).Sum(CalculatePrize);

    public object SolvePart2(string[] inputLines)
        => ParseInputs(inputLines, 10000000000000).Sum(CalculatePrize);

    long CalculatePrize(ButtonConfig config)
    {
        var (A, B, Price) = config;

        //Linear algebra - Cramer's Rule
        var determinant = A.X * B.Y - A.Y * B.X;
        var aPress = (Price.X * B.Y - Price.Y * B.X) / determinant;
        var bPress = (Price.Y * A.X - Price.X * A.Y) / determinant;

        if ((A.X * aPress + B.X * bPress, A.Y * aPress + B.Y * bPress) == (Price.X, Price.Y))
            return aPress * 3 + bPress;
        else
            return 0;
    }

    static IEnumerable<ButtonConfig> ParseInputs(string[] inputLines, long offset = 0)
    {
        var chunks = inputLines.Where(x => x != string.Empty).Chunk(3).ToList();
        foreach (var chunk in chunks)
        {
            var configs = chunk.Order()
                .Select(x =>
                 {
                     var matches = Regex.Matches(x, @"\d+");
                     return new Point(long.Parse(matches[0].Value), long.Parse(matches[1].Value));
                 })
                .ToArray();

            yield return new ButtonConfig(configs[0], configs[1], configs[2].ShiftOffset(offset));
        }
    }

    record ButtonConfig(Point A, Point B, Point Price);

    record Point(long X, long Y)
    {
        public Point ShiftOffset(long offset) => new(X + offset, Y + offset);
    };
}
