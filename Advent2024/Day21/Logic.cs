using AdventUtils;
using System.Numerics;

namespace Advent2024.Day21;

public class Grid : GridBase<string>
{
}
//<A^A>^^AvvvA, <A^A^>^AvvvA, and <A^A^^>AvvvA.

public class Pad
{
    public Coord Coord { get; set; }
    public Grid Grid { get; set; }

    private List<Coord> GetCoordsYFirst(Coord start, Coord finalCoord)
    {
        var list = new List<Coord>();
        var coord = start.Copy();

        var vector = finalCoord.Subtract(Coord);
        var numY = (int)Math.Abs(vector.Y);
        var signY = Math.Sign(vector.Y);
        var numX = (int)Math.Abs(vector.X);
        var signX = Math.Sign(vector.X);

        for (int i = 0; i < numY; i++)
        {
            var dir = Coord.Create(0, signY);
            coord = coord.Add(dir);
            if (Grid.GetValue(coord) == " ")
                return null;
            list.Add(dir);
        }

        for (int i = 0; i < numX; i++)
        {
            var dir = Coord.Create(signX, 0);
            coord = coord.Add(dir);
            if (Grid.GetValue(coord) == " ")
                return null;
            list.Add(dir);
        }
        return list;
    }

    private List<Coord> GetCoordsXFirst(Coord start, Coord finalCoord)
    {
        var list = new List<Coord>();
        var coord = start.Copy();

        var vector = finalCoord.Subtract(Coord);
        var numY = (int)Math.Abs(vector.Y);
        var signY = Math.Sign(vector.Y);
        var numX = (int)Math.Abs(vector.X);
        var signX = Math.Sign(vector.X);

        for (int i = 0; i < numX; i++)
        {
            var dir = Coord.Create(signX, 0);
            coord = coord.Add(dir);
            if (Grid.GetValue(coord) == " ")
                return null;
            list.Add(dir);
        }

        for (int i = 0; i < numY; i++)
        {
            var dir = Coord.Create(0, signY);
            coord = coord.Add(dir);
            if (Grid.GetValue(coord) == " ")
                return null;
            list.Add(dir);
        }

        return list;
    }

    public List<List<Coord>> PressButton(string val)
    {
        var retList = new List<List<Coord>>();
        var finalCoord = Grid._grid.Where(p => p.Value == val).First().Key;

        var listx1 = GetCoordsXFirst(Coord, finalCoord);
        var listy1 = GetCoordsYFirst(Coord, finalCoord);

        var listx2 = GetCoordsXFirst(finalCoord, Coord);
        var listy2 = GetCoordsYFirst(finalCoord, Coord);

        if (listx1 != null && listx2 != null)
        {
            var list = listx1.ToList();
            list.AddRange(listx2);
            retList.Add(list);
        }
        if (listx1 != null && listy2 != null)
        {
            var list = listx1.ToList();
            list.AddRange(listy2);
            retList.Add(list);
        }
        if (listy1 != null && listx2 != null)
        {
            var list = listy1.ToList();
            list.AddRange(listx2);
            retList.Add(list);
        }
        if (listy1 != null && listy2 != null)
        {
            var list = listy1.ToList();
            list.AddRange(listy2);
            retList.Add(list);
        }

        return retList;
    }


}

public class NumericPad : Pad
{

    public NumericPad()
    {
        Grid = new Grid();
        Grid.SetValue(Coord.Create(0, 0), "7");
        Grid.SetValue(Coord.Create(1, 0), "8");
        Grid.SetValue(Coord.Create(2, 0), "9");
        Grid.SetValue(Coord.Create(0, 1), "4");
        Grid.SetValue(Coord.Create(1, 1), "5");
        Grid.SetValue(Coord.Create(2, 1), "6");
        Grid.SetValue(Coord.Create(0, 2), "1");
        Grid.SetValue(Coord.Create(1, 2), "2");
        Grid.SetValue(Coord.Create(2, 2), "3");
        Grid.SetValue(Coord.Create(0, 3), " ");
        Grid.SetValue(Coord.Create(1, 3), "0");
        Grid.SetValue(Coord.Create(2, 3), "A");

        Coord = Coord.Create(2, 3);
    }

    public string GetCurrentValue()
    {
        return Grid.GetValue(Coord);
    }
}


public class DirectionPad: Pad
{
    public DirectionPad()
    {
        Grid = new Grid();
        Grid.SetValue(Coord.Create(0, 0), " ");
        Grid.SetValue(Coord.Create(1, 0), "^");
        Grid.SetValue(Coord.Create(2, 0), "A");
        Grid.SetValue(Coord.Create(0, 1), "<");
        Grid.SetValue(Coord.Create(1, 1), "v");
        Grid.SetValue(Coord.Create(2, 1), ">");

        Coord = Coord.Create(2, 0);
    }
}


public class Arrangement
{
    public DirectionPad MyPad { get; set; }
    public DirectionPad RobPad1 { get; set; }
    public DirectionPad RobPad2 { get; set; }
    public NumericPad NumPad { get; set; }

    public Arrangement()
    {
        MyPad = new DirectionPad();
        RobPad1 = new DirectionPad();
        RobPad2 = new DirectionPad();
        NumPad = new NumericPad();
    }

    public List<List<Coord>> FindDirectionsForNumber(string number)
    {
        var dirs1 = NumPad.PressButton(number);

        return null;
    }
}

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    public string Run()
    {
        //var numPad = new NumericPad();

        //var dirs1 = numPad.GetDirectionsForValue("5");

        //var dirPad = new DirectionPad();

        //var dirs2 = dirPad.GetDirectionsForValue("<");

        long sum = 0;
        var arr = new Arrangement();

        return sum.ToString();
    }
}
