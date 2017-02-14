using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Weapon : Item
    {
        public Weapon(string name, int weight, int attack) : base(name, weight)
        {
            Attack = attack;
        }

        protected Weapon(Weapon source) : base (source)
        {
            Attack = source.Attack;
        }

        public override object Clone() 
        {
            return new Weapon(this);
        }

        public override void UseOrDrop(Player player, Room room)
        {
            player.ReplaceOrDrop(this, room);
        }

        public override void EnhancePlayer(Player player)
        {
            player.MaxAttack += Attack;
            MessageSystem.Add(MessageSystem.EventTypes.Game, $"{player.Name} +{Attack} Attack");
        }

        public override void LowerPlayer(Player player)
        {
            player.MaxAttack -= Attack;
            MessageSystem.Add(MessageSystem.EventTypes.Game, $"{player.Name} -{Attack} Attack");
        }

        public int Attack { get; set; }
        public override string GetAttack() { return Attack.ToString(); }
        //public override string GetRegeneration() { return ""; }
        //public override string GetDefense() { return ""; }

        public override string Description
        {
            get { return $"{Name} ({Weight} kg) with {Attack} in Attack"; }
        }
    }
}
