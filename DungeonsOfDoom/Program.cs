using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    class Program
    {
        static class Imports
        {
            public static IntPtr HWND_BOTTOM = (IntPtr)1;
            // public static IntPtr HWND_NOTOPMOST = (IntPtr)-2;l
            public static IntPtr HWND_TOP = (IntPtr)0;
            // public static IntPtr HWND_TOPMOST = (IntPtr)-1;

            public static uint SWP_NOSIZE = 1;
            public static uint SWP_NOZORDER = 4;

            [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
            public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, uint wFlags);
        }

        static void Main(string[] args)
        {
            var consoleWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            Imports.SetWindowPos(consoleWnd, 0, 0, 0, 0, 0, Imports.SWP_NOSIZE | Imports.SWP_NOZORDER);
            Console.Title = "Dungeons of Doom - by Martin & Johnson";
            try
            {
                bool playAgain = false;
                do
                {
                    Console.Clear();
                    IGamePresenter presenter = null;
                    IGameInput input = new StandardGameInput();
                    do
                    {
                        Console.WriteLine("Press 'S' for standard presenter");
                        Console.WriteLine("  or  'A' for alternative presenter");
                        Console.WriteLine("  or  'T' for the third presenter");
                        ConsoleKeyInfo presenterChoice = Console.ReadKey();

                        switch (presenterChoice.Key)
                        {
                            case ConsoleKey.S:
                                presenter = new StandardGamePresenter();
                                break;
                            case ConsoleKey.A:
                                presenter = new AlternativeGamePresenter();
                                break;
                            case ConsoleKey.T:
                                presenter = new ThirdGamePresenter();
                                input = new ThirdGamePresenter();
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Sorry, wrong key...");
                                break;
                        }
                    } while (presenter == null);

                    presenter.ClearDisplay();

                    Game game = new Game(20, 10, presenter, input);
                    game.Start();

                    bool unanswered = true;
                    do
                    {
                        Console.WriteLine("Do you wanna play again? Press Y/N for yes or no");
                        ConsoleKey key = Console.ReadKey().Key;

                        switch (key)
                        {
                            case ConsoleKey.Y:
                                unanswered = false;
                                playAgain = true;
                                break;
                            case ConsoleKey.N:
                                unanswered = false;
                                playAgain = false;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Sorry, wrong key...");
                                break;
                        }
                    } while (unanswered);

                } while (playAgain);
                Console.Clear();
                Console.WriteLine("Bye bye!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ooops ett fel har uppstått.");
                Console.WriteLine($"Om du verkligen vill veta så var det: \n {e.Message} \n {e.StackTrace}");
            }

        }
    }
}
