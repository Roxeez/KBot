using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Maps
{
    public class Out : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }

    public class OutCreate : IPacketCreator
    {
        public string Header { get; } = "out";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Out
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong()
            };
        }
    }
}