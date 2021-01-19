using KBot.Game.Entities;
using KBot.Game.Maps;

namespace KBot.Event.Maps
{
    public class EntityLeaveEvent : IEvent
    {
        public Entity Entity { get; set; }
        public Map Map { get; set; }
    }
}