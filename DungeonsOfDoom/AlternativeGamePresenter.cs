using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class AlternativeGamePresenter : IGamePresenter
    {
        public void DisplayPlayerInfo(Player player)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Player: {player.Description}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Backpack: {player.ListItems()}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayWorld(Player player, Room[,] rooms)
        {

            String message = "Room: ";
            if (rooms[player.X, player.Y].PlaceItem != null)
            {
                message += $"{rooms[player.X, player.Y].PlaceItem.Description } ";
            }
            if (rooms[player.X, player.Y].RoomMonster != null)
            {
                char symbol = rooms[player.X, player.Y].RoomMonster.Symbol;
                switch (symbol)
                {
                    case 'D':
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                }
                message += $"{rooms[player.X, player.Y].RoomMonster.Description}";
            }
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;



            for (int y = 0; y < rooms.GetLength(1); y++)
            {
                for (int x = 0; x < rooms.GetLength(0); x++)
                {
                    if (y == player.Y && x == player.X)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[0]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (rooms[x, y].RoomMonster != null)
                    {
                        char symbol = rooms[x, y].RoomMonster.Symbol;
                        switch (symbol)
                        {
                            case 'D':
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                        }
                        Console.Write($"[{rooms[x, y].RoomMonster.Symbol}]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (rooms[x, y].PlaceItem != null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("[i]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write("[ ]");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("|");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void DisplayMessages()
        {
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

        public void DisplayWin()
        {
            ClearDisplay();
            Console.WriteLine("You have slain the Demon!!");
        }

        public void DisplayLost()
        {
            ClearDisplay();
            Console.WriteLine($"You have been slain!!");
        }

        public void ClearDisplay()
        {
            Console.Clear();
        }

        public void DisplayQuit()
        {
            ClearDisplay();
            Console.WriteLine($"So, too much pressure for you?!");
        }
    }
}
