using AOC.Runner;
using Spectre.Console;

try
{
    PuzzleRunner.RunPuzzle(8, 2024, InputMode.Actual);
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]{ex.GetBaseException().Message}[/]");
}

Console.WriteLine("Press any key to exit..");
Console.ReadKey();