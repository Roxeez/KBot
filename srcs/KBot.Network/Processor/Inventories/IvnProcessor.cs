using System.Linq;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;
using KBot.Network.Packet.Inventories;

namespace KBot.Network.Processor.Inventories
{
    public class IvnProcessor : PacketProcessor<Ivn>
    {
        private readonly ItemFactory itemFactory;
        private readonly EventPipeline eventPipeline;

        public IvnProcessor(ItemFactory itemFactory, EventPipeline eventPipeline)
        {
            this.itemFactory = itemFactory;
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Ivn packet)
        {
            int slot = packet.Item.Slot;
            int amount = packet.Item.Amount;
            int modelId = packet.Item.ModelId;
            
            Inventory inventory = session.Character.GetInventory(packet.InventoryType);
            if (inventory == null)
            {
                Log.Warning($"Inventory {packet.InventoryType} is not loaded");
                return;
            }

            if (modelId == -1)
            {
                inventory[slot] = null;
                return;
            }
            
            Item item = itemFactory.CreateItem(modelId);
            
            inventory[packet.Item.Slot] = new InventoryItem(new ItemStack(item, packet.Item.Amount), packet.InventoryType, packet.Item.Slot);
            
            eventPipeline.Process(session, new InventoryChangeEvent
            {
                Inventory = inventory
            });
        }
    }
}