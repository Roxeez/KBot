using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBot.Game;

namespace KBot.Event.Characters
{
    public class WalkEvent : IEvent
    {
        public Position From { get; set; }
        public Position To { get; set; }
    }
}
