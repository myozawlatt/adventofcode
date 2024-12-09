using System.Drawing;

namespace AOC._2024;

[Puzzle(8, 2024, "Resonant Collinearity")]
internal class Day08 : IPuzzleSolver
{
    public object SolvePart1(string[] inputLines)
    {
        var (antennaLocationGroups, map) = ParseInputs(inputLines);

        HashSet<Point> antinodes = [];
        foreach (var antennaLocations in antennaLocationGroups)
        {
            for (var i = 0; i < antennaLocations.Length; i++)
            {
                var current = antennaLocations[i];
                foreach (var next in antennaLocations.Skip(i + 1))
                {
                    var distance = next.Diff(current);
                    antinodes.Add(current.Diff(distance));
                    antinodes.Add(next.Add(distance));
                }
            }
        }
        return
            antinodes.Where(map.ContainsKey)
            .Count();
    }
    public object SolvePart2(string[] inputLines)
    {
        var (antennaLocationGroups, map) = ParseInputs(inputLines);

        HashSet<Point> antinodes = [];
        foreach (var antennaLocations in antennaLocationGroups)
        {
            for (var i = 0; i < antennaLocations.Length; i++)
            {
                var current = antennaLocations[i];
                foreach (var next in antennaLocations.Skip(i + 1))
                {
                    //forward
                    var antinode = current;
                    var distance = next.Diff(current);
                    while (map.ContainsKey(antinode))
                    {
                        antinodes.Add(antinode);
                        antinode = antinode.Add(distance);
                    }

                    //backword
                    antinode = current;
                    distance = current.Diff(next);
                    while (map.ContainsKey(antinode))
                    {
                        antinodes.Add(antinode);
                        antinode = antinode.Add(distance);
                    }
                }
            }
        }

        return
            antinodes.Count;
    }

    static (List<Point[]> antennaLocations, Dictionary<Point, char> map) ParseInputs(string[] inputLines)
    {
        var map = inputLines.CreateMap();

        var antennaGroups = map
            .Where(x => char.IsAsciiLetterOrDigit(x.Value))
            .GroupBy(x => x.Value)
            .Select(x => x.Select(x => x.Key).ToArray())
            .ToList();

        return (antennaGroups, map);
    }
}
