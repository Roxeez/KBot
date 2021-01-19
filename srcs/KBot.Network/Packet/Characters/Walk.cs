using KBot.Common.Extension;
using KBot.Game;

namespace KBot.Network.Packet.Characters
{
    public class Walk : IPacket
    {
        public Position Position { get; set; }
        public int Speed { get; set; }
    }

    public class WalkCreator : IPacketCreator
    {
        public string Header { get; } = "walk";
        public PacketType PacketType { get; } = PacketType.Send;
        
        public IPacket Create(string[] content)
        {
            return new Walk
            {
                Position = new Position(content[0].ToShort(), content[1].ToShort()),
                Speed = content[2].ToInt()
            };
        }
    }
}