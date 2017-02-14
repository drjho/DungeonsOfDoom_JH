using System;

namespace DoD
{
    class ThirdGamePresenter : StandardGameInput, IGamePresenter
    {
        bool ScreenSizeSet = false;
        int messageStartY = 0;
        int playerStartX = 0;

        public void ClearDisplay()
        {
            Console.Clear();
        }

        public void DisplayLost()
        {
            ClearDisplay();
            Console.WriteLine($"You have been slain!!");
        }

        public void DisplayMessages()
        {
            Console.CursorTop = messageStartY;
            Console.CursorLeft = 0;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var message in MessageSystem.GetMessages())
            {
                switch (message.Type)
                {
                    case MessageSystem.EventTypes.Player:
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;
                    case MessageSystem.EventTypes.Demon:
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        break;
                    case MessageSystem.EventTypes.Game:
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case MessageSystem.EventTypes.Battle:
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;
                    case MessageSystem.EventTypes.Error:
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }
                Console.WriteLine(message.Text);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            MessageSystem.Purge();
        }

        public void DisplayPlayerInfo(Player player)
        {

        }

        public void DisplayQuit()
        {
            ClearDisplay();
            Console.WriteLine($"So it didn't so well?!");
        }

        public void DisplayWin()
        {
            ClearDisplay();
            Console.WriteLine("You have slain the Demon!!");
        }

        public void DisplayWorld(Player player, Room[,] rooms)
        {
            int mapWidth = rooms.GetLength(0) * 3;
            if (!ScreenSizeSet)
            {
                int winHeight = 1 + rooms.GetLength(1) + 3 + 9 + 7;
                //int winWidth = mapWidth + 23 + 16 + 2;
                int winWidth = 60 + 23 + 16 + 2; 
                Console.WindowHeight = Math.Min(winHeight, Console.LargestWindowHeight);
                Console.WindowWidth = Math.Min(winWidth, Console.LargestWindowWidth);
                ScreenSizeSet = true;
            }

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("The Map:".PadRight(mapWidth, ' '));

            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("[");
                    if (y == player.Y && x == player.X)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write((char)2);
                    }
                    else if (rooms[x, y].RoomMonster != null)
                    {
                        char symbol = rooms[x, y].RoomMonster.Symbol;
                        switch (symbol)
                        {
                            case 'D':
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                        }
                        Console.Write($"{rooms[x, y].RoomMonster.Symbol}");
                    }
                    else if (rooms[x, y].PlaceItem != null)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("i");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("]");
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Room item: ");
            if (rooms[player.X, player.Y].PlaceItem != null)
            {
                IPackable item = rooms[player.X, player.Y].PlaceItem;
                Console.Write($"{rooms[player.X, player.Y].PlaceItem.Description }");
            }
            Console.WriteLine();
            Console.Write("Room monster: ");
            if (rooms[player.X, player.Y].RoomMonster != null)
            {
                char symbol = rooms[player.X, player.Y].RoomMonster.Symbol;
                switch (symbol)
                {
                    case 'D':
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                }
                Console.Write($"{rooms[player.X, player.Y].RoomMonster.Description }");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            messageStartY = Console.CursorTop + 1;
            //playerStartX = rooms.GetLength(0) * 3 + 1;
            playerStartX = Console.WindowWidth - 23 - 16 - 1;

            int yPos = 0;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(playerStartX, yPos++);
            Console.Write(String.Format("{0,-23}{1,4}{2,4}{3,4}{4,4}",
                "Player:", "HP", "At", "De", "kg"));
            Console.SetCursorPosition(playerStartX, yPos++);
            Console.Write(String.Format("{0,-23}{1,4}{2,4}{3,4}",
                player.Name, player.Health, player.MaxAttack, player.Defense));
            if (player.Weight == player.MaxWeight)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (player.Weight > player.MaxWeight-5)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            Console.Write(String.Format("{0,4}", player.Weight));
            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkGray;
            int counter = 0;
            foreach (IPackable item in player.BackPack)
            {
                Console.SetCursorPosition(playerStartX, yPos++);
                Console.Write(String.Format("{0,-3}{1,-20}{2,4}{3,4}{4,4}{5,4}",
                   counter++, item.Name, item.GetRegeneration(), item.GetAttack(), item.GetDefense(), item.Weight));
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}