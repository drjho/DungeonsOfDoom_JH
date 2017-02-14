using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    interface IGameInput
    {
        void WaitForCommand(IGameContract gc);
        string GetLine(string MessageToPlayer);
        bool GameActionPerformed { get; set; }
    }
}
