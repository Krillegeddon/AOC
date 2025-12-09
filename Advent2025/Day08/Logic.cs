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
    private Dictionary<int, bool> _junctionBoxIndexNotConnected = new Dictionary<int, bool>();

    private Dictionary<string, double> _distanceCache = new Dictionary<string, double>();
    private List<Tuple<string, double>> _sistanceCacheSorted;

    private int _lastIndex1 = -1;
    private int _lastIndex2 = -1;

    private void ConnectTwoShortest()
    {
        var minDist = double.MaxValue;
        var i1 = -1;
        var i2 = -1;

        foreach (var dist in _sistanceCacheSorted)
        {
            if (_connectionDict.ContainsKey(dist.Item1))
            {
                // We have already connected these two guys
                continue;
            }

            var arr = dist.Item1.Split('_');
            i1 = int.Parse(arr[0]);
            i2 = int.Parse(arr[1]);
            _lastIndex1 = i1;
            _lastIndex2 = i2;
            var p1 = _model.JunctionBoxes[i1];
            var p2 = _model.JunctionBoxes[i2];

            if (p1.X == 216 && p2.X == 117)
            {
                var xxx = 9;
            }
            if (p2.X == 216 && p1.X == 117)
            {
                var xxx = 9;
            }
            break;
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
            _junctionBoxIndexNotConnected.Remove(i1);
            _junctionBoxIndexNotConnected.Remove(i2);
            _circuits.Add(circ1);
        }
        else if (circ1 != null && circ2 == null)
        {
            // Only circ1 exists, add i2 to it
            circ1.JuncionBoxIndexes.Add(i2);
            _junctionBoxIndexToCircuit.Add(i2, circ1);
            _junctionBoxIndexNotConnected.Remove(i2);
        }
        else if (circ1 == null && circ2 != null)
        {
            // Only circ2 exists, add i1 to it
            circ2.JuncionBoxIndexes.Add(i1);
            _junctionBoxIndexToCircuit.Add(i1, circ2);
            _junctionBoxIndexNotConnected.Remove(i1);
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


    private void PopulateDistanceCache()
    {
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
                _distanceCache.Add(key, dist);
            }
        }
    }



    public string Run()
    {
        long sum = 0;

        PopulateDistanceCache();

        _sistanceCacheSorted = _distanceCache.Select(p => new Tuple<string, double>(p.Key, p.Value)).OrderBy(p => p.Item2).ToList();

        for (int i =0; i < _model.JunctionBoxes.Count; i++)
        {
            _junctionBoxIndexNotConnected.Add(i, true);
        }

        var numLimit = 1000;

        if (_distanceCache.Count < 1000)
        {
            numLimit = 10;
        }

        //while (_connectionDict.Count < numLimit) // Part 1
        while (true) // Part 2
        {
            ConnectTwoShortest();

            if (_junctionBoxIndexNotConnected.Count == 0)
            {
                var p1 = _model.JunctionBoxes[_lastIndex1];
                var p2 = _model.JunctionBoxes[_lastIndex2];

                var part2 = p1.X * p2.X;

                break;
            }
        }

        var circs = _circuits.Select(p=>p.JuncionBoxIndexes.Count).OrderByDescending(p => p).Take(3).ToList();

        // Part 1
        sum = circs[0] * circs[1] * circs[2];

        return sum.ToString();
    }
}
