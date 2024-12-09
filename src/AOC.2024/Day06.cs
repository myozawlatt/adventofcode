namespace AOC._2024;

[Puzzle(6, 2024, "Guard Gallivant")]
internal class Day06 : IPuzzleSolver
{
    private static readonly Point[] directions =
       [
          new(-1, 0), //up
          new(0, 1), //right
          new(1, 0), //down
          new(0, -1) //left
       ];

    public object SolvePart1(string[] inputLines)
    {
        var (map, guard) = ParseInputs(inputLines);
        var (steps, _) = Move(map, guard);

        return
            steps.Count;
    }
    public object SolvePart2(string[] inputLines)
    {
        var (map, guard) = ParseInputs(inputLines);
        var (steps, _) = Move(map, guard);

        return
            steps.Where(x => Move(map, guard, x).loopFound)
            .Count();
    }
    static (Dictionary<Point, char> map, Point guard) ParseInputs(string[] inputLines)
    {
        var map = inputLines.CreateMap();
        var guard = map.First(x => x.Value == '^').Key;

        return (map, guard);
    }
    static (List<Point> steps, bool loopFound) Move(
        Dictionary<Point, char> map,
        Point step,
        Point newObstruction = default)
    {
        HashSet<(Point step, Point dir)> trips = [];
        bool loopFound = false;

        int dirIndex = 0; // 0 is Up
        Point dir = directions[dirIndex];

        while (true)
        {
            if (!trips.Add((step, dir)))
            {
                loopFound = true;
                break;
            }

            Point next = step.Sum(dir);
            if (!map.TryGetValue(next, out var x))
                break; //guard exit

            if (x == '#' || (next.X == newObstruction.X && next.Y == newObstruction.Y))
            {
                Turn();
                next = step;
            }
            step = next;
        }

        return (trips.Select(x => x.step).Distinct().ToList(), loopFound);

        void Turn()
        {
            dirIndex++;
            if (dirIndex >= directions.Length)
                dirIndex = 0; //if Left is done, set to Up

            dir = directions[dirIndex];
        }
    }
}
