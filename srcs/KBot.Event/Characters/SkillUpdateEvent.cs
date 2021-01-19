using System.Collections.Generic;
using KBot.Game.Battle;

namespace KBot.Event.Characters
{
    public class SkillUpdateEvent : IEvent
    {
        public List<Skill> OldSkills { get; set; }
        public List<Skill> NewSkills { get; set; }
    }
}