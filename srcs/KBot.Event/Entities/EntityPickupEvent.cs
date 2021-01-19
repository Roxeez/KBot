using KBot.Game.Entities;

namespace KBot.Event.Entities
{
    public class EntityPickupEvent : IEvent
    {
        public MapObject Drop { get; set; }
        public LivingEntity Entity { get; set; }
    }
}