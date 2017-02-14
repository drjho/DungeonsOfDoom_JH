using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    static class MessageSystem
    {
        public enum EventTypes { Player, Demon, Game, Battle, Error };

        public struct Message
        {
            public EventTypes Type { get; set; }
            public string Text { get; set; }
        }

        static List<Message> messages = new List<Message>();

        static public void Add(EventTypes e, string s)
        {
            Message m = new Message();
            m.Type = e;
            m.Text = s;
            messages.Add(m);
        }

        static public void Purge()
        {
            messages.Clear();
        }

        static public Message LastMessage()
        {
            return messages[messages.Count - 1];
        }

        static public List<Message> GetMessages()
        {
            return messages;
        }

    }
}
