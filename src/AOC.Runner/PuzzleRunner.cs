using Spectre.Console;
using System.Diagnostics;
using System.Reflection;

namespace AOC.Runner;
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

    static readonly string InputPathNotFoundError =
        """
            Input file does not found.
            Please create your input like this:
            inputs/
            ├── {year}/
            │   ├── {day}.txt
            """;
    public static void RunPuzzle(int day, int year)
    {
        var puzzle = puzzles24.FirstOrDefault(x => x.Day == day && x.Year == year)
            ?? throw new Exception("Puzzle does not found.");

        var inputPath = @$"inputs\{puzzle.Year}\day{puzzle.Day}.txt";
        if (!File.Exists(inputPath))
            throw new Exception(InputPathNotFoundError);

        var inputs = File.ReadAllLines(inputPath);

        var sw = Stopwatch.StartNew();
        var part1 = puzzle.Solver.SolvePart1(inputs);
        sw.Stop();
        var part1Elapsed = sw.Elapsed;

        sw.Restart();
        var part2 = puzzle.Solver.SolvePart2(inputs);
        sw.Stop();
        var part2Elapsed = sw.Elapsed;

        AnsiConsole.MarkupLine($"[bold aquamarine1]{puzzle.Year}[/], [bold aquamarine1]Day {puzzle.Day}[/] : [bold olive]{puzzle.Name}[/] \n");
        AnsiConsole.MarkupLine($"Part 1: {part1}    [green]({part1Elapsed.TotalMilliseconds} ms)[/]");
        AnsiConsole.MarkupLine($"Part 2: {part2}    [green]({part2Elapsed.TotalMilliseconds} ms)[/] \n");
    }
}
