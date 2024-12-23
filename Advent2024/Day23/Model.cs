namespace Advent2024.Day23;

public class Model
{
    public required Dictionary<string, Dictionary<string, bool>> ConnectionsDict { get; set; }

    private void VerifyDictOne(string name1, string name2)
    {
        if (!ConnectionsDict.ContainsKey(name1))
        {
            ConnectionsDict.Add(name1, new Dictionary<string, bool>());
        }
        var subDict = ConnectionsDict[name1];
        if (!subDict.ContainsKey(name2))
        {
            subDict.Add(name2, true);
        }
    }

    public void VerifyDict(string name1, string name2)
    {
        VerifyDictOne(name1, name2);
        VerifyDictOne(name2, name1);
    }

    public static Model Parse()
    {
        var retObj = new Model
        {
            ConnectionsDict = new Dictionary<string, Dictionary<string, bool>>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var arr = l.Split("-");
            retObj.VerifyDict(arr[0], arr[1]);
        }

        return retObj;
    }
}
