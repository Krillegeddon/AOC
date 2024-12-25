namespace Advent2024.Day25;


public class Model
{
    public required List<List<int>> Locks { get; set; }
    public required List<List<int>> Keys { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            Locks = new List<List<int>>(),
            Keys = new List<List<int>>()
        };

        int y = 0;
        var isKey = false;
        var currentPart = new List<int> { 10, 10, 10, 10, 10 };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
            {
                if (isKey)
                {
                    for (int i = 0; i < currentPart.Count; i++)
                        currentPart[i] = 6 - currentPart[i];
                    retObj.Keys.Add(currentPart.ToList());
                }
                else
                {
                    for (int i = 0; i < currentPart.Count; i++)
                        currentPart[i] = currentPart[i] - 1;
                    retObj.Locks.Add(currentPart.ToList());
                }
                currentPart = new List<int> { 10, 10, 10, 10, 10 };
                y = 0;
                continue;
            }

            if (y == 0)
            {
                if (l == "#####")
                    isKey = false; // It's a lock
                else
                    isKey = true;
                y++;
                continue;
            }

            var arr = l.ToCharArray().ToList();
            var charToLookFor = ".";
            if (isKey)
                charToLookFor = "#";
            for (int i = 0; i<arr.Count; i++)
            {
                if (arr[i].ToString() == charToLookFor)
                {
                    if (y < currentPart[i])
                        currentPart[i] = y;
                }
            }

            y++;
        }

        if (isKey)
        {
            for (int i = 0; i < currentPart.Count; i++)
                currentPart[i] = 6 - currentPart[i];
            retObj.Keys.Add(currentPart.ToList());
        }
        else
        {
            for (int i = 0; i < currentPart.Count; i++)
                currentPart[i] = currentPart[i] - 1;
            retObj.Locks.Add(currentPart.ToList());
        }

        return retObj;
    }
}
