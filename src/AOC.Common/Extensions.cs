using System.Drawing;

namespace AOC.Common;
public static class Extensions
{
    public static Dictionary<Point,char> CreateMap(this string[] lines)
    {
        Dictionary<Point, char> map = [];
        for (int i = 0; i < lines.Length; i++)
            for (int j = 0; j < lines[i].Length; j++)
                map.Add(new(i, j), lines[i][j]);

        return map;
    }
    public static Point Add(this Point a, Point b)
        => new(a.X + b.X, a.Y + b.Y);
    public static Point Diff(this Point a, Point b)
        => new(a.X - b.X, a.Y - b.Y);
}
