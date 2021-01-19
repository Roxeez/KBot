using KBot.Common.Extension;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Network.Processor;

namespace KBot.Network.Packet.Battle
{
    public class Su : IPacket
    {
        public EntityType CasterType { get; set; }
        public long CasterId { get; set; }
        public EntityType TargetType { get; set; }
        public long TargetId { get; set; }
        public int SkillId { get; set; }
        public bool IsTargetAlive { get; set; }
        public int HpPercentage { get; set; }
        public int Damage { get; set; }
    }

    public class SuCreator : IPacketCreator
    {
        public string Header { get; } = "su";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            return new Su
            {
                CasterType = content[0].ToEnum<EntityType>(),
                CasterId = content[1].ToLong(),
                TargetType = content[2].ToEnum<EntityType>(),
                TargetId = content[3].ToLong(),
                SkillId = content[4].ToInt(),
                IsTargetAlive = content[10].ToBool(),
                HpPercentage = content[11].ToInt(),
                Damage = content[12].ToInt()
            };
        }
    }
}