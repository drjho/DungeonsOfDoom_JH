using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Troll : Monster, IPackable
    {
        public Troll(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense, 'T')
        {

        }

        public void UseOrDrop(Player player, Room room)
        {
            if (room.RoomMonster == null)
            {
                MessageSystem.Add(MessageSystem.EventTypes.Game, $"There is no one else in here.");
                return;
            }

            MessageSystem.Add(MessageSystem.EventTypes.Player, $"You throw the head and infected the {room.RoomMonster.Name}");
            this.Fight(room.RoomMonster, room);
            player.RemoveItem(this);
        }

        public void EnhancePlayer(Player player)
        {

        }

        public void LowerPlayer(Player player)
        {

        }

        public override void Die(Room room)
        {
            MonsterItem = this;
            Name = "*Infected Trollhead*";
            MaxAttack = 50;
            base.Die(room);
        }

        string IPackable.Description { get { return $"{Name} inflict 50 damage when used"; } }

        public int Weight { get; set; } = 5;

        public string GetAttack() { return $"{MaxAttack.ToString()}*"; }
        public string GetRegeneration() { return ""; }
        public string GetDefense() { return ""; }
    }
}
