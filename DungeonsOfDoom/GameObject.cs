using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    abstract class GameObject
    {
        static int counter = 0;
        public GameObject(string name)
        {
            Name = name;
            Id = counter++;
        }

        protected GameObject(GameObject obj)
        {
            Name = obj.Name;
            Id = counter++;
        }

        public virtual string Name { get; set; }
        public int Id { get; set; }
    }
}
