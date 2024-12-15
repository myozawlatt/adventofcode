namespace AOC._2024;

[Puzzle(11, 2024, "Plutonian Pebbles")]
internal class Day11 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
        => CountStones(inputLines, 25);
    public object SolvePart2(string[] inputLines)
        => CountStones(inputLines, 75);
    static long CountStones(string[] inputLines, int totalBlinks)
    {
        var stones = inputLines.Combine()
            .Split(' ')
            .Select(long.Parse)
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => (long)x.Count());

        for (int blink = 0; blink < totalBlinks; blink++)
        {
            var newStones = new Dictionary<long, long>();

            foreach (var (stone, count) in stones)
            {
                var str = stone.ToString();
                if (stone == 0)
                    newStones[1] = newStones.GetValueOrDefault(1, 0) + count;
                if (str.Length % 2 == 0)
                {
                    int part = str.Length / 2;
                    long left = long.Parse(str[..part]);
                    long right = long.Parse(str[part..]);

                    newStones[left] = newStones.GetValueOrDefault(left, 0) + count;
                    newStones[right] = newStones.GetValueOrDefault(right, 0) + count;
                }
                else
                {
                    long transformed = stone * 2024;
                    newStones[transformed] = newStones.GetValueOrDefault(transformed, 0) + count;
                }
            }
            stones = newStones;
        }

        return stones.Values.Sum();
    }
}
