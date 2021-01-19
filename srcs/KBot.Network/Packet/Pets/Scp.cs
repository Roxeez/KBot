using KBot.Common.Extension;

namespace KBot.Network.Packet.Pets
{
    public class Scp : IPacket
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

    public class ScpCreator : IPacketCreator
    {
        public string Header { get; } = "sc_p";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Scp
            {
                PetId = content[0].ToInt(),
                ModelId = content[1].ToInt(),
                EntityId = content[2].ToLong(),
                Level = content[3].ToInt(),
                Loyalty = content[4].ToInt(),
                IsTeamMember = content[28].ToBool(),
                Name = content[31],
                IsSummonable = content[32].ToBool()
            };
        }
    }
}