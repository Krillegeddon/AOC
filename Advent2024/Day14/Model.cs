using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day14
{
    public class Robot
    {
        public Coord Coord {get; set;}
        public Coord Speed {get; set;} 
    }

    public class Model
    {
        private static Robot ParseRobot(string l)
        {
            var arr1 = l.Split(" ");
            var arr11 = arr1[0].Split(",");
            var arr12 = arr1[1].Split(",");
            var px = arr11[0].Replace("p=", "");
            var py = arr11[1];
            var sx = arr12[0].Replace("v=", "");
            var sy = arr12[1];


            return new Robot
            {
                Coord = Coord.Create(int.Parse(px), int.Parse(py)),
                Speed = Coord.Create(int.Parse(sx), int.Parse(sy)),
            };
        }


        public required List<Robot> Robots { get; set; }
        public long Height { get; set; }
        public long Width { get; set; }

        public static Model Parse()
        {
            var retObj = new Model
            {
                Robots = new List<Robot>()
            };

            foreach (var lx in Data.InputData.Split("\n"))
            {
                var l = lx.Trim();

                if (string.IsNullOrEmpty(l))
                    continue;

                var robot = ParseRobot(l);

                //if (robot.Coord.X == 2 && robot.Coord.Y == 4)
                    retObj.Robots.Add(robot);

                if (robot.Coord.X + 1 > retObj.Width)
                    retObj.Width = robot.Coord.X + 1;
                if (robot.Coord.Y + 1 > retObj.Height)
                    retObj.Height = robot.Coord.Y + 1;
            }

            return retObj;
        }
    }
}
