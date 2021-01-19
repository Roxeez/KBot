using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Battle
{
    public class Die : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }

    public class DieCreator : IPacketCreator
    {
        public string Header { get; } = "die";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Die
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong()
            };
        }
    }
}