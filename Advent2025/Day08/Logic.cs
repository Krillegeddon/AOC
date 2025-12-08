namespace Advent2025.Day08;


public class Circuit
{
    public List<int> JuncionBoxIndexes { get; set; }
}

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private List<Circuit> _circuits = new List<Circuit>();

    private Dictionary<string, bool> _connectionDict = new Dictionary<string, bool>();

    private Dictionary<int, Circuit> _junctionBoxIndexToCircuit = new Dictionary<int, Circuit>();

    private void ConnectTwoShortest()
    {
        var minDist = double.MaxValue;
        Point p1 = null;
        Point p2 = null;
        var i1 = -1;
        var i2 = -1;
        for (int i = 0; i < _model.JunctionBoxes.Count; i++)
        {
            for (int j = i + 1; j < _model.JunctionBoxes.Count; j++)
            {
                var key = $"{i}_{j}";

                if (_connectionDict.ContainsKey(key))
                {
                    // We have already connected these two guys
                    continue;
                }

                var dist = Point.CalculateDistance(_model.JunctionBoxes[i], _model.JunctionBoxes[j]);
                if (dist < minDist)
                {
                    minDist = dist;
                    p1 = _model.JunctionBoxes[i];
                    p2 = _model.JunctionBoxes[j];
                    i1 = i;
                    i2 = j;
                }
            }
        }

        // See if any of them has a circuit
        Circuit circ1 = null;
        if (_junctionBoxIndexToCircuit.ContainsKey(i1))
        {
            circ1 = _junctionBoxIndexToCircuit[i1];
        }
        Circuit circ2 = null;
        if (_junctionBoxIndexToCircuit.ContainsKey(i2))
        {
            circ2 = _junctionBoxIndexToCircuit[i2];
        }

        if (circ1 == null && circ2 == null)
        {
            // Both are new, create a new circuit
            circ1 = new Circuit { JuncionBoxIndexes = new List<int> { i1, i2 } };
            _junctionBoxIndexToCircuit.Add(i1, circ1);
            _junctionBoxIndexToCircuit.Add(i2, circ1);
            _circuits.Add(circ1);
        }
        else if (circ1 != null && circ2 == null)
        {
            // Only circ1 exists, add i2 to it
            circ1.JuncionBoxIndexes.Add(i2);
            _junctionBoxIndexToCircuit.Add(i2, circ1);
        }
        else if (circ1 == null && circ2 != null)
        {
            // Only circ2 exists, add i1 to it
            circ2.JuncionBoxIndexes.Add(i1);
            _junctionBoxIndexToCircuit.Add(i1, circ2);
        }
        else
        {
            if (circ1 == circ2)
            {
                // Both are the same circuit, nothing to do
                //return;
            }
            else
            {

                // Both exist, merge them
                circ1.JuncionBoxIndexes.AddRange(circ2.JuncionBoxIndexes);
                _circuits.Remove(circ2);
                foreach (var id in (circ1.JuncionBoxIndexes))
                {
                    _junctionBoxIndexToCircuit[id] = circ1;
                }
            }
        }

        // Make a connection
        _connectionDict.Add($"{i1}_{i2}", true);
    }


    public string Run()
    {
        long sum = 0;

        while (_connectionDict.Count < 1000)
        {
            ConnectTwoShortest();
        }

        var circs = _circuits.Select(p=>p.JuncionBoxIndexes.Count).OrderByDescending(p => p).Take(3).ToList();

        sum = circs[0] * circs[1] * circs[2];

        return sum.ToString();
    }
}
