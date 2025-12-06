namespace Advent2025.Day05;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private static bool IsIngredientFresh(List<Range> ranges, long ingredientId)
    {
        foreach (var range in ranges)
        {
            if (ingredientId >= range.Start && ingredientId <= range.End)
                return true;
        }

        return false;
    }

    private static bool AreRangesOverlapping(Range r1, Range r2)
    {
        if (r1.End < r2.Start || r2.End < r1.Start)
            return false;
        return true;
    }

    private static Range MergeRanges(Range r1, Range r2)
    {
        return new Range
        {
            Start = Math.Min(r1.Start, r2.Start),
            End = Math.Max(r1.End, r2.End),
        };
    }

    private static List<Range> MergeAllOverlappingRanges(List<Range> ranges)
    {
        ranges = ranges.OrderBy(r => r.Start).ToList();
        List<Range> mergedRanges = new List<Range>();
        Range currentRange = ranges[0];
        for (int i = 1; i < ranges.Count; i++)
        {
            if (AreRangesOverlapping(currentRange, ranges[i]))
            {
                currentRange = MergeRanges(currentRange, ranges[i]);
            }
            else
            {
                mergedRanges.Add(currentRange);
                currentRange = ranges[i];
            }
        }
        mergedRanges.Add(currentRange);
        return mergedRanges;
    }

    public string Run()
    {
        long sum = 0;

        //foreach (var ingredientId in _model.Ingredients)
        //{
        //    if (IsIngredientFresh(_model.Ranges, ingredientId))
        //        sum++;
        //}

        var mergedRanges = MergeAllOverlappingRanges(_model.Ranges);

        foreach (var range in mergedRanges)
        {
            sum += (range.End - range.Start + 1);
        }

        return sum.ToString();
    }
}
