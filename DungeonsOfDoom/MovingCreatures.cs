using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    abstract class MovingCreatures : Creature
    {
        public MovingCreatures(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense)
        {

        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
