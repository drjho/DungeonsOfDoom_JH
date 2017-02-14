using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    static class RandomUtils
    {
        static Random random = new Random();


        static public int RandomStat(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        static public int RandomStat(int value)
        {
            return RandomStat(1, value);
        }

        static public bool Try(int precentage)
        {       
         return RandomStat(101) < precentage;
        }
    }
}
