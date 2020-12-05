using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class LevProcessor : PacketProcessor<Lev>
    {
        protected override void Process(GameSession session, Lev packet)
        {
            Character character = session.Character;

            character.Level = packet.Level;
            character.HeroLevel = packet.HeroLevel;
            character.JobLevel = packet.JobLevel;

            character.Experience = packet.Experience == 0 || packet.ExperienceRequired == 0 ? 0 : (packet.Experience / packet.ExperienceRequired) * 100;
            character.JobExperience = packet.JobExperience == 0 || packet.JobExperienceRequired == 0 ? 0 : (packet.JobExperience / packet.JobExperienceRequired) * 100;
            character.HeroExperience = packet.HeroExperience == 0 || packet.HeroExperienceRequired == 0 ? 0 : (packet.HeroExperience / packet.HeroExperienceRequired) * 100;

            Log.Trace("Updated character level, experience and experience requirements");
        }
    }
}