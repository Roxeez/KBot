using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Characters
{
    public class Ncif : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }

    public class NcifCreator : IPacketCreator
    {
        public string Header { get; } = "ncif";
        
        public IPacket Create(string[] content)
        {
            return new Ncif
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong()
            };
        }
    }
}