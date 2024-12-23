using System;

namespace Advent2024.Day23;

public class Logic
{
    private Dictionary<string, Dictionary<string, bool>> _connectionsDict;
    public Logic(Model model)
    {
        _connectionsDict = model.ConnectionsDict;
    }

    private Dictionary<string, bool> _foundTriplets = new Dictionary<string, bool>();

    private static string GetKey(List<string> path)
    {
        var sorted = path.OrderBy(p => p).ToList();
        return string.Join(",", sorted);
    }

    private void SavePath(List<string> path)
    {
        var key = GetKey(path);
        if (!_foundTriplets.ContainsKey(key))
        {
            _foundTriplets.Add(key, true);
        }
    }

    private bool AreConnected(List<string> pathNames)
    {
        for (int index0 = 0; index0 < pathNames.Count; index0++)
        {
            for (int index1 = 0; index1 < pathNames.Count; index1++)
            {
                if (index1 == index0) continue;

                for (int index2 = 0; index2 < pathNames.Count; index2++)
                {
                    if (index2 == index0 || index2 == index1) continue;
                    if (!_connectionsDict[pathNames[index0]].ContainsKey(pathNames[index1]))
                        return false;
                    if (!_connectionsDict[pathNames[index0]].ContainsKey(pathNames[index2]))
                        return false;
                }
            }
        }

        return true;
    }

    private void CalcNum(string computerName, List<string> pathNames, int numComputers)
    {
        if (!AreConnected(pathNames))
            return;

        if (pathNames.Contains(computerName))
        {
            if (numComputers == 3)
            {
                if (!pathNames.Where(p => p.StartsWith("t")).Any())
                    return;

                SavePath(pathNames);
            }

            return;
        }

        if (numComputers >= 3)
            return;

        var connections = _connectionsDict[computerName];

        foreach (var conn in connections)
        {
            var tempPath = pathNames.ToList();
            tempPath.Add(computerName);
            CalcNum(conn.Key, tempPath, numComputers + 1);
        }

        return;
    }

    private List<string> GetTestPath(string rootName, List<string> children, int index)
    {
        var retList = new List<string> { rootName };

        for (int i = 0; i < children.Count; i++)
        {
            if (i == index)
                continue;
            retList.Add(children[i]);
        }
        return retList;
    }

    public string Run()
    {
        foreach (var kvp in _connectionsDict)
        {
            CalcNum(kvp.Key, new List<string>(), 0);
        }
        var part1 = _foundTriplets.Count;
        Console.WriteLine("Part1: " + part1);

        var part2 = "";
        foreach (var kvp in _connectionsDict)
        {
            var children = kvp.Value.Keys.ToList();
            for (int i = 0; i < children.Count; i++)
            {
                var path = GetTestPath(kvp.Key, children, i);
                if (AreConnected(path))
                {
                    part2 = GetKey(path);
                    Console.WriteLine("Part2: " + part2);
                    return "--";
                }
            }
        }

        return "exit";
    }
}
