using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Group;
using KBot.Game.Skills;
using KBot.Network.Packet.Group;

namespace KBot.Network.Processor.Group
{
    public class PSkiProcessor : PacketProcessor<PSki>
    {
        private readonly SkillFactory skillFactory;

        public PSkiProcessor(SkillFactory skillFactory)
        {
            this.skillFactory = skillFactory;
        }
        
        protected override void Process(GameSession session, PSki packet)
        {
            Partner partner = session.Character.Partner;
            
            if (partner == null)
            {
                Log.Warning("Can't add partner skills (partner is not yet created)");
                return;
            }

            partner.Skills.Clear();

            foreach (int skillId in packet.Skills)
            {
                Skill skill = skillFactory.CreateSkill(skillId);
                if (skill.Category != SkillCategory.Partner)
                {
                    Log.Warning($"Trying to add a non partner skill to partner {skillId}");
                    continue;
                }

                partner.Skills.Add(skill);
            }
        }
    }
}