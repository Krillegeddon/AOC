using AdventUtils;
using System.Diagnostics;

namespace Advent2025.Day07;

public class NumGrid : GridBase<long>
{
}

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private long numSplits = 0;

    private void TraverseDown(int x, int y)
    {
        //var sss = _model.Grid.GetAsString();

        var coord = Coord.Create(x, y);

        // Check if someone else already traversed this coordinate.
        if (_model.Grid.GetValue(coord) == "|")
        {
            return;
        }

        // Check if we are out of bounds
        if (!_model.Grid.IsInside(coord))
        {
            //numSplits++;
            return;
        }

        // Check if to just continue downwards
        if (_model.Grid.GetValue(coord) == "." || _model.Grid.GetValue(coord) == "S")
        {
            _model.Grid.SetValue(coord, "|");
            TraverseDown(x, y + 1);
            return;
        }

        // Check if splitter ^
        if (_model.Grid.GetValue(coord) == "^")
        {
            numSplits++;
            TraverseDown(x - 1, y);
            TraverseDown(x + 1, y);
            return;
        }
    }


    private NumGrid _numGrid;

    private long _part2 = 0;

    public void TraverseUp(int y)
    {
        if (y < 0)
            return;

        for (int x = 0; x < _model.Grid.Width; x++)
        {
            var val = _model.Grid.GetValue(x, y);
            if (val != "|")
                continue;

            var valBelow = _model.Grid.GetValue(x, y + 2);

            var numBelow = _numGrid.GetValue(x, y + 2); // The numeric value below us (in case we are not on a splitter)
            var numBelowLeft = _numGrid.GetValue(x - 1, y + 2); // the numeric value left below us (in case we ARE on a splitter)
            var numBelowRight = _numGrid.GetValue(x + 1, y + 2); // the numeric value right below us (also in the case of splitter)

            long myNum = 0;
            if (valBelow == "|")
            {
                myNum = numBelow;
            }
            else if (valBelow == "^")
            {
                myNum = numBelowLeft + numBelowRight;
            }

            if (y == 0)
            {
                // We are on the first row, there should be only one number here = the result of part 2.
                _part2 = myNum;
            }

            // Save the numeric value for this coordinate
            _numGrid.SetValue(Coord.Create(x, y), myNum);
        }

        var sss = _numGrid.GetAsString();

        TraverseUp(y - 2);
    }


    public string Run()
    {
        var sw = Stopwatch.StartNew();

        long sum = 0;

        _numGrid = new NumGrid();

        numSplits = 0;
        for (int x = 0; x< _model.Grid.Width; x++)
        {
            if (_model.Grid.GetValue(Coord.Create(x, 0)) == "S")
            {
                TraverseDown(x, 0);
            }
        }

        var part1 = numSplits;

        //var sss = _model.Grid.GetAsString();


        // Part 2 uses the map drawn by part 1 to go from bottom to the top. For last row, the number of "forks" from
        // one | is of course 1 - it is what it is.

        // Lines above that, for each splitter, we sum the two values from below right + left to get the value.
        // Going up one row at a time, fewer sums are calculated until we reach the top, where there is only
        // one value = the result!

        // Note, we can skip every other row, they are just pointless. So start with the
        // next last row, which has ^ on it. (Last row are just continuation of this)
        // Set a 1 on every column that has a |
        for (int x = 0; x < _model.Grid.Width; x++)
        {
            var coord = Coord.Create(x, _model.Grid.Height - 2);
            if (_model.Grid.GetValue(coord) == "|")
                _numGrid.SetValue(coord, 1);
        }

        // Now go to two lines above that and traverse every second line until we reach the top.
        TraverseUp((int) _model.Grid.Height - 4);

        //var xxx = _numGrid.GetAsString();

        var part2 = _part2;

        sw.Stop();
        var millisecs = sw.ElapsedMilliseconds; // 155 ms.

        return sum.ToString();
    }
}
