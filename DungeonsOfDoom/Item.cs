using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Item : GameObject, IPackable
    {
        public Item(string name, int weight) : base(name)
        {
            Weight = weight;
 
        }

        protected Item(Item source) : base(source) // Nu kan Item anropa konstruktorn i basklassen.
        {
            Weight = source.Weight;
        }

        public virtual object Clone()
        {
            return new Item(this);
        }

        public virtual void UseOrDrop(Player player, Room room)
        {
        }

        public virtual void EnhancePlayer(Player player)
        {

        }

        public virtual void LowerPlayer(Player player)
        {

        }

        public int Weight { get; set; }

        public virtual string Description 
        {
            get { return $"{Name} ({Weight} kg)"; }
        }

        public virtual string GetAttack() { return ""; }
        public virtual string GetRegeneration() { return ""; }
        public virtual string GetDefense() { return ""; }
    }
}
