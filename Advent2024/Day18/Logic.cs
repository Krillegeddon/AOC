using AdventUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day18
{
    public record Step
    {
        public Coord Coord { get; set; }
        public Coord Direction { get; set; }
        public long Points { get; set; }
        public bool IsGoal { get; set; }
        public List<Step> Prevs { get; set; }

        public string GetKey()
        {
            return Coord.ToString() + "_" + Direction.ToString();
        }
    }

    public class Logic
    {
        private Grid _grid;
        public Logic(Model model)
        {
            _grid = model.Grid;
        }

        private Dictionary<string, Step> _doneSteps = new Dictionary<string, Step>();

        private Dictionary<long, Dictionary<string, Step>> _queue = new Dictionary<long, Dictionary<string, Step>>();

        private void AddStep(Step step, int level)
        {
            if (step.IsGoal && step.Points == 98416)
            {
                int bb = 9;
            }

            if (step.Coord.X == 15 && step.Coord.Y == 6)
            {
                int bb = 9;
            }

            var skey = step.GetKey();
            if (_doneSteps.ContainsKey(skey))
            {
                var doneStep = _doneSteps[skey];

                // If this point is higher than seen before, then just return...
                if (step.Points > doneStep.Points)
                    return;

                if (step.Points < doneStep.Points)
                {
                    doneStep.Prevs.Clear();
                }
                doneStep.Prevs.AddRange(step.Prevs);

                if (step.IsGoal)
                {
                    if (step.Points < _minPoints)
                        _minPoints = step.Points;
                }

                doneStep.Points = step.Points;
            }
            else
            {
                _doneSteps.Add(step.GetKey(), step);
                if (step.IsGoal)
                {
                    if (step.Points < _minPoints)
                        _minPoints = step.Points;
                }
            }
            var queue = _queue[level + 1];
            if (queue.ContainsKey(step.GetKey()))
            {
                var aa = queue[step.GetKey()];
                if (step.Points < aa.Points)
                {
                    queue[step.GetKey()] = step;
                }
            }
            else
            {
                queue.Add(step.GetKey(), step);
            }

        }

        private long _minPoints = long.MaxValue;

        private void AddNext(Step step, int level)
        {
            if (step.IsGoal)
                return;

            var dir1 = step.Direction.RotateClockwise();
            var dir2 = step.Direction.RotateCounterClockwise();

            var coord0 = step.Coord.Add(step.Direction);
            var coord1 = step.Coord.Add(dir1);
            var coord2 = step.Coord.Add(dir2);

            if (_grid.GetValue(coord0) != "#" && _grid.IsInside(coord0))
            {
                AddStep(new Step()
                {
                    Coord = coord0,
                    Direction = step.Direction,
                    Points = step.Points + 1,
                    Prevs = new List<Step> { step },
                    IsGoal = coord0 == Coord.Create(_grid.Width-1, _grid.Height-1)
                }, level);
            }
            if (_grid.GetValue(coord1) != "#" && _grid.IsInside(coord1))
            {
                AddStep(new Step()
                {
                    Coord = coord1,
                    Direction = dir1,
                    Points = step.Points + 1,
                    Prevs = new List<Step> { step },
                    IsGoal = coord0 == Coord.Create(_grid.Width - 1, _grid.Height - 1)
                }, level);
            }
            if (_grid.GetValue(coord2) != "#" && _grid.IsInside(coord2))
            {
                AddStep(new Step()
                {
                    Coord = coord2,
                    Direction = dir2,
                    Points = step.Points + 1,
                    Prevs = new List<Step> { step },
                    IsGoal = coord0 == Coord.Create(_grid.Width - 1, _grid.Height - 1)
                }, level);
            }

        }

        private void CalcDist(int level)
        {
            _queue.Add(level + 1, new Dictionary<string, Step>());
            foreach (var s in _queue[level])
            {
                AddNext(s.Value, level);
            }

        }

        public string Run()
        {
            long sum = 0;

            var startStep = new Step()
            {
                Coord = Coord.Create(0,0),
                Direction = Coord.Create(1,0),
                Points = 0
            };

            _queue.Add(0, new Dictionary<string, Step>());
            _queue[0].Add(startStep.GetKey(), startStep);

            for (int i = 0; true; i++)
            {
                CalcDist(i);
                var newQueue = _queue[i + 1];
                if (newQueue.Count == 0)
                    break;
            }

            //var ends = _doneSteps.Where(p => p.Value.IsGoal && p.Value.Points == _minPoints).ToList();
            ////var ends = _doneSteps.Where(p => p.Value.IsGoal).ToList();
            //foreach (var end in ends)
            //{
            //    TraverseBack(end.Value);
            //}

            var xx = long.MaxValue;

            PrintGrid();
            var ss = _minPoints;
            return sum.ToString();
        }



        public void PrintGrid(bool waitForEnter = false)
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                var line = "";
                for (int x = 0; x < _grid.Width; x++)
                {
                    var coord = Coord.Create(x, y);
                    if (coord.X == 5 && coord.Y == 13)
                    {
                        line += "$";
                        continue;
                    }

                    if (_grid._grid.ContainsKey(coord))
                    {
                        line += _grid._grid[coord];
                    }
                    else
                        line += " ";
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
}
