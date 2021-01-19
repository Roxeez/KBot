using System.Linq;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Enum;

namespace KBot.Core.Event.Processor
{
    public class SkillUpdateProcessor : EventProcessor<SkillUpdateEvent>
    {
        private readonly Bot bot;

        public SkillUpdateProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, SkillUpdateEvent e)
        {
            bot.DamageSkills.Clear();
            bot.BuffSkills.Clear();

            foreach (Skill skill in e.NewSkills)
            {
                if (skill.IsCombo)
                {
                    continue;
                }

                if (skill.CastId == 0) // Ignore basic attack
                {
                    continue;
                }
                
                if (skill.Type == SkillType.Damage)
                {
                    bot.DamageSkills.Add(skill);
                }
                else if (skill.Type == SkillType.Buff)
                {
                    if (skill.Target != SkillTarget.Self && skill.Target != SkillTarget.SelfOrTarget)
                    {
                        continue;
                    }
                    
                    bot.BuffSkills.Add(skill);
                }
            }

            foreach (Skill skill in bot.UsedDamageSkills.ToList())
            {
                if (!bot.DamageSkills.Contains(skill))
                {
                    bot.UsedDamageSkills.Remove(skill);
                }
            }

            foreach (Skill skill in bot.UsedBuffSkills.ToList())
            {
                if (!bot.BuffSkills.Contains(skill))
                {
                    bot.UsedBuffSkills.Remove(skill);
                }
            }
        }
    }
}