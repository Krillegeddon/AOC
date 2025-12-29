using AdventUtils;

namespace Advent2025.Day09;

public class Line
{
    public required Coord C1 { get; set; }
    public required Coord C2 { get; set; }

    public int DeltaXForInside {get; set; }
    public int DeltaYForInside { get; set; }
}

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private Dictionary<string, double> _areaCache = new Dictionary<string, double>();

    private void PopulateAreaCache()
    {
        for (int i = 0; i < _model.TileCoordinates.Count; i++)
        {
            for (int j = i + 1; j < _model.TileCoordinates.Count; j++)
            {
                var key = $"{i}_{j}";

                var xdist = Math.Abs(_model.TileCoordinates[j].X - _model.TileCoordinates[i].X) + 1;
                var ydist = Math.Abs(_model.TileCoordinates[j].Y - _model.TileCoordinates[i].Y) + 1;

                if (i == 1 && j == 5)
                {
                    int bb = 9;
                }

                var area = xdist * ydist;
                _areaCache.Add(key, area);
            }
        }
    }



    public string Run()
    {
        PopulateAreaCache();

        var areaCacheSorted = _areaCache.Select(p => new Tuple<string, double>(p.Key, p.Value)).OrderByDescending(p => p.Item2).ToList();

        var f = areaCacheSorted.First().Item2;
        return f.ToString();
    }
}
