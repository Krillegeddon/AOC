using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day17
{
    public class Model
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public List<int> Program { get; set; }
        public int PP { get; set; }

        public List<int> Output { get; set; }

        public static Model Parse()
        {
            var retObj = new Model();

            foreach (var lx in Data.InputData.Split("\n"))
            {
                var l = lx.Trim();

                if (string.IsNullOrEmpty(l))
                    continue;

                if (l.StartsWith("Register"))
                {
                    var arr = l.Split(":");
                    var val = int.Parse(arr[1].Trim());
                    var regId = arr[0].Replace("Register ", "");
                    if (regId == "A") retObj.A = val;
                    if (regId == "B") retObj.B = val;
                    if (regId == "C") retObj.C = val;
                }
                else
                {
                    retObj.Program = l.Split(":")[1].Split(",").Select(p => int.Parse(p)).ToList();
                }
            }
            retObj.Output = new List<int>();

            return retObj;
        }
    }
}
