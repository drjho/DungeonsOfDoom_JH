using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    static class CrowdControll
    {
        static List<Creature> AffectedCreatures = new List<Creature>();
        static List<Creature> CreaturesToBeRemove = new List<Creature>();

        static public void Add(Creature creature)
        {
            AffectedCreatures.Add(creature);
        }

        static public void Remove(Creature creature)
        {
            CreaturesToBeRemove.Add(creature);
        }

        static public void UpdateAffectedCreature()
        {
            foreach (var creature in AffectedCreatures)
            {
                creature.UpdateCrowdControll();
            }
            foreach (var creature in CreaturesToBeRemove)
            {
                int index = AffectedCreatures.FindIndex(x => x.Id == creature.Id);
                AffectedCreatures.RemoveAt(index);
            }
            CreaturesToBeRemove.Clear();
        }
    }
}
