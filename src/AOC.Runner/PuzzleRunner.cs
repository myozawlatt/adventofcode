using Spectre.Console;
using System.Diagnostics;
using System.Reflection;

namespace AOC.Runner;
internal enum InputMode
{
    Sample,
    Actual
}
public class PuzzleRunner
{
    record PuzzleInfo(int Day, int Year, string Name, IPuzzleSolver Solver);

    private static readonly List<PuzzleInfo> puzzles24 =
        Assembly.GetAssembly(typeof(_2024.Day01))!
        .GetTypes()
        .Select(x => new
        {
            PuzzleType = x,
            Attr = x.GetCustomAttribute<PuzzleAttribute>()!
        })
        .Where(x => x.Attr != null)
        .Select(x => new PuzzleInfo(
            x.Attr.Day,
            x.Attr.Year,
            x.Attr.Name,
            (IPuzzleSolver)Activator.CreateInstance(x.PuzzleType)!))
        .ToList();
    internal static void RunPuzzle(int day, int year, InputMode mode = InputMode.Actual)
    {
        var puzzle = puzzles24.FirstOrDefault(x => x.Day == day && x.Year == year)
            ?? throw new Exception("Puzzle does not found.");

        string yearFolder = @$"inputs\{puzzle.Year}";
        if (!Directory.Exists(yearFolder))
            Directory.CreateDirectory(yearFolder);

        var inputFileName = $"day{puzzle.Day:00}";
        if (mode == InputMode.Sample)
            inputFileName += ".sample";

        var inputPath = @$"{yearFolder}\{inputFileName}.txt";
        if (!File.Exists(inputPath))
            File.WriteAllText(inputPath, string.Empty);

        PrintTitle();
        var inputs = File.ReadAllLines(inputPath);

        object part1 = null!, part2 = null!;
        TimeSpan part1Elapsed = TimeSpan.Zero, part2Elapsed = TimeSpan.Zero;

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Christmas)
            .SpinnerStyle(Style.Parse("bold cornflowerblue"))
            .Start("Hold up, I'm solving..", ctx =>
            {
                var sw = Stopwatch.StartNew();
                part1 = puzzle.Solver.SolvePart1(inputs);
                sw.Stop();
                part1Elapsed = sw.Elapsed;

                sw.Restart();
                part2 = puzzle.Solver.SolvePart2(inputs);
                sw.Stop();
                part2Elapsed = sw.Elapsed;

            });

        AnsiConsole.MarkupLine($"Part 1: {part1}    [green]({part1Elapsed.TotalMilliseconds} ms)[/]");
        AnsiConsole.MarkupLine($"Part 2: {part2}    [green]({part2Elapsed.TotalMilliseconds} ms)[/] \n");

        void PrintTitle()
            => AnsiConsole.MarkupLine($"[bold aquamarine1]{puzzle.Year}[/], [bold aquamarine1]Day {puzzle.Day}[/] : [bold olive]{puzzle.Name}[/] \n");
    }
}
