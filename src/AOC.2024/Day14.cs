using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace AOC._2024;

[Puzzle(14, 2024, "Restroom Redoubt")]
internal class Day14 : IPuzzleSolver
{

    //const int WIDTH = 11, HEIGHT = 7;
    const int WIDTH = 101, HEIGHT = 103;

    public object SolvePart1(string[] inputLines)
    {
        const int
           _midWidth = WIDTH / 2,
           _midHeight = HEIGHT / 2;

        var sf = ParseInputs(inputLines)
            .Select(robot =>
            {
                var newX = (robot.p.X + robot.v.X * 100) % WIDTH;
                var newY = (robot.p.Y + robot.v.Y * 100) % HEIGHT;

                newX = newX < 0 ? newX + WIDTH : newX;
                newY = newY < 0 ? newY + HEIGHT : newY;

                return (newX, newY);
            })
            .CountBy(x => x switch
            {
                ( < _midWidth, < _midHeight) => 1,
                ( > _midWidth, < _midHeight) => 2,
                ( < _midWidth, > _midHeight) => 3,
                ( > _midWidth, > _midHeight) => 4,
                _ => 0
            })
            .Where(x => x.Key != 0)
            .Aggregate(1, (x, y) => x * y.Value);

        return sf;
    }

    public object SolvePart2(string[] inputLines)
    {
        var robots = ParseInputs(inputLines);

        var totalSec = WIDTH + HEIGHT;
        HashSet<(int x, int y)> seens;

        for (; ; totalSec++)
        {
            seens = [];
            foreach (var (p, v) in robots)
            {
                var nX = (p.X + v.X * totalSec) % WIDTH;
                var nY = (p.Y + v.Y * totalSec) % HEIGHT;

                nX = nX < 0 ? nX + WIDTH : nX;
                nY = nY < 0 ? nY + HEIGHT : nY;

                seens.Add((nX, nY));
                if (seens.Count == robots.Length)
                    goto exit;
            }
        }

    exit:

        DrawChristmasTree(seens);
        return totalSec;
    }

    static void DrawChristmasTree(HashSet<(int x, int y)> robots)
    {
        StringBuilder sb = new();
        for (var x = 0; x < WIDTH; x++)
        {
            for (var y = 0; y < HEIGHT; y++)
                sb.Append(robots.Contains((x, y)) ? "#" : ".");
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
