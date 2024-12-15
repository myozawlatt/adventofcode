namespace AOC._2024;

using System.Collections.Generic;
using Map = Dictionary<Point, char>;

[Puzzle(12, 2024, "Garden Groups")]
internal class Day12 : IPuzzleSolver
{

    private static readonly List<(Point, Point)> Corners =
        [
            (Directions.Up, Directions.Right),
            (Directions.Right, Directions.Down),
            (Directions.Down, Directions.Left),
            (Directions.Left, Directions.Up)
        ];

    public object SolvePart1(string[] inputLines)
    {
        var map = inputLines.CreateMap();

        return
            CollectPlantRegions(map)
            .Select(x => CalculateFencePriceByEdges(x.Regions))
            .Sum();
    }

    public object SolvePart2(string[] inputLines)
    {
        var map = inputLines.CreateMap();

        return
            CollectPlantRegions(map)
            .Select(x => CalculateFencePriceByCorners(x, map))
            .Sum();
    }

    static long CalculateFencePriceByEdges(HashSet<Point> regions)
    {
        var edges = regions.SelectMany(x => Directions.All.Select(dir => dir.Sum(x)))
                 .Where(x => !regions.Contains(x))
                 .Count();

        return edges * regions.Count;
    }

    static long CalculateFencePriceByCorners(PlantRegion plantRegion, Map map)
    {
        int totalCorners = 0;

        var (plant,regions) = plantRegion;
        foreach (var region in regions)
        {
            foreach (var (dir1, dir2) in Corners)
            {
                //convex
                if (map.GetValueOrDefault(region.Sum(dir1)) != plant &&
                    map.GetValueOrDefault(region.Sum(dir2)) != plant)
                    totalCorners++;

                //concave
                if (map.GetValueOrDefault(region.Sum(dir1)) == plant &&
                    map.GetValueOrDefault(region.Sum(dir2)) == plant &&
                    map.GetValueOrDefault(region.Sum(dir1).Sum(dir2)) != plant)
                    totalCorners++;
            }
        }

        return totalCorners * regions.Count;
    }

    private static List<PlantRegion> CollectPlantRegions(Map map)
    {
        List<PlantRegion> regionCollection = [];

        HashSet<Point> seens = [];
        foreach (var (pos, plant) in map)
        {
            if (!seens.Add(pos))
                continue;

            HashSet<Point> regions = [];
            CollectRegions(map, plant, pos, seens, regions);
            regionCollection.Add(new(plant, regions));
        }

        return regionCollection;
    }

    static void CollectRegions(Map map,
        char plant,
        Point position,
        HashSet<Point> seens,
        HashSet<Point> regions)
    {
        if (!regions.Add(position))
            return;

        foreach (var dir in Directions.All)
        {
            var next = position.Sum(Directions.Turn(dir));
            if (map.TryGetValue(next, out var value) && value == plant)
            {
                seens.Add(next);
                CollectRegions(map, plant, next, seens, regions);
            }
        }
    }
    record PlantRegion(char Plant, HashSet<Point> Regions);

}
