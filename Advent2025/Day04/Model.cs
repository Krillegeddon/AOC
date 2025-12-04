using AdventUtils;

namespace Advent2025.Day04;

public class Grid : GridBase<string>
{
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

            var arr = l.ToCharArray();
            for (int x = 0; x < arr.Length; x++)
            {
                retObj.Grid.SetValue(Coord.Create(x, y), arr[x].ToString());
            }

            y++;
        }

        var debug = retObj.Grid.GetAsString();

        return retObj;
    }
}
