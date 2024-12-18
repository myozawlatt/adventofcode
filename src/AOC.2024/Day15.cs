namespace AOC._2024;

using static AOC.Common.Directions;
using Map = Dictionary<Point, char>;

[Puzzle(15, 2024, "Warehouse Woes")]
internal class Day15 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
        => FindCoordinates(inputLines, x => x == '.');

    public object SolvePart2(string[] inputLines)
        => FindCoordinates(Upgrade(inputLines));

    static string[] Upgrade(string[] inputLines)
        => inputLines.Select(x => x.Replace("#", "##")
            .Replace(".", "..")
            .Replace("O", "[]")
            .Replace("@", "@."))
            .ToArray();

    static int FindCoordinates(string[] inputLines, Func<char, bool> breaker = null!)
    {
        var (map, moves) = ParseInputs(inputLines);

        var robot = map.Keys.Single(x => map[x] == '@');
        foreach (var move in moves)
        {
            List<Point> boxes = [robot];
            bool isWall = false;

            for (int i = 0; i < boxes.Count; i++)
            {
                var next = boxes[i].Sum(move);
                if (map[next] == '#')
                {
                    isWall = true;
                    break;
                }
                if (breaker?.Invoke(map[next]) == true)
                    break;

                if ("[]O".Contains(map[next]) && !boxes.Contains(next))
                {
                    if (map[next] == 'O')
                        boxes.Add(next);

                    if (map[next] == ']')
                    {
                        boxes.Add(next);
                        boxes.Add(next.Sum(Left));
                    }
                    if (map[next] == '[')
                    {
                        boxes.Add(next);
                        boxes.Add(next.Sum(Right));
                    }
                }
            }

            if (isWall)
                continue;

            var mapCopy = map.ToDictionary();

            boxes.RemoveAt(0);
            foreach (var box in boxes)
                map[box] = '.';
            foreach (var box in boxes)
                map[box.Sum(move)] = mapCopy[box];

            map[robot] = '.';
            map[robot.Sum(move)] = '@';
            robot = robot.Sum(move);
        }

        return
            map.Keys
            .Where(x => "[O".Contains(map[x]))
            .Sum(x => x.X * 100 + x.Y);
    }

    static (Map, Point[]) ParseInputs(string[] inputLines)
    {
        var emptyLine = Array.IndexOf(inputLines, string.Empty);

        var map = inputLines.Take(emptyLine)
             .ToArray()
             .CreateMap();

        var moves = inputLines.Skip(emptyLine + 1)
            .SelectMany(line =>
                line.Select(c => c switch
                {
                    '^' => Up,
                    '>' => Right,
                    'v' => Down,
                    '<' => Left,
                    _ => throw new Exception("Invalid move.")
                }))
            .ToArray();

        return (map, moves);
    }
}
