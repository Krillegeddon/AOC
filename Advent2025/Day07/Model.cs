using AdventUtils;

namespace Advent2025.Day07;

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

            var larr = l.ToCharArray();
            for (var x = 0; x < larr.Length; x++)
            {
                retObj.Grid.SetValue(Coord.Create(x, y), larr[x].ToString());
            }
            y++;
        }

        var ss = retObj.Grid.GetAsString();


        return retObj;
    }
}
