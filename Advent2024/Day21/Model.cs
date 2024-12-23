namespace Advent2024.Day21;

public class Model
{
    public required List<string> Codes { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Codes = new List<string>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            retObj.Codes.Add(l);
        }

        return retObj;
    }
}
