using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Armor : Item
    {
        public Armor(string name, int weight, int defense) : base (name, weight)
        {
            Defense = defense;
        }
        protected Armor(Armor source) : base (source)
        {
            Defense = source.Defense;
        }

        public override object Clone() 
        {
            return new Armor(this);
        }

        public override void UseOrDrop(Player player, Room room)
        {
            player.ReplaceOrDrop(this, room);
        }

        public override void EnhancePlayer(Player player)
        {
            player.Defense += Defense;
            MessageSystem.Add(MessageSystem.EventTypes.Game, $"{player.Name} +{Defense} Defense");

        }

        public override void LowerPlayer(Player player)
        {
            player.Defense -= Defense;
            MessageSystem.Add(MessageSystem.EventTypes.Game, $"{player.Name} -{Defense} Defense");         
        }

        public int Defense { get; set; }

        //public override string GetAttack() { return ""; }
        //public override string GetRegeneration() { return ""; }
        public override string GetDefense() { return Defense.ToString(); }

        public override string Description
        {
            get { return $"{Name} ({Weight} kg) with {Defense} in Defense"; }
        }
    }
}
