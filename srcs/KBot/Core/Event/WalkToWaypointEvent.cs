using KBot.Event;
using KBot.Game;

namespace KBot.Core.Event
{
    public class WalkToWaypointEvent : IEvent
    {
        public Position Waypoint { get; }

        public WalkToWaypointEvent(Position waypoint)
        {
            Waypoint = waypoint;
        }
    }
}