using KBot.Common.Extension;
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

            character.Experience = packet.Experience.GetPercentage(packet.ExperienceRequired);
            character.JobExperience = packet.JobExperience.GetPercentage(packet.JobExperienceRequired);
            character.HeroExperience = packet.HeroExperience.GetPercentage(packet.HeroExperienceRequired);

            Log.Trace("Updated character level, experience and experience requirements");
        }
    }
}