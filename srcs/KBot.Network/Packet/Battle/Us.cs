using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Battle
{
    public class Us : IPacket
    {
        public int CastId { get; set; }
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }

    public class UsCreator : IPacketCreator
    {
        public string Header { get; } = "u_s";
        public PacketType PacketType { get; } = PacketType.Send;
        
        public IPacket Create(string[] content)
        {
            return new Us
            {
                CastId = content[0].ToInt(),
                EntityType = content[1].ToEnum<EntityType>(),
                EntityId = content[2].ToLong()
            };
        }
    }
}