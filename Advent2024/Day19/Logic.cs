using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2024.Day19
{
    public class Logic
    {
        private Model _model;
        public Logic(Model model)
        {
            _model = model;
        }

        private Dictionary<string, long> _cache = new Dictionary<string, long>();

        private void VerifyCache(string design, long num)
        {
            if (!_cache.ContainsKey(design))
                _cache.Add(design, num);
        }

        private long NumComputes(string design)
        {
            // If we have already calculated this design (or remainder of design), just
            // return from cache.
            if (_cache.ContainsKey(design))
                return _cache[design];

            long numComputes = 0;
            foreach (var towel in _model.Towels)
            {
                // Check if one towel matches the end of the design
                if (design == towel)
                {
                    numComputes++;
                    continue;
                }

                // If the towel is longer than the rest of the design, no need to check if it matches
                if (towel.Length > design.Length)
                    continue;

                // Recursively check remainder of the design, given that first part is the towel.
                if (design.Substring(0, towel.Length) == towel)
                {
                    var subNumComputes = NumComputes(design.Substring(towel.Length));
                    numComputes += subNumComputes;
                }
            }

            // Save in cache
            VerifyCache(design, numComputes);
            return numComputes;
        }

        public string Run()
        {
            long sum = 0;
            int num = 0;

            foreach (var design in _model.Designs)
            {
                var subNum = NumComputes(design);
                if (subNum > 0)
                    num++;
                sum += subNum;
            }

            // num = result of part1
            // sum = result of part2

            return sum.ToString();
        }
    }
}
