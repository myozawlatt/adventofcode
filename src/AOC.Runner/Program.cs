using AOC.Runner;
using Spectre.Console;
using System.Text;
using static AOC.Runner.InputMode;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
Console.Title = "Advent of Code 🎄";

start:
HardCleanConsole();
try
{
    PuzzleRunner.RunPuzzle(15, 2024, Actual);

    if (AnsiConsole.Confirm("Run again?"))
        goto start;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]{ex.GetBaseException().Message}[/]");
}

static void HardCleanConsole()
{
    Console.Clear();
    Console.WriteLine("\x1b[3J");
}