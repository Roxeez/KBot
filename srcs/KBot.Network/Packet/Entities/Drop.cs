using KBot.Common.Extension;
using KBot.Game;

namespace KBot.Network.Packet.Entities
{
    public class Drop : IPacket
    {
        public int ModelId { get; set; }
        public long DropId { get; set; }
        
        public Position Position { get; set; }
        public int Amount { get; set; }
        public long OwnerId { get; set; }
    }

    public class DropCreator : IPacketCreator
    {
        public string Header { get; } = "drop";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Drop
            {
                ModelId = content[0].ToInt(),
                DropId = content[1].ToInt(),
                Position = new Position(content[2].ToShort(), content[3].ToShort()),
                Amount = content[4].ToInt(),
                OwnerId = content[6].ToInt()
            };
        }
    }
}