namespace Advent2024.Day24;

public class Op
{
    public string Wire1 { get; set; }
    public string Wire2 { get; set; }
    public string Operation { get; set; }
    public string OutputWire { get; set; }
}

public class Model
{
    public required Dictionary<string, bool?> Wires { get; set; }
    public List<Op> Ops { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Wires = new Dictionary<string, bool?>(),
            Ops = new List<Op>()
        };

        var lookingForWires = true;
        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
            {
                lookingForWires = false;
                continue;
            }

            if (lookingForWires)
            {
                var arr = l.Split(": ");
                retObj.Wires.Add(arr[0], arr[1] == "1");
            }
            else
            {
                var arr1 = l.Split(" -> ");
                var arr2 = arr1[0].Split(" ");

                var op = new Op()
                {
                    Wire1 = arr2[0],
                    Wire2 = arr2[2],
                    Operation = arr2[1],
                    OutputWire = arr1[1]
                };

                if (!retObj.Wires.ContainsKey(op.Wire1))
                    retObj.Wires.Add(op.Wire1, null);
                if (!retObj.Wires.ContainsKey(op.Wire2))
                    retObj.Wires.Add(op.Wire2, null);
                if (!retObj.Wires.ContainsKey(op.OutputWire))
                    retObj.Wires.Add(op.OutputWire, null);

                retObj.Ops.Add(op);
            }

        }

        return retObj;
    }
}
