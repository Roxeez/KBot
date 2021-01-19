using System.Collections.Generic;
using System.Linq;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Characters;
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
        private readonly EventPipeline eventPipeline;
        
        public SkiProcessor(SkillFactory skillFactory, EventPipeline eventPipeline)
        {
            this.skillFactory = skillFactory;
            this.eventPipeline = eventPipeline;
        }
        
        protected override void Process(GameSession session, Ski packet)
        {
            Character character = session.Character;

            var old = character.Skills.ToList();
            
            character.Skills.Clear();
            
            foreach (int skillId in packet.Skills)
            {
                Skill skill = skillFactory.CreateSkill(skillId);
                if (skill.Category == SkillCategory.Player || skill.Category == SkillCategory.Partner)
                {
                    character.Skills.Add(skill);
                }
            }

            eventPipeline.Process(session, new SkillUpdateEvent
            {
                OldSkills = old,
                NewSkills = character.Skills.ToList()
            });
            
            Log.Debug("Skills successfully loaded");
        }
    }
}