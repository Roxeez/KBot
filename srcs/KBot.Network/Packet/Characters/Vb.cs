using KBot.Common.Extension;

namespace KBot.Network.Packet.Characters
{
    public class Vb : IPacket
    {
        public int BuffId { get; set; }
        public bool IsEnabled { get; set; }
        public int Duration { get; set; }
    }

    public class VbCreator : IPacketCreator
    {
        public string Header { get; } = "vb";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Vb
            {
                BuffId = content[0].ToInt(),
                IsEnabled = content[1].ToBool(),
                Duration = content[2].ToInt()
            };
        }
    }
}