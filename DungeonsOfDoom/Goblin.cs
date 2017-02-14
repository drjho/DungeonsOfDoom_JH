using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Goblin : Monster, IPackable, IMovable
    {
        public Goblin(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense, '@')
        {

        }

        public void Move(Player player, Room[,] rooms)
        {
            if (AffectedBy == EffectTypes.Freeze)
            {
                MessageSystem.Add(MessageSystem.EventTypes.Game, $"{Name} can't move due to freeze");
                return;
            }
            if (MathUtils.Distance(player, this) > 2)
                return;

            rooms[X, Y].RoomMonster = null;
            int newX = X;
            int newY = Y;

            if (RandomUtils.Try(50))
            {
                if (player.X > X)
                {
                    if (newX > 0)
                        newX--;
                }
                else if (player.X < X)
                {
                    if (newX < rooms.GetLength(0) - 1)
                        newX++;
                }
            }
            else
            {
                if (player.Y > Y)
                {
                    if (newY > 0)
                        newY--;
                }
                else if (player.Y < Y)
                {
                    if (newY < rooms.GetLength(1) - 1)
                        newY++;
                }
            }
            if (rooms[newX, newY].IsEmpty)
            {
                X = newX;
                Y = newY;
            }
            rooms[X, Y].RoomMonster = this;
        }

        public override void Fight(Creature opponent, Room room)
        {
            base.Fight(opponent, room);
            if (RandomUtils.Try(75))
            {
                opponent.SetCrowdControlled(this, EffectTypes.Poison);
            }
        }

        public override void Die(Room room)
        {
            MonsterItem = this;
            Name = "*Ice Cold Goblin*";
            base.Die(room);
        }

        public void UseOrDrop(Player player, Room room)
        {
            if (room.RoomMonster == null)
            {
                MessageSystem.Add(MessageSystem.EventTypes.Game, $"There is no one else in here.");
                return;
            }

            MessageSystem.Add(MessageSystem.EventTypes.Player, $"You throw {Name} and hope to freeze {room.RoomMonster.Name}");
            if (RandomUtils.Try(50))
            {
                room.RoomMonster.SetCrowdControlled(player, EffectTypes.Freeze);
            }
            else
            {
                MessageSystem.Add(MessageSystem.EventTypes.Game, $"Unfortunately, you missed {room.RoomMonster.Name}");
            }
            player.RemoveItem(this);
        }

        public void EnhancePlayer(Player player)
        {

        }

        public void LowerPlayer(Player player)
        {

        }

        string IPackable.Description { get { return $"{Name} can freeze opponent when used"; } }

        public int Weight { get; set; } = 5;

        public string GetAttack() { return "F*"; }
        public string GetRegeneration() { return ""; }
        public string GetDefense() { return ""; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
