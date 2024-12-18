using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day15
{
    public class Grid : GridBase<string>
    {
        public Coord RobotPosition { get; set; }

        public int MovePointer = 0;
        public List<Coord> Moves { get; set; }

        public Grid()
        {
            Moves = new List<Coord>();
            RobotPosition = Coord.Create(0, 0);
        }

        public void Print(bool waitForEnter = false)
        {
            for (int y = 0; y < Height; y++)
            {
                var line = "";
                for (int x = 0; x < Width; x++)
                {
                    var coord = Coord.Create(x, y);
                    if (x == RobotPosition.X && y == RobotPosition.Y)
                    {
                        line += "@";
                    }
                    else
                    {
                        if (_grid.ContainsKey(coord))
                        {
                            line += _grid[coord];
                        }
                        else
                            line += " ";
                    }
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("******************************************************************************************");
            if (waitForEnter)
                Console.ReadLine();
        }

    }

    public class Model
    {
        public required Grid Grid { get; set; }

        public static Model Parse()
        {
            var retObj = new Model
            {
                Grid = new Grid()
            };

            var y = 0;
            var isReadingGrid = true;
            foreach (var lx in Data.InputData.Split("\n"))
            {
                var l = lx.Trim();

                if (string.IsNullOrEmpty(l))
                    isReadingGrid = false;

                if (isReadingGrid)
                {
                    var row = l.ToCharArray();
                    for (int x = 0; x < row.Length; x++)
                    {
                        var c = row[x].ToString();
                        var c2 = c;
                        if (c == "@")
                        {
                            retObj.Grid.RobotPosition = Coord.Create(x * 2, y);
                            c = ".";
                            c2 = ".";
                        }
                        if (c == "O")
                        {
                            c = "[";
                            c2 = "]";
                        }

                        retObj.Grid.SetValue(Coord.Create(x * 2, y), c);
                        retObj.Grid.SetValue(Coord.Create(x * 2 + 1, y), c2);
                    }
                }
                else
                {
                    var row = l.ToCharArray();
                    foreach (var c in row)
                    {
                        Coord direction;
                        switch (c.ToString())
                        {
                            case ">":
                                direction = Coord.Create(1, 0);
                                break;
                            case "<":
                                direction = Coord.Create(-1, 0);
                                break;
                            case "^":
                                direction = Coord.Create(0, -1);
                                break;
                            case "v":
                                direction = Coord.Create(0, 1);
                                break;
                            default:
                                throw new Exception("Weird move character found: " + c.ToString());
                        }

                        retObj.Grid.Moves.Add(direction);
                    }
                }

                y++;
            }

            return retObj;
        }
    }
}
