using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Player : Creature, IMovable
    {
        public List<IPackable> BackPack { get; private set; } = new List<IPackable>();

        public Player(string name, int health, int maxAttack, int defense) : base(name, health, maxAttack, defense)
        {
            if (name.Length < 2)
                throw new ArgumentException("Vi vill gärna att du skapar ett namn som är längre än 1 bokstav.");

            Weight = 0;
        }

        public override void Fight(Creature opponent, Room room)
        {
            base.Fight(opponent, room);
            if (RandomUtils.Try(25))
            {
                MessageSystem.Add(MessageSystem.EventTypes.Player, $"You righteousness has weakened your foe");
                opponent.SetCrowdControlled(this, EffectTypes.Armor);
            }
        }

        public void Cheat()
        {
            PickItem(new Armor("*Gods Aura*", 0, 100));
            //PickItem(new Weapon("*Gods Breath*", 0, 100));
            PickItem(new Weapon("*The Knife*", 0, 5));
            MessageSystem.Add(MessageSystem.EventTypes.Game, $"Ahh you cheeky monkey!!");
        }

        public void ReplaceOrDrop(IPackable itemToDrop, Room room)
        {
            if (room.PlaceItem != null)
            {
                IPackable roomItem = room.PlaceItem;
                int newWeight = Weight - itemToDrop.Weight + roomItem.Weight;
                if (newWeight <= MaxWeight)
                {
                    MessageSystem.Add(MessageSystem.EventTypes.Player, $"You replaced {itemToDrop.Name} with {roomItem.Name}");
                    RemoveItem(itemToDrop);
                    PickItem(room.PlaceItem);
                    room.PlaceItem = itemToDrop;
                }
                else
                {
                    MessageSystem.Add(MessageSystem.EventTypes.Error, $"{roomItem.Name} is too heavy, cannot switch with {itemToDrop.Name}");
                }
            }
            else
            {
                MessageSystem.Add(MessageSystem.EventTypes.Player, $"You left {itemToDrop.Name} in the room");
                room.PlaceItem = itemToDrop;
                RemoveItem(itemToDrop);
            }
        }

        public void TryPickItemFromRoom(Room room)
        {
            if (room.PlaceItem == null)
            {
                MessageSystem.Add(MessageSystem.EventTypes.Game, $"The room is empty");
                return;
            }
            IPackable roomItem = room.PlaceItem;
            if (Weight + roomItem.Weight <= MaxWeight)
            {
                MessageSystem.Add(MessageSystem.EventTypes.Player, $"You picked up {roomItem.Name}");
                PickItem(roomItem);
                room.PlaceItem = null;
            }
            else
            {
                MessageSystem.Add(MessageSystem.EventTypes.Error, $"{roomItem.Name} is too heavy for your backpack");
            }
        }

        public void PickItem(IPackable item)
        {
            BackPack.Add(item);
            item.EnhancePlayer(this);
            Weight += item.Weight;
        }

        public void RemoveItem(IPackable item)
        {
            int index = BackPack.FindIndex(x => x.Id == item.Id);
            BackPack.RemoveAt(index);
            item.LowerPlayer(this);
            Weight -= item.Weight;
        }

        public string ListItems()
        {
            string listItems = $"{Weight}/{MaxWeight}kg| ";
            for (int i = 0; i < BackPack.Count; i++)
            {
                listItems += $"{i}) {BackPack[i].Name}";
                if (i < BackPack.Count - 1)
                {
                    listItems += ", ";
                }
            }
            return listItems;
        }

        public bool ValidNumber(int itemIndex)
        {
            if (BackPack.Count > 0)
            {
                if (itemIndex < BackPack.Count)
                    return true;
            }
            MessageSystem.Add(MessageSystem.EventTypes.Error, $"Wrong index number.");
            return false;
        }

        public void HandleItem(int itemIndex, Room room)
        {
            if (ValidNumber(itemIndex))
            {
                IPackable myItem = BackPack[itemIndex];
                myItem.UseOrDrop(this, room);
            }
        }

        public void Move(Player player, Room[,] rooms)
        {
            throw new NotImplementedException();
        }

        public int MaxWeight { get; } = 30;
        public int Weight { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

    }

}
