using AOC.Runner;
using Spectre.Console;
using System.Text;
Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;
Console.Title = "Advent of Code 🎄";

start:
Console.Clear();
try
{
    PuzzleRunner.RunPuzzle(11, 2024, InputMode.Actual);

    if (AnsiConsole.Confirm("Run again?"))
        goto start;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]{ex.GetBaseException().Message}[/]");
}