using System.Collections.Generic;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Battle;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class SkiProcessor : PacketProcessor<Ski>
    {
        private readonly SkillFactory skillFactory;

        public SkiProcessor(SkillFactory skillFactory)
        {
            this.skillFactory = skillFactory;
        }
        
        protected override void Process(GameSession session, Ski packet)
        {
            Character character = session.Character;
            
            character.Skills.Clear();
            
            foreach (int skillId in packet.Skills)
            {
                Skill skill = skillFactory.CreateSkill(skillId);
                if (skill.Category == SkillCategory.Player || skill.Category == SkillCategory.Partner)
                {
                    character.Skills.Add(skill);
                }
            }
            
            Log.Debug("Skills successfully loaded");
        }
    }
}