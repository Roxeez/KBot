using System.Collections.Generic;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Inventories;
using KBot.Network.Packet.Inventories;

namespace KBot.Network.Processor.Inventories
{
    public class InvProcessor : PacketProcessor<Inv>
    {
        private readonly ItemFactory itemFactory;

        public InvProcessor(ItemFactory itemFactory)
        {
            this.itemFactory = itemFactory;
        }
        
        protected override void Process(GameSession session, Inv packet)
        {
            var items = new Dictionary<int, InventoryItem>();
            foreach (InventorySub sub in packet.Items)
            {
                Item item = itemFactory.CreateItem(sub.ModelId);
                var stack = new ItemStack(item, sub.Amount);

                items[sub.Slot] = new InventoryItem(stack, packet.InventoryType, sub.Slot);
            }
            
            session.Character.Inventories[packet.InventoryType] = new Inventory(packet.InventoryType, items);
            
            Log.Debug($"Inventory {packet.InventoryType} successfully populated");
        }
    }
}