using System.Collections.Generic;
using System.Linq;
using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Group
{
    public class PInit : IPacket
    {
        public List<PInitSub> Members { get; set; }
    }
    
    public class PInitSub
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public bool IsPartner { get; set; }
    }

    public class PInitCreator : IPacketCreator
    {
        public string Header { get; } = "pinit";
        
        public IPacket Create(string[] content)
        {
            List<PInitSub> subs = new List<PInitSub>();
            foreach (string value in content.Skip(1))
            {
                string[] data = value.Split('|');
                subs.Add(new PInitSub
                {
                    EntityType = data[0].ToEnum<EntityType>(),
                    EntityId = data[1].ToLong(),
                    IsPartner = data[7].ToBool()
                });
            }

            return new PInit
            {
                Members = subs
            };
        }
    }
}