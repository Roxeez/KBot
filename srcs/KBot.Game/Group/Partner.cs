using System.Collections.Generic;
using KBot.Game.Entities;
using KBot.Game.Skills;

namespace KBot.Game.Party
{
    public class Partner
    {
        public LivingEntity Entity { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}