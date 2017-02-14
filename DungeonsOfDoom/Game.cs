using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{

	class Game : IGameContract // IGameContract ska låta IGameInput veta vilka saker den får styra.
	{
		public Room[,] Rooms { get; private set; }
		public Player Player { get; private set; }
		public Demon Demon { get; private set; }
        public IGamePresenter GamePresenter { get; set; }
        public IGameInput GameInput { get; set; }
        public bool WantToQuit { get; set; }
        public Room CurrentRoom { get { return Rooms[Player.X, Player.Y]; } }
        private int WorldWidth { get; set; }
        private int WorldHeight { get; set; }
        private List<Item> items { get; set; } = new List<Item>();
        private List<Goblin> Goblins { get; set; } = new List<Goblin>();

        public void SetInputAndPresenter(IGameInput gameInput, IGamePresenter gamePresenter) // Måste ha den annars kan inte man ändra Presenter/Input i spelet.
        {
            this.GamePresenter = gamePresenter;
            this.GameInput = gameInput;
        }

		public Game(int worldWidth, int worldHeight, IGamePresenter gamePresenter, IGameInput gameInput)
		{
			// Sätt klassens "State"
			this.WorldWidth = worldWidth;
			this.WorldHeight = worldHeight;
            SetInputAndPresenter(gameInput, gamePresenter);
            WantToQuit = false;
		}

		public void Start()
		{
			NewItems();
			CreatePlayer();
			CreateRooms();
			CreateDemon();
			do
			{
				GamePresenter.ClearDisplay();
				GamePresenter.DisplayPlayerInfo(Player);
				GamePresenter.DisplayWorld(Player, Rooms);
                CrowdControll.UpdateAffectedCreature();
                GamePresenter.DisplayMessages();
                if (Player.AffectedBy != Creature.EffectTypes.Freeze)
                {
                    GameInput.WaitForCommand(this); 
                }
                else
                {
                    GameInput.GameActionPerformed = true;
                }
                if (GameInput.GameActionPerformed)
                {
                    MonsterTurn();
                    DemonChase();
                    GoblinRun();
                }

                if (WantToQuit)
                {
                    GamePresenter.DisplayQuit();
                    return;
                }

				if (Demon.Health < 1)
				{
					GamePresenter.DisplayWin();
					return;
				}

			} while (Player.Health > 0);
			GamePresenter.DisplayLost();
		}

        private void GoblinRun()
        {
            foreach (var goblin in Goblins)
            {
                if (goblin.Health > 0)
                    goblin.Move(Player, Rooms);
            }
        }

        private void DemonChase()
		{
            Demon.Move(Player, Rooms);
        }

        private void MonsterTurn()
        {
            if (CurrentRoom.RoomMonster != null)
            {
                if (CurrentRoom.RoomMonster.Health < 1)
                {
                    MessageSystem.Add(MessageSystem.EventTypes.Player, $"You have killed a {CurrentRoom.RoomMonster.Name}");
                    CurrentRoom.RoomMonster.Die(CurrentRoom);
                }
                else
                {
                    CurrentRoom.RoomMonster.Fight(Player, CurrentRoom);
                }
            }
        }

		void CreateRooms()
		{
		
			Rooms = new Room[WorldWidth, WorldHeight];

			// SKapa Rummen
			for (int y = 0; y < WorldHeight; y++)
			{
				for (int x = 0; x < WorldWidth; x++)
				{
					Rooms[x, y] = new Room(RandomUtils.RandomStat(100 + 1));
					if (x == Player.X && y == Player.Y)
					{
						continue;
					}
					if (RandomUtils.Try(12) && Rooms[x, y] != CurrentRoom)
					{
						Rooms[x, y].PlaceItem = (Item)items[(RandomUtils.RandomStat(0, items.Count))].Clone();
					}
					else if (RandomUtils.Try(13) && Rooms[x, y] != CurrentRoom) 
					{
						Rooms[x, y].RoomMonster = CreateMonster(x, y);
					}
					
				}
			}
		}
	   
		void CreatePlayer()
		{
			string input = GameInput.GetLine("Please enter your name: ");
			if (input.Length == 0)
				input = "^Fighter^";
			Player = new Player(input, 100, 5, 5);
			Player.X = RandomUtils.RandomStat(WorldWidth); 
			Player.Y = RandomUtils.RandomStat(WorldHeight);

		}

		void CreateDemon()
		{
			Demon = new Demon("Demon", 75, 45, 25);
			do
			{
				Demon.X = RandomUtils.RandomStat(WorldWidth);
				Demon.Y = RandomUtils.RandomStat(WorldHeight);
			} while (MathUtils.Distance(Player, Demon) < 6 || Rooms[Demon.X, Demon.Y].RoomMonster != null);

			Rooms[Demon.X, Demon.Y].RoomMonster = Demon; 
		}

		public void NewItems()
		{
			items.Add(new Weapon("Broad Sword", 2, 5));
			items.Add(new Armor("Wooden Shield", 3, 3));
			items.Add(new Armor("Chain Armor", 3, 4));
			items.Add(new Weapon("Stick", 1, 3));
			items.Add(new Consumables("Apple", 1, 10));
		}

		Monster CreateMonster(int x, int y)
		{
            int random = RandomUtils.RandomStat(100);
			if (random < 25)
			{
				return new Skeleton("Skeleton", 9, 18, 2);
			}
			else if (random < 50)
			{
                return new Giant("Giant", 15, 15, 6);
			}
			else if (random < 75)
            {
                return new Troll("Troll", 12, 11, 4);
			}
            else 
            {
                Goblin goblin = new Goblin("Goblin", 1, 0, 0);
                goblin.X = x;
                goblin.Y = y;
                Goblins.Add(goblin);
                return goblin;
            }
		}
	}
}
