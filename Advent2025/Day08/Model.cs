namespace Advent2025.Day08;

public class Model
{
    public required List<object> Obj { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Obj = new List<object>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;
        }

        return retObj;
    }
}
