using KBot.Event;
using KBot.Game;

namespace KBot.Core.Event
{
    public class KillMonstersEvent : IEvent
    {
        public Position Waypoint { get; }

        public KillMonstersEvent(Position waypoint)
        {
            Waypoint = waypoint;
        }
    }
}