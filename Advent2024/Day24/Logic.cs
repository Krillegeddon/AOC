namespace Advent2024.Day24;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private bool RunOne()
    {
        foreach (var op in _model.Ops)
        {
            // If wire1 and wire2 has value, but outputWire doesn't, then compute the output wire
            if (_model.Wires[op.Wire1].HasValue && _model.Wires[op.Wire2].HasValue && !_model.Wires[op.OutputWire].HasValue)
            {
                var wire1Value = _model.Wires[op.Wire1].Value;
                var wire2Value = _model.Wires[op.Wire2].Value;

                bool res;
                switch (op.Operation)
                {
                    case ("AND"):
                        res = wire1Value && wire2Value;
                        break;
                    case ("OR"):
                        res = wire1Value || wire2Value;
                        break;
                    case ("XOR"):
                        res = wire1Value ^ wire2Value;
                        break;
                    default:
                        throw new Exception("Unknown operation" + op.Operation);
                }

                _model.Wires[op.OutputWire] = res;

                return true;
            }
        }

        return false;
    }


    public string Run()
    {
        long sum = 0;

        while (RunOne())
        {
        }

        long bitVal = 1;
        var zWires = _model.Wires.Where(p => p.Key.StartsWith("z")).OrderBy(p => p.Key).ToList();
        foreach (var wire in zWires)
        {
            var num = int.Parse(wire.Key.Replace("z", ""));
            if (wire.Value.Value)
            {
                sum += bitVal;
            }
            bitVal = bitVal * 2;
        }

        return sum.ToString();
    }
}
