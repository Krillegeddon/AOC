namespace Advent2024.Day19;

public class Model
{
    public List<string> Towels { get; set; }
    public List<string> Designs { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Towels = new List<string>(),
            Designs = new List<string>()
        };

        var isReadingDesigns = false;
        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
            {
                isReadingDesigns = true;
                continue;
            }

            if (!isReadingDesigns)
            {
                retObj.Towels = l.Split(',').Select(p=>p.Trim()).ToList();
            }

            if (isReadingDesigns)
            {
                retObj.Designs.Add(l);
            }
        }

        return retObj;
    }
}
