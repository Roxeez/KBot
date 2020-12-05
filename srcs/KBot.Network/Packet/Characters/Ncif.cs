using KBot.Game.Enum;

namespace KBot.Network.Packet.Player
{
    public class Ncif : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
    }
}