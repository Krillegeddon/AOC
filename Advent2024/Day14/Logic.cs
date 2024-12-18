using AdventUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day14
{
    public class Logic
    {
        private Model _model;
        public Logic(Model model)
        {
            _model = model;
        }

        private void Move(List<Robot> robots, long width, long height)
        {
            foreach (var robot in robots)
            {
                var x = robot.Coord.X + robot.Speed.X;
                var y = robot.Coord.Y + robot.Speed.Y;

                if (x < 0)
                    x += width;
                if (x >= width)
                    x -= width;
                if (y < 0)
                    y += height;
                if (y >= height)
                    y -= height;
                robot.Coord = Coord.Create(x, y);
            }
        }

        private int _maxVisibleRobots = 0;

        public void PrintRobots(Model model, int i)
        {
            var dict = new Dictionary<Coord, bool>();

            foreach (var r in model.Robots)
            {
                if (!dict.ContainsKey(r.Coord))
                    dict.Add(r.Coord, true);
            }

            if (dict.Count > _maxVisibleRobots)
            {
                _maxVisibleRobots = dict.Count;
            }
            else
            {
                return;
            }

            for (int x = 0; x < model.Width; x++)
            {
                var line = "";
                for (int y = 0; y < model.Height; y++)
                {
                    if (dict.ContainsKey(Coord.Create(x, y)))
                        line += "X";
                    else
                        line += " ";
                }
                Console.WriteLine(line);
            }
            Console.WriteLine(i + "******************************************************************************************");
            Console.WriteLine("******************************************************************************************");
            Console.ReadLine();
        }



        public string Run()
        {
            long sum = 0;
            int i = 0;
            while (true)
            {
                Move(_model.Robots, _model.Width, _model.Height);
                i++;
                PrintRobots(_model, i);
            }

            int numQ1 = 0, numQ2 = 0, numQ3 = 0, numQ4 = 0;

            var midX = _model.Width / 2;
            var midY = _model.Height / 2;

            foreach (var robot in _model.Robots)
            {
                if (robot.Coord.X < midX && robot.Coord.Y < midY) numQ1++;
                if (robot.Coord.X > midX && robot.Coord.Y < midY) numQ2++;
                if (robot.Coord.X < midX && robot.Coord.Y > midY) numQ3++;
                if (robot.Coord.X > midX && robot.Coord.Y > midY) numQ4++;
            }

            sum = numQ1 * numQ2 * numQ3 * numQ4;

            return sum.ToString();
        }
    }
}
