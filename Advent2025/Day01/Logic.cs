using static Advent2025.Day01.Model;

namespace Advent2025.Day01;

public class Logic
{
    private Model _model;
    public Logic(Model model)
    {
        _model = model;
    }

    private static int TurnDial(ref int position, Turn turn)
    {
        var delta = 1;
        if (turn.Direction == 'L')
            delta = -1;

        var numZeroes = 0;

        for (var i = 0; i < turn.Distance; i++)
        {
            position += delta;
            if (position < 0)
                position = 99;
            if (position > 99)
                position = 0;

            if (position == 0)
            {
                numZeroes++;
            }
        }

        return numZeroes;
    }


    public string Run()
    {
        var dial = 50;

        long sum = 0;

        foreach (var turn in _model.Turns)
        {
            var numZeroes = TurnDial(ref dial, turn);
            sum += numZeroes;
        }

        return sum.ToString();
    }
}
