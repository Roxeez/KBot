using KBot.Common.Extension;
using KBot.Game;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Entities
{
    public class Mv : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public Position Position { get; set; }
        public int Speed { get; set; }
    }

    public class MvCreator : IPacketCreator
    {
        public string Header { get; } = "mv";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Mv
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToInt(),
                Position = new Position(content[2].ToShort(), content[3].ToShort()),
                Speed = content[4].ToInt()
            };
        }
    }
}