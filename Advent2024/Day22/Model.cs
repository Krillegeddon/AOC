namespace Advent2024.Day22;

public class Model
{
    public required List<long> Numbers { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Numbers = new List<long>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            retObj.Numbers.Add(long.Parse(l));
        }

        return retObj;
    }
}
