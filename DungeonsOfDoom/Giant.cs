using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Giant : Monster
    {
        public Giant(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense, 'G')
        {
            MonsterItem = new Armor("*Iron Armor*", 4, 8);

        }

        public override void Fight(Creature opponent, Room room)
        {
            base.Fight(opponent, room);
            if (RandomUtils.Try(25))
            {
                MessageSystem.Add(MessageSystem.EventTypes.Player, $"{Name} mades {opponent.Name} to tremble");
                opponent.SetCrowdControlled(this, EffectTypes.Attack);
            }
        }
    }
}
