using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventUtils
{
    public record Coord
    {
        public long X { get; private set; }
        public long Y { get; private set; }

        [DebuggerHidden]
        public Coord Copy()
        {
            return Coord.Create(X, Y);
        }
        [DebuggerHidden]
        public static Coord Create(int x, int y)
        {
            return new Coord {X = x, Y = y};
        }
        [DebuggerHidden]
        public static Coord Create(long x, long y)
        {
            return new Coord { X = x, Y = y };
        }

        [DebuggerHidden]
        public override string ToString()
        {
            return X + "," + Y;
        }

        [DebuggerHidden]
        public Coord Up() {  return Coord.Create(X, Y - 1); }
        [DebuggerHidden]
        public Coord Down() { return Coord.Create(X, Y + 1); }
        [DebuggerHidden]
        public Coord Left() { return Coord.Create(X - 1, Y); }
        [DebuggerHidden]
        public Coord Right() { return Coord.Create(X + 1, Y); }

        [DebuggerHidden]
        public Coord Add(Coord direction)
        {
            var x = this.X + direction.X;
            var y = this.Y + direction.Y;
            return Coord.Create(x, y);
        }

        [DebuggerHidden]
        public Coord Subtract(Coord direction)
        {
            var x = this.X - direction.X;
            var y = this.Y - direction.Y;
            return Coord.Create(x, y);
        }

        public Coord RotateClockwise()
        {
            if (this.X == 1 && this.Y == 0) // East
                return Coord.Create(0, 1); // South
            if (this.X == -1 && this.Y == 0) // West
                return Coord.Create(0, -1); // North
            if (this.X == 0 && this.Y == 1) // South
                return Coord.Create(-1, 0); // West
            if (this.X == 0 && this.Y == -1) // North
                return Coord.Create(1, 0); // East
            throw new Exception("Can only rotate directional coordinates");
        }

        public Coord RotateCounterClockwise()
        {
            if (this.X == 1 && this.Y == 0) // East
                return Coord.Create(0, -1); // North
            if (this.X == -1 && this.Y == 0) // West
                return Coord.Create(0, 1); // South
            if (this.X == 0 && this.Y == 1) // South
                return Coord.Create(1, 0); // East
            if (this.X == 0 && this.Y == -1) // North
                return Coord.Create(-1, 0); // West
            throw new Exception("Can only rotate directional coordinates");
        }
    }

    public class GridBase<T>
    {
        public long Height { get; set; }
        public long Width { get; set; }

        public Dictionary<Coord, T> _grid = new Dictionary<Coord, T>();

        public bool IsInside(Coord coord)
        {
            if (coord.X < 0 || coord.Y < 0)
                return false;

            if (coord.Y >= Height)
                return false;
            if (coord.X >= Width)
                return false;

            return true;
        }

        public T GetValue(Coord coord)
        {
            if (!IsInside(coord))
                return default;

            if (!_grid.ContainsKey(coord))
                return default;

            return _grid[coord];
        }

        public void SetValue(Coord coord, T value)
        {
            if (coord.X >= Width)
                Width = coord.X + 1;
            if (coord.Y >= Height)
                Height = coord.Y + 1;
            _grid[coord] = value;
        }

        public string GetAsString()
        {
            var sb = new StringBuilder("");
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var c = GetValue(Coord.Create(x, y));
                    if (c != null)
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        sb.Append(".");
                    }
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }


    }
}
