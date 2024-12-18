using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day15
{
    public class Logic
    {
        private Grid _grid;
        public Logic(Model model)
        {
            _grid = model.Grid;
        }


        private Coord FindFirstFreeSlot(Coord position, Coord direction)
        {
            for (var coord = position.Copy(); _grid.GetValue(coord) != "#"; coord = coord.Add(direction))
            {
                if (_grid.GetValue(coord) == ".")
                    return coord;
            }
            return null;
        }

        private void Move(Coord direction)
        {
            var nextCoord = _grid.RobotPosition.Add(direction);

            // If the new position is free - then just move there!
            if (_grid.GetValue(nextCoord) == ".")
            {
                _grid.RobotPosition = nextCoord;
                return;
            }

            // If next position is a wall, just stand still...
            if (_grid.GetValue(nextCoord) == "#")
            {
                return;
            }

            // If we are on an obstacle...
            if (_grid.GetValue(nextCoord) == "[" || _grid.GetValue(nextCoord) == "]")
            {
                if (direction.Y == 0)
                {
                    // Left/right works just like in part 1...
                    var nextFreeSlot = FindFirstFreeSlot(nextCoord, direction);

                    // If no free slot was found, just stand still
                    if (nextFreeSlot == null)
                        return;

                    // Move all obstacles one step at a time...
                    for (var coord = nextFreeSlot.Copy(); !coord.Equals(nextCoord); coord = coord.Subtract(direction))
                    {
                        var obstaclePosition = coord.Subtract(direction);
                        var obstacleChar = _grid.GetValue(obstaclePosition);

                        // Now, we shall move the obstacle from obstaclePosition to coord. Coord is always
                        // a free spot, and obstaclePosition will be free afterwards.
                        _grid.SetValue(coord, obstacleChar);
                        _grid.SetValue(obstaclePosition, ".");
                    }
                    _grid.RobotPosition = nextCoord;
                }
                else
                {
                    // Up/down is a bit different. We need take into account that multiple x-values is affecting
                    // when a block can be moved.

                }
            }
        }

        public string Run()
        {
            long sum = 0;
            _grid.Print();

            foreach (var direction in _grid.Moves)
            {
                Move(direction);
                _grid.Print(true);
            }

            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GetValue(Coord.Create(x, y)) == "O")
                    {
                        sum += 100 * y + x;
                    }
                }

            }


            return sum.ToString();
        }
    }
}
