using KBot.Game;
using KBot.Game.Entities;

namespace KBot.Event.Entities
{
    public class EntityMoveEvent : IEvent
    {
        public LivingEntity Entity { get; set; }
        public Position From { get; set; }
        public Position To { get; set; }
    }
}