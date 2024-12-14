namespace AOC._2024;
using Map = Dictionary<Point, int>;

[Puzzle(10, 2024, "Hoof It")]
internal class Day10 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
        => FindTrailHeads<HashSet<Point>>(inputLines);
    public object SolvePart2(string[] inputLines)
        => FindTrailHeads<List<Point>>(inputLines);
    public static int FindTrailHeads<T>(string[] inputLines)
        where T : ICollection<Point>,new()
    {
        var map = inputLines.CreateMap<int>();
        int total = 0;
        foreach (var (pos, head) in map)
        {
            if (head != 0)
                continue;
            var scores = new T();
            FindTrails(map, pos, scores);
            total += scores.Count;
        }

        return total;
    }
    static void FindTrails(Map map,
        Point position,
        ICollection<Point> scores)
    {
        if (!map.TryGetValue(position, out int height))
            return;

        if (height == 9)
            scores.Add(position);

        foreach (var dir in Directions.All)
        {
            var next = position.Sum(Directions.Turn(dir));
            if (map.TryGetValue(next, out int value) && value == map[position] + 1)
                FindTrails(map, next, scores);
        }
    }    
}
