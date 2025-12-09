using AdventUtils;

namespace Advent2025.Day09;

public class Model
{
    public required List<Coord> TileCoordinates { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            TileCoordinates = new List<Coord>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var arr = l.Split(',');
            var coord = Coord.Create(long.Parse(arr[0]), long.Parse(arr[1]));
            retObj.TileCoordinates.Add(coord);
        }

        return retObj;
    }
}
