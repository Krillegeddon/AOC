namespace Advent2025.Day08;

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Point(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"({X},{Y},{Z})";
    }

    public static double CalculateDistance(Point p1, Point p2)
    {
        double distance = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) +
                                     Math.Pow(p2.Y - p1.Y, 2) +
                                     Math.Pow(p2.Z - p1.Z, 2));
        return distance;
    }
}

public class Model
{
    public required List<Point> JunctionBoxes { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            JunctionBoxes = new List<Point>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var parts = l.Split(',');
            var point = new Point(double.Parse(parts[0]),
                                  double.Parse(parts[1]),
                                  double.Parse(parts[2]));
            retObj.JunctionBoxes.Add(point);
        }

        return retObj;
    }
}
