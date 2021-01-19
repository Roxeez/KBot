using KBot.Common.Extension;

namespace KBot.Network.Packet.Characters
{
    public class Lev : IPacket
    {
        public int Level { get; set; }
        public int Experience { get; set; }
        
        public int JobLevel { get; set; }
        public int JobExperience { get; set; }
        
        public int ExperienceRequired { get; set; }
        public int JobExperienceRequired { get; set; }
        
        public int Reputation { get; set; }
        public int Cp { get; set; }
        
        public int HeroExperience { get; set; }
        public int HeroLevel { get; set; }
        public int HeroExperienceRequired { get; set; }
    }

    public class LevCreator : IPacketCreator
    {
        public string Header { get; } = "lev";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Lev
            {
                Level = content[0].ToInt(),
                Experience = content[1].ToInt(),
                JobLevel = content[2].ToInt(),
                JobExperience = content[3].ToInt(),
                ExperienceRequired = content[4].ToInt(),
                JobExperienceRequired = content[5].ToInt(),
                Reputation = content[6].ToInt(),
                Cp = content[7].ToInt(),
                HeroExperience = content[8].ToInt(),
                HeroLevel = content[9].ToInt(),
                HeroExperienceRequired = content[10].ToInt()
            };
        }
    }
}