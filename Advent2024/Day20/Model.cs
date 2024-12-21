using AdventUtils;

namespace Advent2024.Day20;

public class Grid : GridBase<string>
{
    public Coord StartPosition { get; set; }
    public Coord EndPosition { get; set; }
}

public class Model
{
    public required Grid Grid { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Grid = new Grid()
        };

        int y = 0;
        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var row = l.ToCharArray();
            for (int x = 0; x < row.Length; x++)
            {
                var c = row[x].ToString();
                var coord = Coord.Create(x, y);
                if (c == "S")
                {
                    retObj.Grid.StartPosition = coord;
                    c = ".";
                }
                if (c == "E")
                {
                    retObj.Grid.EndPosition = coord;
                    c = ".";
                }
                retObj.Grid.SetValue(coord, c);
            }

            y++;
        }



        return retObj;
    }
}
