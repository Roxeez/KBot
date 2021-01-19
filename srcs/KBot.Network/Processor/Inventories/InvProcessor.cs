using System.Collections.Generic;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Inventories;
using KBot.Network.Packet.Inventories;

namespace KBot.Network.Processor.Inventories
{
    public class InvProcessor : PacketProcessor<Inv>
    {
        private readonly ItemFactory itemFactory;
        private readonly EventPipeline eventPipeline;
        public InvProcessor(ItemFactory itemFactory, EventPipeline eventPipeline)
        {
            this.itemFactory = itemFactory;
            this.eventPipeline = eventPipeline;
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
            
            eventPipeline.Process(session, new InventoryLoadedEvent
            {
                Inventory = session.Character.Inventories[packet.InventoryType]
            });
        }
    }
}