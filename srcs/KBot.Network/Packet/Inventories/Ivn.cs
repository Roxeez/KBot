using System;
using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Inventories
{
    public class Ivn : IPacket
    {
        public InventoryType InventoryType { get; set; }
        public InventorySub Item { get; set; }
    }

    public class IvnCreator : IPacketCreator
    {
        public string Header { get; } = "ivn";
        public PacketType PacketType { get; } = PacketType.Received;
        
        public IPacket Create(string[] content)
        {
            InventoryType type = content[0].ToEnum<InventoryType>();
            
            InventorySub item = null;
            string[] data = content[1].Split('.');

            if (data.Length == 4)
            {
                item = new InventorySub
                {
                    Slot = data[0].ToInt(),
                    ModelId = data[1].ToInt(),
                    Amount = data[2].ToInt(),
                };
            }
            else if (data.Length == 6)
            {
                item = new InventorySub
                {
                    Slot = data[0].ToInt(),
                    ModelId = data[1].ToInt(),
                    Amount = 1,
                    Rarity = data[2].ToInt(),
                    Upgrade = data[3].ToInt(),
                    Perfection = type == InventoryType.Specialist ? data[4].ToInt()  : 0,
                    RuneLevel = type == InventoryType.Equipment ? data[4].ToInt() : 0
                };
            }

            if (item == null)
            {
                throw new InvalidOperationException("Failed to create item");
            }

            return new Ivn
            {
                InventoryType = type,
                Item = item
            };
        }
    }
}