using System.Linq;
using KBot.Core.Configuration;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;

namespace KBot.Core.Event.Processor
{
    public class InventoryChangeProcessor : EventProcessor<InventoryChangeEvent>
    {
        private readonly Bot bot;

        public InventoryChangeProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, InventoryChangeEvent e)
        {
            if (e.Inventory.Type != InventoryType.Main && e.Inventory.Type != InventoryType.Etc)
            {
                return;
            }

            foreach (ItemConfiguration item in bot.HealItems.ToList())
            {
                if (item.Item.InventoryType == e.Inventory.Type)
                {
                    bot.HealItems.Remove(item);
                }
            }

            foreach (InventoryItem item in e.Inventory)
            {
                if (item.IsPotion() || item.IsSnack())
                {
                    bot.HealItems.Add(new ItemConfiguration
                    {
                        Item = item.Stack.Item
                    });
                }
            }

            foreach (ItemConfiguration item in bot.UsedHealItems.ToList())
            {
                if (bot.HealItems.Contains(item))
                {
                    continue;
                }

                bot.UsedHealItems.Remove(item);
            }
        }
    }
}