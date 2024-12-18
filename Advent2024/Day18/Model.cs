using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day18
{
    public class Grid : GridBase<string>
    {

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

            int num = 0;
            foreach (var lx in Data.InputData.Split("\n"))
            {
                var l = lx.Trim();

                if (string.IsNullOrEmpty(l))
                    continue;

                var arr = l.Split(',');
                var coord = Coord.Create(int.Parse(arr[0]), int.Parse(arr[1]));
                retObj.Grid.SetValue(coord, "#");

                num++;
                if (num == 2907)
                    return retObj;
            }

            return retObj;
        }
    }
}
