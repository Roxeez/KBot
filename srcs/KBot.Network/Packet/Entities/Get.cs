using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Entities
{
    public class Get : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public long DropId { get; set; }
    }

    public class GetCreator : IPacketCreator
    {
        public string Header { get; } = "get";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Get
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                DropId = content[2].ToLong()
            };
        }
    }
}