using KBot.Common.Extension;
using KBot.Game;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Maps
{
    public class Gp : IPacket
    {
        public Position Position { get; set; }
        public int DestinationId { get; set; }
        public PortalType PortalType { get; set; }
        public int Id { get; set; }
    }

    public class GpCreator : IPacketCreator
    {
        public string Header { get; } = "gp";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Gp
            {
                Position = new Position(content[0].ToShort(), content[1].ToShort()),
                DestinationId = content[2].ToShort(),
                PortalType = content[3].ToEnum<PortalType>(),
                Id = content[4].ToInt()
            };
        }
    }
}