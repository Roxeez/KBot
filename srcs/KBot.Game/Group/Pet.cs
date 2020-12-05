using System.Collections.Generic;
using KBot.Game.Entities;
using KBot.Game.Skills;

namespace KBot.Game.Party
{
    public class Pet
    {
        public LivingEntity Entity { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
    }
}