using System.Runtime.Intrinsics;

namespace Advent2025.Day03;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private static long _maxValue = 0;
    private static string _maxValueStr = "";

    private static void Traverse(List<int> includedJolts, List<int> remainingJolts)
    {
        if (_maxValueStr == "999999999999")
        {
            return;
        }

        if (_maxValueStr != "")
        {
            // If we already have a max, we can directly see if we are on a track/branch that will never beat it!
            for (int i = 0; i < includedJolts.Count; i++)
            {
                var c = includedJolts[i];
                var m = int.Parse(_maxValueStr[i].ToString());
                if (c < m)
                    return; // cannot beat max value on this branch.
                if (c > m)
                    break; // can beat max value on this branch, continue.
            }
        }

        if (includedJolts.Count == 12)
        {
            // check if max value.
            var s = string.Concat(includedJolts.Select(i => i.ToString()));
            var v = long.Parse(s);
            if (v > _maxValue)
            {
                _maxValue = v;
                _maxValueStr = s;
            }

            return;
        }

        if (remainingJolts.Count == 0)
            return;

        if (includedJolts.Count + remainingJolts.Count < 12)
            return;

        if (includedJolts.Count > 12)
            return;

        // First traverse including the first of the remainting jolts.
        var includedJoltsWithFirstRemaining = includedJolts.ToList();
        includedJoltsWithFirstRemaining.Add(remainingJolts[0]);
        Traverse(includedJoltsWithFirstRemaining, remainingJolts.Skip(1).ToList());

        // And then traverse but skipping the first of the remaining jolts.
        Traverse(includedJolts, remainingJolts.Skip(1).ToList());
    }

    private static int GetHighestValue(List<int> list, int maxIndex)
    {
        int maxVal = 0;
        for (int i = 0; i <= maxIndex; i++)
        {
            if (list[i] > maxVal)
                maxVal = list[i];
        }
        return maxVal;
    }


    private static long CalcHighestJolt(List<int> jolts)
    {
        var lastPossibleIndex = jolts.Count - 12;

        var maxVal = GetHighestValue(jolts, lastPossibleIndex);

        var startingIndex = 0;
        for (int i = lastPossibleIndex; i >= 0; i--)
        {
            if (jolts[i] == maxVal)
            {
                startingIndex = i;
                break;
            }
        }

        _maxValue = 0;
        _maxValueStr = "";

        var firstTryList = jolts.Skip(startingIndex).ToList();

        Traverse(new List<int>(), firstTryList.ToList());


        Traverse(new List<int>(), jolts.ToList());
        return _maxValue;

        //var maxVal = 0;

        //for (int i = 0; i < jolts.Count - 1; i++)
        //{
        //    for (int j = i + 1; j < jolts.Count; j++)
        //    {
        //        var v1 = jolts[i];
        //        var v2 = jolts[j];
        //        var val = v1 * 10 + v2;

        //        if (val > maxVal)
        //            maxVal = val;
        //    }
        //}

        //return maxVal;
    }

    public string Run()
    {
        long sum = 0;
        int numCalcs = 0;

        foreach (var battery in _model.BatteryBanks)
        {
            var val = CalcHighestJolt(battery.Jolts);
            sum += val;
            numCalcs++;
        }

        return sum.ToString();
    }
}
