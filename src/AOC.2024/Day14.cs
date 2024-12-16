using System.Text;

namespace AOC._2024;

[Puzzle(14, 2024, "Restroom Redoubt")]
internal class Day14 : IPuzzleSolver
{
    const int WIDTH = 101, HEIGHT = 103;
    public object SolvePart1(string[] inputLines)
    {
        var robots = ParseInputs(inputLines)
            .Select(robot =>
            {
                var nX = (robot.p.X + robot.v.X * 100) % WIDTH;
                var nY = (robot.p.Y + robot.v.Y * 100) % HEIGHT;

                nX = nX < 0 ? nX + WIDTH : nX;
                nY = nY < 0 ? nY + HEIGHT : nY;

                return new Point(nX, nY);
            });

        int
            midWidth = WIDTH / 2,
            midHeight = HEIGHT / 2,

            q1 = robots.Count(x => x.X < midWidth && x.Y < midHeight),
            q2 = robots.Count(x => x.X > midWidth && x.Y < midHeight),
            q3 = robots.Count(x => x.X < midWidth && x.Y > midHeight),
            q4 = robots.Count(x => x.X > midWidth && x.Y > midHeight);

        return q1 * q2 * q3 * q4;
    }

    public object SolvePart2(string[] inputLines)
    {
        var robots = ParseInputs(inputLines);
        var total = robots.Length;
        var totalSeconds = 0;
        HashSet<Point> seens = [];

        for (var i = 1; i < 100000; i++)
        {
            seens = [];
            foreach (var (p, v) in robots)
            {
                var nX = (p.X + v.X * i) % WIDTH;
                var nY = (p.Y + v.Y * i) % HEIGHT;

                nX = nX < 0 ? nX + WIDTH : nX;
                nY = nY < 0 ? nY + HEIGHT : nY;

                seens.Add(new Point(nX, nY));
                if (seens.Count == total)
                {
                    totalSeconds = i;
                    goto exit;
                }
            }
        }

    exit:

        DrawChristmasTree(seens);
        return totalSeconds;
    }

    static void DrawChristmasTree(HashSet<Point> robots)
    {
        StringBuilder sb = new();
        for (var x = 0; x < WIDTH; x++)
        {
            for (var y = 0; y < HEIGHT; y++)
                sb.Append(robots.Contains(new Point(x, y)) ? "#" : ".");
            sb.AppendLine();
        }

        var vtzFolder = $"{FilePaths.Virtualizations}/2024";
        if (!Directory.Exists(vtzFolder))
            Directory.CreateDirectory(vtzFolder);
        File.WriteAllText($"{vtzFolder}/day14.txt", sb.ToString());
    }

    static (Point p, Point v)[] ParseInputs(string[] inputLines)
        => inputLines.Select(x =>
        {
            var matches = Regex.Matches(x, @"-?\d+").Select(x => int.Parse(x.Value)).ToArray();
            var p = new Point(matches[0], matches[1]);
            var v = new Point(matches[2], matches[3]);
            return (p, v);
        }).ToArray();
}
