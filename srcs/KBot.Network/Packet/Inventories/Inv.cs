using System.Collections.Generic;
using System.Linq;
using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Inventories
{
    public class Inv : IPacket
    {
        public InventoryType InventoryType { get; set; }
        public List<InventorySub> Items { get; set; }
    }

    public class InventorySub
    {
        public int Slot { get; set; }
        public int ModelId { get; set; }
        public int Amount { get; set; }
        public int Rarity { get; set; }
        public int Upgrade { get; set; }
        public int Perfection { get; set; }
        public int RuneLevel { get; set; }
    }

    public class InvCreator : IPacketCreator
    {
        public string Header { get; } = "inv";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            InventoryType type = content[0].ToEnum<InventoryType>();
            
            var items = new List<InventorySub>();
            foreach (string value in content.Skip(1))
            {
                string[] values = value.Split('.');
                if (values.Length == 3)
                {
                    items.Add(new InventorySub
                    {
                        Slot = values[0].ToInt(),
                        ModelId = values[1].ToInt(),
                        Amount = values[2].ToInt()
                    });
                }
                else if (values.Length >= 5)
                {
                    items.Add(new InventorySub
                    {
                        Slot = values[0].ToInt(),
                        ModelId = values[1].ToInt(),
                        Amount = 1,
                        Rarity = values[2].ToInt(),
                        Upgrade = values[3].ToInt(),
                        Perfection = type == InventoryType.Specialist ? values[4].ToInt()  : 0,
                        RuneLevel = type == InventoryType.Equipment ? values[4].ToInt() : 0
                    });
                }
            }
            
            return new Inv
            {
                InventoryType = content[0].ToEnum<InventoryType>(),
                Items = items
            };
        }
    }
}