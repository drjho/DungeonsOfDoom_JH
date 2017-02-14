using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class StandardGameInput : IGameInput
    {
        public bool GameActionPerformed { get; set; }

        public string GetLine(string MessageToPlayer)
        {
            Console.WriteLine(MessageToPlayer);
            return Console.ReadLine();
        }

        protected bool YesOrNo(string question)
        {
            Console.Clear();
            do
            {
                Console.WriteLine(question);
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                    default:
                        Console.Clear();
                        Console.WriteLine("Sorry, wrong key...");
                        break;
                }
            } while (true);
        }

        protected void ChangePresenter(IGameContract igc)
        {
            Console.Clear();
            IGamePresenter igp = new StandardGamePresenter();
            IGameInput igi = new StandardGameInput();
            bool notChosen = true;
            do
            {
                Console.WriteLine("Press 'S' for standard presenter");
                Console.WriteLine("  or  'A' for alternative presenter");
                Console.WriteLine("  or  'T' for the third presenter");
                ConsoleKeyInfo presenterChoice = Console.ReadKey();

                switch (presenterChoice.Key)
                {
                    case ConsoleKey.S:
                        notChosen = false;
                        break;
                    case ConsoleKey.A:
                        igp = new AlternativeGamePresenter();
                        igi = new StandardGameInput();
                        notChosen = false;
                        break;
                    case ConsoleKey.T:
                        igp = new ThirdGamePresenter();
                        igi = new ThirdGamePresenter();
                        notChosen = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Sorry, wrong key...");
                        break;
                }
            } while (notChosen);
            igc.SetInputAndPresenter(igi, igp);
        }

        public void WaitForCommand(IGameContract igc)
        {
            
            GameActionPerformed = false;

            Console.CursorTop = Console.WindowHeight - 7;
            Console.CursorLeft = 0;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Commands:");
            Console.WriteLine(" [ESC] to quit this game.");
            Console.WriteLine(" [F11] to change Presenter mode.");
            Console.WriteLine(" [Arrows] for movement; [F] to fight; [P] for pick up");
            Console.WriteLine(" [Number + Enter] to drop/ switch with room item/ use stuff in backpack");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter your command:");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.F11:
                    ChangePresenter(igc);
                    break;
                case ConsoleKey.F12:
                    igc.Player.Cheat();
                    GameActionPerformed = true;

                    break;
                case ConsoleKey.Escape:
                    igc.WantToQuit = YesOrNo("Do you want to quit? (y)es or (n)o");
                    break;
                case ConsoleKey.UpArrow:
                    if (igc.Player.Y > 0)
                    {
                        igc.Player.Y--;
                        MessageSystem.Add(MessageSystem.EventTypes.Player, $"You moved up");
                        GameActionPerformed = true;

                    }
                    break;

                case ConsoleKey.DownArrow:

                    if (igc.Player.Y < igc.Rooms.GetLength(1) - 1)
                    {
                        igc.Player.Y++;
                        MessageSystem.Add(MessageSystem.EventTypes.Player, $"You moved down");
                        GameActionPerformed = true;

                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (igc.Player.X > 0)
                    {
                        igc.Player.X--;
                        MessageSystem.Add(MessageSystem.EventTypes.Player, $"You moved left");
                        GameActionPerformed = true;

                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (igc.Player.X < igc.Rooms.GetLength(0) - 1)
                    {
                        igc.Player.X++;
                        MessageSystem.Add(MessageSystem.EventTypes.Player, $"You moved right");
                        GameActionPerformed = true;

                    }
                    break;
                case ConsoleKey.P:
                    igc.Player.TryPickItemFromRoom(igc.CurrentRoom);
                    GameActionPerformed = true;

                    break;
                case ConsoleKey.F:
                    if (igc.CurrentRoom.RoomMonster != null)
                    {
                        MessageSystem.Add(MessageSystem.EventTypes.Player, $"You attacked a {igc.CurrentRoom.RoomMonster.Name}");
                        igc.Player.Fight(igc.CurrentRoom.RoomMonster, igc.CurrentRoom);
                    }
                    else
                    {
                        MessageSystem.Add(MessageSystem.EventTypes.Game, $"The room is empty");
                    }
                    GameActionPerformed = true;
                    break;
                default:
                    if (Char.IsNumber(keyInfo.KeyChar))
                    {
                        string numeric = keyInfo.KeyChar.ToString();
                        do
                        {
                            keyInfo = Console.ReadKey();
                            if (Char.IsNumber(keyInfo.KeyChar))
                            {
                                numeric += keyInfo.KeyChar.ToString();
                            }
                            else
                            {
                                if (keyInfo.Key == ConsoleKey.Backspace)
                                {
                                    Console.Write('\0');
                                    Console.CursorLeft--;
                                    if (numeric.Length > 0)
                                        numeric = numeric.Remove(numeric.Length - 1);
                                }
                                else if (Console.CursorLeft > 0)
                                {
                                    Console.CursorLeft--;
                                    Console.Write('\0');
                                    Console.CursorLeft--;
                                }
                            }
                        } while (keyInfo.Key != ConsoleKey.Enter);

                        int itemIndex;
                        if (int.TryParse(numeric, out itemIndex))
                        {
                            igc.Player.HandleItem(itemIndex, igc.CurrentRoom);
                            GameActionPerformed = true;

                        }
                    }
                    else
                    {
                        MessageSystem.Add(MessageSystem.EventTypes.Error, $"You have entered an invalid command");
                    }
                    break;


            }
        }
    }
}
