using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Battle
{
    public class Bf : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        
        public int BuffId { get; set; }
        public int Duration { get; set; }
        
        public int Level { get; set; }
    }

    public class BfCreator : IPacketCreator
    {
        public string Header { get; } = "bf";
        
        public IPacket Create(string[] content)
        {
            string[] data = content[2].Split('.');
            return new Bf
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                BuffId = data[1].ToInt(),
                Duration = data[2].ToInt(),
                Level = content[3].ToInt()
            };
        }
    }
}