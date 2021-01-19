using KBot.Game.Maps;

namespace KBot.Event.Maps
{
    public class MapChangeEvent : IEvent
    {
        public Map Source { get; set; }
        public Map Destination { get; set; }
    }
}