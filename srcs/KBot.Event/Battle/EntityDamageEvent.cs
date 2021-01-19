using KBot.Game.Entities;

namespace KBot.Event.Battle
{
    public class EntityDamageEvent : IEvent
    {
        public LivingEntity Caster { get; set; }
        public LivingEntity Target { get; set; }
        public int Damage { get; set; }
    }
}