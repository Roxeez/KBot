using KBot.Event;
using KBot.Game;

namespace KBot.Core.Event
{
    public class PickUpDropsEvent : IEvent
    {
        public Position Waypoint { get; }

        public PickUpDropsEvent(Position waypoint)
        {
            Waypoint = waypoint;
        }
    }
}