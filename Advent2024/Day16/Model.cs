using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day16
{
    public class Grid : GridBase<string>
    {
        public Coord S { get; set; }
        public Coord Direction { get; set; }
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

            int y = 0;
            foreach (var lx in Data.InputData.Split("\n"))
            {
                var l = lx.Trim();

                if (string.IsNullOrEmpty(l))
                    continue;

                var row = l.ToCharArray();
                for (int x = 0; x < row.Length; x++)
                {
                    var c = row[x].ToString();
                    var coord = Coord.Create(x, y);
                    if (c == "S")
                    {
                        retObj.Grid.S = coord;
                        retObj.Grid.Direction = Coord.Create(1, 0); // Eastbound
                        c = ".";
                    }
                    retObj.Grid.SetValue(coord, c);
                }

                y++;
            }

            return retObj;
        }
    }
}
