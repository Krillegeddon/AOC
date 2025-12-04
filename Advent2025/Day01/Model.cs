namespace Advent2025.Day01;

public class Model
{
    public class Turn
    {
        public char Direction { get; set; }
        public int Distance { get; set; }
    }

    public required List<Turn> Turns { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Turns = new List<Turn>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var turn = "L";
            if (l.StartsWith("R"))
                turn = "R";
            var distance = int.Parse(l[1..]);

            retObj.Turns.Add(new Turn
            {
                Direction = turn[0],
                Distance = distance
            });
        }

        return retObj;
    }
}
