using KBot.Game.Entities;

namespace KBot.Event.Battle
{
    public class EntityDeathEvent : IEvent
    {
        public LivingEntity Entity { get; set; }
        public LivingEntity Killer { get; set; }
    }
}