namespace AOC.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class PuzzleAttribute(int Day, int Year, string Name) : Attribute
{
    public int Day { get; } = Day;
    public int Year { get; } = Year;
    public string Name { get; } = Name;
}
