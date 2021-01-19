using KBot.Common.Extension;
using KBot.Game;

namespace KBot.Network.Packet.Maps
{
    public class At : IPacket
    {
        public int MapId { get; set; }
        public Position Position { get; set; }
        public int Direction { get; set; }
    }

    public class AtCreator : IPacketCreator
    {
        public string Header { get; } = "at";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new At
            {
                MapId = content[1].ToInt(),
                Position = new Position(content[2].ToShort(), content[3].ToShort()),
                Direction = content[4].ToInt()
            };
        }
    }
}