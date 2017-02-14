using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    interface IGamePresenter
    {
        void ClearDisplay();
        void DisplayWorld(Player player, Room[,] rooms);
        void DisplayPlayerInfo(Player player);
        void DisplayMessages();
        void DisplayWin();
        void DisplayLost();
        void DisplayQuit();
    }
}
