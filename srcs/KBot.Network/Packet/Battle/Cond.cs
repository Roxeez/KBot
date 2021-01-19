using KBot.Game.Enum;

namespace KBot.Network.Packet.Battle
{
    public class Cond : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public bool CantAttack { get; set; }
        public bool CantMove { get; set; }
        public int Speed { get; set; }
    }
}