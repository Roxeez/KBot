using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Group;
using KBot.Game.Skills;
using KBot.Network.Packet.Group;

namespace KBot.Network.Processor.Group
{
    public class PetSkiProcessor : PacketProcessor<PetSki>
    {
        private readonly SkillFactory skillFactory;

        public PetSkiProcessor(SkillFactory skillFactory)
        {
            this.skillFactory = skillFactory;
        }
        
        protected override void Process(GameSession session, PetSki packet)
        {
            Pet pet = session.Character.Pet;
            
            if (pet == null)
            {
                Log.Warning("Can't add pet skills (pet is not yet created)");
                return;
            }

            pet.Skills.Clear();

            foreach (int skillId in packet.Skills)
            {
                Skill skill = skillFactory.CreateSkill(skillId);
                if (skill.Category != SkillCategory.Partner)
                {
                    Log.Warning($"Trying to add a non pet skill to pet {skillId}");
                    continue;
                }

                pet.Skills.Add(skill);
            }
        }
    }
}