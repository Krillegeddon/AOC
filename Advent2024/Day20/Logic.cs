using AdventUtils;

namespace Advent2024.Day20;

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

    private Dictionary<Coord, Step> _doneSteps = new Dictionary<Coord, Step>();
    private Dictionary<Coord, Step> _shortcutSteps = new Dictionary<Coord, Step>();

    private Dictionary<long, Dictionary<string, Step>> _queue = new Dictionary<long, Dictionary<string, Step>>();

    private void AddStep(Step step, int level)
    {
        if (step.IsGoal)
        {
            int bb = 9;
        }

        if (step.Coord.X == 15 && step.Coord.Y == 6)
        {
            int bb = 9;
        }

        var skey = step.GetKey();
        if (_doneSteps.ContainsKey(step.Coord))
        {
            var doneStep = _doneSteps[step.Coord];

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
            _doneSteps.Add(step.Coord, step);
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
    private List<long> _cheatSteps = new List<long>();

    private List<Coord> GetCheatCoords(Coord coord)
    {
        var retList = new List<Coord>();

        var dir = Coord.Create(1, 0);
        for (int i = 0; i < 4; i++)
        {
            dir = dir.RotateClockwise();
            var c1 = coord.Add(dir);
            var c2 = coord.Add(Coord.Create(dir.X * 2, dir.Y * 2));
            var c3 = coord.Add(Coord.Create(dir.X * 3, dir.Y * 3));

            // C2 and C3 are eligible to add... but only if c1 is #
            if (!_grid.IsInside(c1) || _grid.GetValue(c1) != "#")
                continue;
            // c2 can be added if it is not #
            if (_grid.IsInside(c2) && _grid.GetValue(c2) != "#")
                retList.Add(c2);
            else
                retList.Add(c3);
        }

        return retList;
    }


    private void AddNext(Step step, int level, Coord goalCoord, bool runningTowardsGoal)
    {
        if (step.IsGoal)
            return;

        if (runningTowardsGoal)
        {
            if (step.Coord == Coord.Create(7, 7))
            {
                int bb = 0;
            }

            var cheatCoords = GetCheatCoords(step.Coord);

            foreach (var cc in cheatCoords)
            {
                if (!_shortcutSteps.ContainsKey(cc))
                    continue;

                var dist = step.Coord.Subtract(cc);
                var absDist = Math.Max(Math.Abs(dist.X), Math.Abs(dist.Y));

                var ss = _shortcutSteps[cc];

                var cheatSteps = step.Points + ss.Points + absDist;
                _cheatSteps.Add(cheatSteps);

                if (step.Points + ss.Points + absDist < _minPoints)
                {
                    // Yeah, this was not what the question was about! :-)
                    _minPoints = step.Points + ss.Points + absDist;
                }
            }
        }

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
                IsGoal = coord0 == goalCoord
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
                IsGoal = coord0 == goalCoord
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
                IsGoal = coord0 == goalCoord,
            }, level);
        }

    }

    private void CalcDist(int level, Coord goalCoord, bool runningTowardsGoal)
    {
        _queue.Add(level + 1, new Dictionary<string, Step>());
        foreach (var s in _queue[level])
        {
            AddNext(s.Value, level, goalCoord, runningTowardsGoal);
        }
    }

    private Dictionary<Coord, int> _visited = new Dictionary<Coord, int>();

    private void TraverseBack(Step s)
    {
        if (s.Coord.X == 15 && s.Coord.Y == 5)
        {
            int bb = 9;
        }
        if (!_visited.ContainsKey(s.Coord))
            _visited.Add(s.Coord, 1);
        else
            _visited[s.Coord] += 1;
        if (s.Prevs == null)
            return;
        var min = s.Prevs.Min(p => p.Points);
        //var mins = s.Prevs.Where(r => r.Points == min).ToList();
        var mins = s.Prevs.ToList();
        foreach (var p in mins)
        {
            TraverseBack(p);
        }
    }

    public string Run()
    {
        long sum = 0;

        // Run search from EndPostion to calculate every coord and it's quickest path back
        // to end. Note, direction must be set based on puzzle input!
        var startStep = new Step()
        {
            Coord = _grid.EndPosition,
            Direction = Coord.Create(0, 1),
            //Direction = Coord.Create(-1, 0),
            Points = 0
        };
        _doneSteps.Add(_grid.EndPosition, startStep);

        _queue.Add(0, new Dictionary<string, Step>());
        _queue[0].Add(startStep.GetKey(), startStep);

        for (int i = 0; true; i++)
        {
            CalcDist(i, _grid.StartPosition, false);
            var newQueue = _queue[i + 1];
            if (newQueue.Count == 0)
                break;
        }

        PrintGrid();

        // Start from scratch and run the solution from Start to End. Cheat logic will check every step to see
        // if a quicker path can be found. All shortcut steps (number of steps to End) is stored in _shortcutSteps.

        _shortcutSteps = _doneSteps;
        _doneSteps = new Dictionary<Coord, Step>();

        var rawMinPoints = _minPoints;
        _minPoints = long.MaxValue;

        startStep = new Step()
        {
            Coord = _grid.StartPosition,
            Direction = Coord.Create(1, 0),
            //Direction = Coord.Create(-1, 0),
            Points = 0
        };
        _doneSteps.Add(_grid.StartPosition, startStep);

        _queue = new Dictionary<long, Dictionary<string, Step>>();
        _queue.Add(0, new Dictionary<string, Step>());
        _queue[0].Add(startStep.GetKey(), startStep);

        for (int i = 0; true; i++)
        {
            CalcDist(i, _grid.EndPosition, true);
            var newQueue = _queue[i + 1];
            if (newQueue.Count == 0)
                break;
        }

        var xx = long.MaxValue;

        PrintGrid();

        //minPoints = 9456
        var ss = _minPoints;

        var part1 = _cheatSteps.Where(p => (p <= rawMinPoints - 100)).ToList().Count;

        // 14 + 14 + 2 + 4 + 2 + 3 + 5 = 44

        return part1.ToString();
    }



    public void PrintGrid(bool waitForEnter = false)
    {
        for (int y = 0; y < _grid.Height; y++)
        {
            var line = "";
            for (int x = 0; x < _grid.Width; x++)
            {
                var coord = Coord.Create(x, y);

                if (coord == _grid.StartPosition)
                {
                    line += "S";
                    continue;
                }
                else if (coord == _grid.EndPosition)
                {
                    line += "E";
                    continue;
                }

                if (_doneSteps.ContainsKey(coord))
                {
                    line += "O";
                    //if (_doneSteps[coord].IsCheat)
                    //    line += "c";
                    //else
                    //    line += "o";
                }
                else
                {
                    if (_grid._grid.ContainsKey(coord))
                    {
                        line += _grid._grid[coord];
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
