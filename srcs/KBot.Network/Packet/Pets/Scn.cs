using KBot.Common.Extension;

namespace KBot.Network.Packet.Pets
{
    public class Scn : IPacket
    {
        public int PetId { get; set; }
        public int ModelId { get; set; }
        public long EntityId { get; set; } 
        public int Level { get; set; }
        public int Loyalty { get; set; }
        public bool IsTeamMember { get; set; }
        public string Name { get; set; }
        public bool IsSummonable { get; set; }
    }

    public class ScnCreator : IPacketCreator
    {
        public string Header { get; } = "sc_n";
        
        public IPacket Create(string[] content)
        {
            return new Scn
            {
                PetId = content[0].ToInt(),
                ModelId = content[1].ToInt(),
                EntityId = content[2].ToLong(),
                Level = content[3].ToInt(),
                Loyalty = content[4].ToInt(),
                IsTeamMember = content[34].ToBool(),
                Name = content[36],
                IsSummonable = content[38].ToBool()
            };
        }
    }
}