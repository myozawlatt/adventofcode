using AOC.Runner;
using Spectre.Console;

try
{
    PuzzleRunner.RunPuzzle(3, 2024);
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]{ex.GetBaseException().Message}[/]");
}

Console.WriteLine("Press any key to exit..");
Console.ReadKey();