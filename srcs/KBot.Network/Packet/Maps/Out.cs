using KBot.Extension;
using KBot.Game.Enum;

namespace KBot.Networking.Packet.Maps
{
    public class Out : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }

    public class OutCreate : IPacketCreator
    {
        public string Header { get; } = "out";
        
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