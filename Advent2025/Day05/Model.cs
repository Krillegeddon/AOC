namespace Advent2025.Day05;

public class Range
{
    public required long Start { get; set; }
    public required long End { get; set; }
}

public class Model
{
    public required List<Range> Ranges { get; set; }
    public required List<long> Ingredients { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Ranges = new List<Range>(),
            Ingredients = new List<long>(),
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            if (l.Contains('-'))
            {
                var parts = l.Split('-');
                retObj.Ranges.Add(new Range
                {
                    Start = long.Parse(parts[0]),
                    End = long.Parse(parts[1]),
                });
            }
            else
            {
                retObj.Ingredients.Add(long.Parse(l));
            }

        }

        return retObj;
    }
}
