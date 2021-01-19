using KBot.Common.Extension;

namespace KBot.Network.Packet.Battle
{
    public class Sr : IPacket
    {
        public int CastId { get; set; }
    }

    public class SrCreator : IPacketCreator
    {
        public string Header { get; } = "sr";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Sr
            {
                CastId = content[0].ToInt()
            };
        }
    }
}