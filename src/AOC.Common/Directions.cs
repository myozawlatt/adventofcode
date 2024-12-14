namespace AOC.Common;
public class Directions
{
    public static readonly Point Up = new(-1, 0); //up
    public static readonly Point Right = new(0, 1); //right
    public static readonly Point Down = new(1, 0); //down
    public static readonly Point Left = new(0, -1); //left

    public static IEnumerable<Point> All
    {
        get
        {
            yield return Up;
            yield return Right;
            yield return Down;
            yield return Left;
        }
    }
    public static Point Turn(int dir)
    {
        return dir switch
        {
            1 => Right,
            2 => Down,
            3 => Left,
            4 => Up,
            _ => new(0, 0)
        };
    }
    public static Point Turn(Point dir)
    {
        return dir switch
        {
            { X: -1, Y: 0 } => Right,
            { X: 0, Y: 1 } => Down,
            { X: 1, Y: 0 } => Left,
            { X: 0, Y: -1 } => Up,
            _ => new(0, 0)
        };
    }
}
