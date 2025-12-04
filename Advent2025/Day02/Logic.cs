namespace Advent2025.Day02;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private static bool IsInvalidId(long val)
    {
        //return IsInvalidId(2, val.ToString()); // Part 1

        var valstr = val.ToString();

        for (int i = 2; i < valstr.Length + 1; i++)
        {
            if (IsInvalidId(i, valstr))
                return true;
        }
        return false;
    }


    private static bool IsInvalidId(int times, string val)
    {
        // If not divisible by length, can't be invalid
        if (val.Length % times != 0)
            return false;

        var strings = new List<string>();

        var partLength = val.Length / times;
        var partVals = "";
        for (var i = 0; i < times; i++)
        {
            var part = val.Substring(i * partLength, partLength);
            if (partVals == "")
                partVals = part;
            else if (partVals != part)
                return false;
        }

        return true;
    }


    public string Run()
    {
        long sum = 0;

        foreach (var interval in _model.Intervals)
        {
            for (long i = interval.Start; i <= interval.End; i++)
            {
                if (IsInvalidId(i))
                {
                    sum += i;
                }
            }
        }

        return sum.ToString();
    }
}
