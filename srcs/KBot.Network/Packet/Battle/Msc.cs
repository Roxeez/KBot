using KBot.Common.Extension;

namespace KBot.Network.Packet.Battle
{
    public class Msc : IPacket
    {
        public int Undefined { get; set; }
    }
    
    public class MscCreator : IPacketCreator
    {
        public string Header { get; } = "ms_c";

        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Msc
            {
                Undefined = content[0].ToInt()
            };
        }
    }
}