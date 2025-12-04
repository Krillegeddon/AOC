namespace Advent2025.Day02;

public class Model
{
    public class Interval
    {
        public long Start { get; set; }
        public long End { get; set; }
    }

    public required List<Interval> Intervals { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Intervals = new List<Interval>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var parts = l.Split(",");
            foreach (var part in parts)
            {
                var subParts = part.Split("-");
                var interval = new Interval
                {
                    Start = long.Parse(subParts[0]),
                    End = long.Parse(subParts[1])
                };
                retObj.Intervals.Add(interval);
            }

        }

        return retObj;
    }
}
