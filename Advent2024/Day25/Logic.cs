namespace Advent2024.Day25;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private bool DoesFit(List<int> list1, List<int> list2)
    {
        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] + list2[i] > 5)
                return false;
        }

        return true;
    }

    public string Run()
    {
        long sum = 0;

        for (int l = 0; l < _model.Locks.Count; l++)
        {
            for (int k = 0; k < _model.Keys.Count; k++)
            {
                if (DoesFit(_model.Locks[l], _model.Keys[k]))
                    sum++;
            }
        }

        return sum.ToString();
    }
}
