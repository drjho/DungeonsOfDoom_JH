using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Skeleton : Monster
    {
        public Skeleton(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense, 'S')
        {
            MonsterItem = new Weapon("*Bone Sword*", 3, 10);
        }

        public override void Fight(Creature opponent, Room room)
        {
            if (room.Brightness < 70)
            {
                base.Fight(opponent, room);
                if (RandomUtils.Try(50))
                {
                    opponent.SetCrowdControlled(this, EffectTypes.Freeze);
                }
            }
            else
            {
                MessageSystem.Add(MessageSystem.EventTypes.Battle, $"{Name} cannot fight due to light");
            }
        }
    }
}
