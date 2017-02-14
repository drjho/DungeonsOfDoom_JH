using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Consumables : Item
    {
        public Consumables(string name, int weight, int regeneration) : base(name, weight)
        {
            Regeneration = regeneration;
        }

        protected Consumables(Consumables source) : base(source) 
        {
            Regeneration = source.Regeneration;
        }

        public override object Clone()
        {
            return new Consumables(this);
        }

        public override void UseOrDrop(Player player, Room room)
        {
            MessageSystem.Add(MessageSystem.EventTypes.Player, $"You felt refreshed from the {Name}");
            player.Health += Regeneration;
            MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} +{Regeneration} HP");
            player.RemoveItem(this);
        }

        public int Regeneration { get; set; }

        //public override string GetAttack() { return ""; }
        public override string GetRegeneration() { return Regeneration.ToString(); }
        //public override string GetDefense() { return ""; }

        public override string Description
        {
            get { return $"{Name} ({Weight} kg) with {Regeneration} in Regeneration"; }
        }
    }
}
