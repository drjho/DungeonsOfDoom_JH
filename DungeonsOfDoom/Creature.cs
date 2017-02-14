using System;

namespace DoD
{
    abstract class Creature : GameObject
    {
        public enum EffectTypes
        {
            None, Poison, Freeze, Armor, Attack
        }

        public Creature(string name, int health, int maxAttack, int defense) : base(name)
        {
            Health = health;
            MaxAttack = maxAttack;
            Defense = defense;
        }

        public virtual void Fight(Creature opponent, Room room)
        {
            if (AffectedBy == EffectTypes.Freeze)
            {
                MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} cannot fight due to freeze");
                return;
            }
            if (MaxAttack > opponent.Defense)
            {
                int damage = MaxAttack - opponent.Defense;
                opponent.Health -= damage;
                MessageSystem.Add(MessageSystem.EventTypes.Battle, $"{Name} caused {damage} damage on {opponent.Name}");
            }
            else
            {
                MessageSystem.Add(MessageSystem.EventTypes.Battle, $"{Name} cannot harm {opponent.Name}");
            }
        }

        public virtual void SetCrowdControlled(Creature affector, EffectTypes effect)
        {
            if (AffectedBy != EffectTypes.None)
            {
                return;
            }
            AffectedBy = effect;
            CrowdControll.Add(this);
            EffectTurnsLeft = 2;
            switch (effect)
            {
                case EffectTypes.Poison:
                    MessageSystem.Add(MessageSystem.EventTypes.Battle, $"{affector.Name} has poisoned {Name}");
                    break;
                case EffectTypes.Freeze:
                    MessageSystem.Add(MessageSystem.EventTypes.Battle, $"{affector.Name} has frozen {Name}");
                    break;
                case EffectTypes.Armor:
                    MessageSystem.Add(MessageSystem.EventTypes.Battle, $"The Armor of {Name} is decreased");
                    Defense -= 20;
                    break;
                case EffectTypes.Attack:
                    MessageSystem.Add(MessageSystem.EventTypes.Battle, $"The Attack of {Name} is decreased");
                    MaxAttack -= 20;
                    break;
            }
        }

        public virtual void ExecuteCrowdControll()
        {

        }

        public virtual void UpdateCrowdControll()
        {
            if (Health <= 0)
            {
                EffectTurnsLeft = 0;
            }

            if (EffectTurnsLeft == 0)
            {
                if (AffectedBy == EffectTypes.Armor)
                    Defense += 20;
                AffectedBy = EffectTypes.None;
                CrowdControll.Remove(this);
            }
            else
            {
                switch (AffectedBy)
                {
                    case EffectTypes.Poison:
                        int poisonDamage = 5;
                        Health -= poisonDamage;
                        MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} loose {poisonDamage} HP/turn for {EffectTurnsLeft} more turn");
                        break;
                    case EffectTypes.Freeze:
                        MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} is frozen for {EffectTurnsLeft} more turn");
                        break;
                    case EffectTypes.Armor:
                        MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} has decreased armor for {EffectTurnsLeft} more turn");
                        Defense -= 20;
                        break;
                    case EffectTypes.Attack:
                        MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} has decreased attack for {EffectTurnsLeft} more turn");
                        MaxAttack -= 20;
                        break;
                }
            }
            EffectTurnsLeft--;
        }

        public int EffectTurnsLeft { get; set; } = 0;
        public EffectTypes AffectedBy { get; set; } = EffectTypes.None;
        public int MaxAttack { get; set; }

        private int defense;
        public int Defense { get { return Math.Max(0, defense); } set { defense = value; } }
        public int Health { get; set; }

        public virtual string Description
        {
            get { return $"{Name} with {Health} HP, {MaxAttack} Damage and {Defense} Armor"; }
        }



    }

    
}
