using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    static class MathUtils
    {
        public static double Distance(IMovable c1, IMovable c2) // Här använder vi interface IMovable istället för MovingCreature.
        {
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
        }

    }
}
