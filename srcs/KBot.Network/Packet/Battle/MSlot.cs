using KBot.Common.Extension;

namespace KBot.Network.Packet.Battle
{
    public class MSlot : IPacket
    {
        public int CastId { get; set; }
    }

    public class MSlotCreator : IPacketCreator
    {
        public string Header { get; } = "mslot";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new MSlot
            {
                CastId = content[0].ToInt()
            };
        }
    }
}