namespace Advent2025.Day06;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    public string Run()
    {
        long sum = 0;

        var numCols = _model.Operands.Count;

        var sums = new List<long>();


        for (var xIndex = 0; xIndex < _model.Operands.Count; xIndex++)
        {
            var operand = _model.Operands[xIndex];
            var startingIndex = _model.StartingIndexes[xIndex];
            var length = _model.Lengths[xIndex];
            long colSum = 0;
            if (operand == "*")
                colSum = 1;

            var subNumbers = new List<long>();

            for (var x = startingIndex; x < startingIndex + length; x++)
            {
                var subDigits = "";

                for (var y = 0; y < _model.NumbersGrid.Count; y++)
                {
                    subDigits += _model.NumbersGrid[y][x];
                }

                subNumbers.Add(long.Parse(subDigits.Replace(" ", "")));
            }

            foreach (var num in subNumbers)
            {
                if (operand == "+")
                    colSum += num;
                if (operand == "*")
                    colSum *= num;
            }

            sums.Add(colSum);
        }

        foreach (var s in sums)
        {
            sum += s;
        }

        return sum.ToString();
    }


    //public string Run1()
    //{
    //    long sum = 0;

    //    var numCols = _model.Operands.Count;

    //    var sums = new List<long>();


    //    for (var x = 0; x < _model.Operands.Count; x++)
    //    {
    //        var operand = _model.Operands[x];
    //        long colSum = 0;
    //        if (operand == "*")
    //            colSum = 1;

    //        for (var y = 0; y < _model.NumbersSheet.Count; y++)
    //        {
    //            var num = _model.NumbersSheet[y][x];
    //            if (operand == "+")
    //                colSum += num;
    //            if (operand == "*")
    //                colSum *= num;
    //        }

    //        sums.Add(colSum);
    //    }

    //    foreach (var s in sums)
    //    {
    //        sum += s;
    //    }

    //    return sum.ToString();
    //}


}
