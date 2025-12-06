using System.Reflection.Emit;

namespace Advent2025.Day06;

public class Model
{
    public required List<List<long>> NumbersSheet { get; set; }
    public required List<string> Operands { get; set; }
    public required List<int> StartingIndexes { get; set; }
    public required List<int> Lengths { get; set; }
    public required List<List<string>> NumbersGrid { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            NumbersSheet = new List<List<long>>(),
            Operands = new List<string>(),
            StartingIndexes = new List<int>(),
            Lengths = new List<int>(),
            NumbersGrid = new List<List<string>>(),
        };

        foreach (var l in Data.InputData.Split("\n"))
        {
            if (string.IsNullOrEmpty(l.Trim()))
                continue;

            var larr = l.ToCharArray().Select(p => p.ToString()).ToList();

            if (l.Contains("+") || l.Contains("-") || l.Contains("*") || l.Contains("/"))
            {
                var operandIndex = 0;
                for (int i = 0; i < larr.Count; i++)
                {
                    if (larr[i] == " ")
                        continue;
                    retObj.StartingIndexes.Add(i);
                    retObj.Operands.Add(larr[i]);
                    if (i > 0)
                    {
                        retObj.Lengths.Add(i - retObj.StartingIndexes[operandIndex - 1] - 1);
                    }
                    operandIndex++;
                }
                retObj.Lengths.Add(larr.Count - retObj.StartingIndexes.Last());
                continue;
            }

            retObj.NumbersGrid.Add(larr);
        }

        return retObj;
    }

    //public static Model Parse1()
    //{
    //    var retObj = new Model
    //    {
    //        NumbersSheet = new List<List<long>>(),
    //        Operands = new List<string>(),
    //        StartingIndexes = new List<int>(),
    //    };

    //    foreach (var lx in Data.InputData.Split("\n"))
    //    {
    //        var l2 = lx.Trim();
    //        var l = l2.Replace("\t", " ");
    //        l = l.Replace("  ", " ");
    //        l = l.Replace("  ", " ");
    //        l = l.Replace("  ", " ");
    //        l = l.Replace("  ", " ");
    //        l = l.Replace("  ", " ");

    //        if (string.IsNullOrEmpty(l))
    //            continue;

    //        if (l.Contains("+") || l.Contains("-") || l.Contains("*") || l.Contains("/"))
    //        {
    //            var rowOps = l.Split(" ").ToList();
    //            retObj.Operands = rowOps;
    //            continue;
    //        }

    //        var row = l.Split(" ").Select(p=>long.Parse(p)).ToList();
    //        retObj.NumbersSheet.Add(row);
    //    }

    //    return retObj;
    //}
}
