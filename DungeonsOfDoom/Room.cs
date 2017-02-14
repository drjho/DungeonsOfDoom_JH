using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
	class Room
	{
		public Room(int brightness)
        {
            Brightness = brightness;

        }

        public bool IsEmpty { get { return (PlaceItem == null && RoomMonster == null); } }
        public int Brightness { get; set; }
		public Monster RoomMonster { get; set; }
		public IPackable PlaceItem { get; set; }
    }
}
