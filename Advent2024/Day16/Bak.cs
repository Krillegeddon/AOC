using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day16
{
    public record BakStep
    {
        public long FromId { get; set; }
        public long ToId { get; set; }
        public Coord FromCoord { get; set; }
        public Coord ToCoord { get; set; }
    }

    internal class Bak
    {
        private Grid _grid;
        public Bak(Model model)
        {
            _grid = model.Grid;
        }

        private Dictionary<Coord, long> _visited = new Dictionary<Coord, long>();

        private long _id = 0;

        public Dictionary<Coord, bool> _winningCoords = new Dictionary<Coord, bool>();
        private bool _hangWhenReturn = false;

        private Dictionary<Coord, int> _junctions = new Dictionary<Coord, int>();

        private long CalcDist(Coord coord, Coord direction, long points, long id)
        {
            if (Coord.Create(5, 13) == coord)
            {
                int bb = 0;
            }

            if (!_grid.IsInside(coord))
                return -1;

            if (_grid.GetValue(coord) == "E")
            {
                return points;
            }

            if (_grid.GetValue(coord) == "#")
                return -1;

            if (_visited.ContainsKey(coord))
            {
                //return -1;
                var v = _visited[coord];
                //if (points > _visited[coord] && points - 1000 != _visited[coord])
                if (points >= _visited[coord])
                {
                    if (_hangWhenReturn)
                    {
                        int bb = 9;
                    }
                    return -1;
                }
                _visited[coord] = points;
            }
            else
            {
                _visited.Add(coord, points);
            }

            var dir1 = direction.RotateClockwise();
            var dir2 = direction.RotateCounterClockwise();

            var coord0 = coord.Add(direction);
            var coord1 = coord.Add(dir1);
            var coord2 = coord.Add(dir2);

            var forks = 0;

            if (_grid.GetValue(coord0) != "#")
            {
                forks++;
            }
            if (_grid.GetValue(coord1) != "#")
            {
                forks++;
            }
            if (_grid.GetValue(coord2) != "#")
            {
                forks++;
            }

            if (forks > 1)
            {
                if (!_junctions.ContainsKey(coord))
                    _junctions.Add(coord, forks);
            }


            _id++;
            var dist0 = CalcDist(coord0, direction, points + 1, _id);
            _id++;
            var dist1 = CalcDist(coord1, dir1, points + 1001, _id);
            _id++;
            var dist2 = CalcDist(coord2, dir2, points + 1001, _id);

            var dists = new List<long>();
            if (dist0 > 0) dists.Add(dist0);
            if (dist1 > 0) dists.Add(dist1);
            if (dist2 > 0) dists.Add(dist2);

            if (dists.Count == 0)
                return -1;

            if (dist0 <= 0)
                dist0 = long.MaxValue;
            if (dist1 <= 0)
                dist1 = long.MaxValue;
            if (dist2 <= 0)
                dist2 = long.MaxValue;

            if (dist0 <= dist1 && dist0 <= dist2)
            {
                if (!_winningCoords.ContainsKey(coord0))
                    _winningCoords.Add(coord0, true);
            }

            if (dist1 <= dist0 && dist1 <= dist2)
            {
                if (!_winningCoords.ContainsKey(coord1))
                    _winningCoords.Add(coord1, true);
            }

            if (dist2 <= dist0 && dist2 <= dist1)
            {
                if (!_winningCoords.ContainsKey(coord2))
                    _winningCoords.Add(coord2, true);
            }
            return dists.Min(p => p);
        }

        public void PrintGrid(bool waitForEnter = false)
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                var line = "";
                for (int x = 0; x < _grid.Width; x++)
                {
                    var coord = Coord.Create(x, y);
                    if (coord.X == 5 && coord.Y == 13)
                    {
                        line += "$";
                        continue;
                    }

                    if (_winningCoords.ContainsKey(coord))
                    {
                        line += "O";
                        continue;
                    }

                    if (_grid._grid.ContainsKey(coord))
                    {
                        line += _grid._grid[coord];
                    }
                    else
                        line += " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("******************************************************************************************");
            if (waitForEnter)
                Console.ReadLine();
        }

        public string Run()
        {
            long sum = 0;

            sum = CalcDist(_grid.S, _grid.Direction, 0, _id);
            var val2 = _winningCoords.Count();
            var j = _junctions;

            //PrintGrid();

            return sum.ToString();
        }
    }
}
