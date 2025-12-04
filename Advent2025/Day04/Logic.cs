using AdventUtils;

namespace Advent2025.Day04;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private static int CountNeighbors(Grid grid, Coord coord)
    {
        var numNeigh = 0;

        for (var x = coord.X - 1; x <= coord.X + 1; x++)
        {
            for (var y = coord.Y - 1; y <= coord.Y + 1; y++)
            {
                if (x == coord.X && y == coord.Y)
                    continue;
                if (grid.GetValue(Coord.Create(x, y)) == "@" || grid.GetValue(Coord.Create(x, y)) == "x")
                    numNeigh++;
            }
        }


        return numNeigh;
    }

    public string Run()
    {
        long sum = 0;



        while (true)
        {
            for (var x = 0; x < _model.Grid.Width; x++)
            {
                for (var y = 0; y < _model.Grid.Height; y++)
                {
                    var coord = Coord.Create(x, y);
                    if (_model.Grid.GetValue(coord) != "@")
                        continue;

                    int numNeigh = CountNeighbors(_model.Grid, coord);
                    if (numNeigh < 4)
                    {
                        _model.Grid.SetValue(coord, "x");
                        sum++;
                    }
                }
            }

            bool anyX = false;
            for (var x = 0; x < _model.Grid.Width; x++)
            {
                for (var y = 0; y < _model.Grid.Height; y++)
                {
                    var coord = Coord.Create(x, y);
                    if (_model.Grid.GetValue(coord) == "x")
                    {
                        _model.Grid.SetValue(coord, ".");
                        anyX = true;
                    }
                }
            }

            if (!anyX)
                break;
        }



        return sum.ToString();
    }
}
