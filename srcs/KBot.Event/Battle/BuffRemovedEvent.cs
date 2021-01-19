using KBot.Game.Battle;
using KBot.Game.Entities;

namespace KBot.Event.Battle
{
    public class BuffRemovedEvent : IEvent
    {
        public LivingEntity Entity { get; set; }
        public Buff Buff { get; set; }
    }
}