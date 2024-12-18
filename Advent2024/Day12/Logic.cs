using AdventUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Advent2024.Day12.Logic;

namespace Advent2024.Day12
{
    public record AreaId
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name + " (" + ID + ")";
        }
    }

    public class Logic
    {
        private static Dictionary<Coord, bool> _visitedAreas = new Dictionary<Coord, bool>();
        private static Dictionary<AreaId, List<Coord>> _areas = new Dictionary<AreaId, List<Coord>>();
        private static Dictionary<Coord, AreaId> _coords = new Dictionary<Coord, AreaId>();
        private static Grid _grid;


        private static List<Coord> GetNeighbourAreas(Coord coord)
        {
            var neighbours = new List<Coord>()
            {
                Coord.Create(coord.X, coord.Y - 1),
                Coord.Create(coord.X, coord.Y + 1),
                Coord.Create(coord.X - 1, coord.Y),
                Coord.Create(coord.X + 1, coord.Y),
            };

            return neighbours;
        }


        private static void Traverse(Coord coord, string wantedId, int currentId)
        {
            if (_visitedAreas.ContainsKey(coord))
                return;
            _visitedAreas.Add(coord, true);

            var myValue = _grid.GetValue(coord);

            var myAreaId = new AreaId { ID = currentId, Name = myValue };

            if (wantedId == myValue)
            {
                if (!_areas.ContainsKey(myAreaId))
                    _areas.Add(myAreaId, new List<Coord>());
                _areas[myAreaId].Add(coord);
                _coords.Add(coord, myAreaId);
            }

            var buddies = GetNeighbourAreas(coord)
                .Where(p => _grid.GetValue(p) == wantedId &&
                            _grid.IsInside(p) &&
                        !_visitedAreas.ContainsKey(p))
                .ToList();

            foreach (var buddy in buddies)
            {
                Traverse(buddy, wantedId, currentId);
            }
        }

        public static bool HasFence(Coord coord, int edgeIndex)
        {
            var neighbours = new List<Coord>()
            {
                Coord.Create(coord.X, coord.Y - 1),
                Coord.Create(coord.X + 1, coord.Y),
                Coord.Create(coord.X, coord.Y + 1),
                Coord.Create(coord.X - 1, coord.Y),
            };

            var coordToCheck = neighbours[edgeIndex];

            if (!_grid.IsInside(coordToCheck))
                return true;
            if (_coords[coordToCheck] != _coords[coord])
                return true;

            return false;
        }


        public class EdgeInfo
        {
            // EdgeIndex - 0=Up, 1=Right, 2=Down, 3=Left

            // True if having this edge
            public Dictionary<int, bool> hasEdge = new Dictionary<int, bool>();
            // 1 or 0 depending if too count this edge or not when summarizing
            public Dictionary<int, int> numEdge = new Dictionary<int, int>();
        }


        private static Dictionary<Coord, EdgeInfo> _edges = new Dictionary<Coord, EdgeInfo>();

        private static void MarkEdgeInfo(Coord coord, int edgeIndex, int num)
        {
            if (!_edges.ContainsKey(coord))
            {
                _edges.Add(coord, new EdgeInfo());
            }
            var edgeInfo = _edges[coord];

            var debug = _edges[Coord.Create(0, 0)];

            edgeInfo.hasEdge.Add(edgeIndex, true);
            edgeInfo.numEdge.Add(edgeIndex, num);
        }

        private static bool HasAlreadyCheckedEdge(Coord coord, int edgeIndex)
        {
            if (!_edges.ContainsKey(coord))
            {
                _edges.Add(coord, new EdgeInfo());
            }
            var edgeInfo = _edges[coord];

            return edgeInfo.hasEdge.ContainsKey(edgeIndex);
        }

        // edgeIndex: 0=up, 1=right, 2=down, 3=left
        private static void FillLine(Coord coord, Coord direction, int edgeIndex)
        {
            var otherCoord = coord.Copy();
            while (true)
            {
                otherCoord = Coord.Create(otherCoord.X + direction.X, otherCoord.Y + direction.Y);
                if (!_grid.IsInside(otherCoord))
                    break;

                if (_coords[coord] != _coords[otherCoord])
                    break;

                if (HasFence(otherCoord, edgeIndex))
                {
                    // Mark it, but don't count the edge, since this edge has already been
                    // counted..
                    MarkEdgeInfo(otherCoord, edgeIndex, 0);
                }
                else
                {
                    break;
                }
            }

            otherCoord = coord.Copy();
            while (true)
            {
                otherCoord = Coord.Create(otherCoord.X - direction.X, otherCoord.Y - direction.Y);
                if (!_grid.IsInside(otherCoord))
                    break;

                if (_coords[coord] != _coords[otherCoord])
                    break;

                if (HasFence(otherCoord, edgeIndex))
                {
                    // Mark it, but don't count the edge, since this edge has already been
                    // counted..
                    MarkEdgeInfo(otherCoord, edgeIndex, 0);
                }
                else
                {
                    break;
                }
            }
        }


        private static void TraverseEdge(Coord coord, int edgeIndex)
        {
            // If we don't have a fence in the direction of the edgeIndex, then we are not of interest.
            if (!HasFence(coord, edgeIndex))
                return;

            // If this edge has already been checked, then skip. This is if this coord has already
            // been marked via FilLine...
            if (HasAlreadyCheckedEdge(coord, edgeIndex))
                return;

            // We add this edgeIndex as true and numEdge to 1 since this will be counted.
            // the other ones set in FillLine will be true/0.
            MarkEdgeInfo(coord, edgeIndex, 1);

            // So, we now have a coord with fence in the same direction, the go through neighbours
            // as far as we can in this direction!
            var direction = Coord.Create(1, 0); // Default direction is right/left
            if (edgeIndex == 1 || edgeIndex == 3)
            {
                // If we are checking on edges to left or right, then the direction is up/down.
                direction = Coord.Create(0, 1);
            }

            FillLine(coord, direction, edgeIndex);
        }

        public static string Run()
        {
            var model = Model.Parse();
            _grid = model.Grid;
            long sum = 0;

            var areaIndex = 0;
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    var coord = Coord.Create(x, y);
                    if (_coords.ContainsKey(coord))
                        continue;
                    var startValue = model.Grid.GetValue(coord);

                    areaIndex++;
                    Traverse(coord, startValue, areaIndex);

                }
            }

            var areaIds = _areas.Keys.ToList();
            foreach (var areaId in areaIds)
            {
                var coordsInArea = _areas[areaId];
                var numFences = 0;

                for (int edgeIndex = 0; edgeIndex < 4; edgeIndex++)
                {
                    foreach (var coord in coordsInArea)
                    {
                        TraverseEdge(coord, edgeIndex);
                    }
                }

                foreach (var c in coordsInArea)
                {
                    if (!_edges.ContainsKey(c))
                    {
                        continue;
                    }
                    foreach (var num in _edges[c].numEdge)
                    {
                        if (num.Value > 0)
                        {
                            numFences += num.Value;
                        }
                    }
                }

                sum += numFences * coordsInArea.Count;
            }

            return sum.ToString();
        }
    }
}
