using System.Text;

namespace AOC.Common;
public static class Extensions
{
    public static Dictionary<Point, char> CreateMap(this string[] lines)
    {
        Dictionary<Point, char> map = [];
        for (int i = 0; i < lines.Length; i++)
            for (int j = 0; j < lines[i].Length; j++)
                map.Add(new(i, j), lines[i][j]);

        return map;
    }
    public static Dictionary<Point, T> CreateMap<T>(this string[] lines)
    {
        Dictionary<Point, T> map = [];
        for (int i = 0; i < lines.Length; i++)
            for (int j = 0; j < lines[i].Length; j++)
                map.Add(new(i, j), (T)Convert.ChangeType(char.GetNumericValue(lines[i][j]), typeof(T)));

        return map;
    }
    public static string Combine(this string[] lines)
        => string.Join("", lines);
    public static Span<byte> AsByteSpan(this string[] lines)
        => Encoding.UTF8.GetBytes(lines.Combine()).AsSpan();
    public static Point Sum(this Point a, Point b)
        => new(a.X + b.X, a.Y + b.Y);
    public static Point Diff(this Point a, Point b)
        => new(a.X - b.X, a.Y - b.Y);
}
