namespace Advent2025.Day10;

public class Button
{
    public required List<int> ToggleIndexes { get; set; }
}

public class Machine
{
    public required List<bool> IndicatorDiagram { get; set; }
    public required List<Button> Buttons { get; set; }
    public required List<int> JoltageRequirements { get; set; }
}

public class Model
{
    public required List<Machine> Machines { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Machines = new List<Machine>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var machine = new Machine()
            {
                Buttons = new List<Button>(),
                IndicatorDiagram = new List<bool>(),
                JoltageRequirements = new List<int>(),
            };

            var arr = l.Split(" ").ToList();

            foreach (var item in arr)
            {
                if (item.StartsWith("["))
                {
                    var s = item.Trim('[', ']');
                    machine.IndicatorDiagram = s.ToCharArray().ToList().Select(x => x == '#').ToList();
                }

                if (item.StartsWith("("))
                {
                    var s = item.Trim('(', ')');
                    machine.Buttons.Add(new Button()
                    {
                        ToggleIndexes = s.Split(",").Select(x => int.Parse(x)).ToList()
                    });
                }

                if (item.StartsWith("{"))
                {
                    var s = item.Trim('{', '}');
                    machine.JoltageRequirements = s.Split(",").Select(x => int.Parse(x)).ToList();
                }
            }

            retObj.Machines.Add(machine);
        }

        return retObj;
    }
}
