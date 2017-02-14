using System;

namespace DoD
{
    class Demon : Monster, IMovable 
    {
        public Demon(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense, 'D')
        {

        }

        public override void Die(Room room)
        {
            room.RoomMonster = null;
        }

        public void Move(Player player, Room[,] rooms)
        {
            if (MathUtils.Distance(this, player) > 0)
            {
                rooms[X, Y].RoomMonster = null; //Demonen ska röra sig, rummets pekare på monster sätts till null.
                int newX = X;
                int newY = Y;

                if (player.X > X)
                    newX++;
                else if (player.X < X)
                    newX--;

                if (player.Y > Y)
                    newY++;
                else if (player.Y < Y)
                    newY--;

                if (Math.Abs(player.X - X) > Math.Abs(player.Y - Y))
                {
                    if (rooms[newX, Y].RoomMonster == null)
                        X = newX;
                    else if (rooms[X, newY].RoomMonster == null)
                        Y = newY;
                }
                else
                {
                    if (rooms[X, newY].RoomMonster == null)
                        Y = newY;
                    else if (rooms[newX, Y].RoomMonster == null)
                        X = newX;
                }
                rooms[X, Y].RoomMonster = this; //Sätta tillbaka demonen i rummet.
            }
        }

        public override void ExecuteCrowdControll()
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
            }
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}