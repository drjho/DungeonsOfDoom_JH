using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    abstract class Monster : Creature
    {
        public Monster(string name, int health, int maxAttack, int defense, char symbol) : base(name, health, maxAttack, defense)
        {
            Symbol = symbol;
            MonsterItem = null;
        }

        public virtual void Die(Room room)
        {
            if (MonsterItem != null)
            {
                if (RandomUtils.RandomStat(100 + 1) < 50) // 50% chans att Monstret droppa sitt item som är lite bättre.
                {
                    MessageSystem.Add(MessageSystem.EventTypes.Game, $"{MonsterItem.Name} dropped");
                    room.PlaceItem = room.RoomMonster.MonsterItem;
                }
            }
            room.RoomMonster = null;
        }

        public char Symbol { get; set; }
        public IPackable MonsterItem { get; set; }
    }
}
