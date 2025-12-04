namespace Advent2025.Day03;

public class Model
{
    public class BatteryBank
    {
        public List<int> Jolts { get; set; }
    }

    public required List<BatteryBank> BatteryBanks { get; set; }

    public static Model Parse()
    {
        var retObj = new Model
        {
            BatteryBanks = new List<BatteryBank>()
        };

        foreach (var lx in Data.InputData.Split("\n"))
        {
            var l = lx.Trim();

            if (string.IsNullOrEmpty(l))
                continue;

            var chars = l.ToCharArray();
            var batteryBank = new BatteryBank
            {
                Jolts = chars.Select(c => int.Parse(c.ToString())).ToList()
            };
            retObj.BatteryBanks.Add(batteryBank);
        }

        return retObj;
    }
}
