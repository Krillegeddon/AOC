namespace Advent2024.Day22;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private static long Mix(long a, long b)
    {
        return a ^ b;
    }

    private static long Prune(long value)
    {
        return value % 16777216;
    }

    private static long GetNext(long value)
    {
        var step1 = Prune(Mix(value * 64, value));
        var step2 = Prune(Mix(step1 / 32, step1));
        var step3 = Prune(Mix(step2 * 2048, step2));
        return step3;
    }

    private static long CalcNumTimes(long value, int numtimes)
    {
        var num = value;
        for (int i = 0; i < numtimes; i++)
        {
            num = GetNext(num);
        }
        return num;
    }

    public string Run()
    {
        long sum = 0;

        foreach (var num in _model.Numbers)
        {
            sum += CalcNumTimes(num, 2000);
        }



        return sum.ToString();
    }
}
