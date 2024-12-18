using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day11
{
    public class Model
    {
        public required List<long> Numbers { get; set; }

        public static Model Parse()
        {
            var retObj = new Model
            {
                Numbers = new List<long>()
            };

            foreach (var lx in Data.InputData.Split("\n"))
            {
                var l = lx.Trim();

                if (string.IsNullOrEmpty(l))
                    continue;

                var arr = l.Split(' ');
                foreach (var n in arr)
                {
                    retObj.Numbers.Add(long.Parse(n));
                }
            }

            return retObj;
        }
    }
}
