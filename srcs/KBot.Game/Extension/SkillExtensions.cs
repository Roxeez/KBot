using System;
using KBot.Game.Battle;

namespace KBot.Game.Extension
{
    public static class SkillExtensions
    {
        public static bool IsOnCooldown(this Skill skill)
        {
            return skill.LastUse.AddMilliseconds(skill.Cooldown * 100) > DateTime.Now;
        }
    }
}