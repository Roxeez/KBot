using System.Collections.Generic;
using System.Linq;
using KBot.Core.Configuration;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Inventories;

namespace KBot.Core.Event.Processor
{
    public class StatChangeEventProcessor : EventProcessor<StatChangeEvent>
    {
        private readonly Bot bot;

        public StatChangeEventProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, StatChangeEvent e)
        {
            if (!bot.IsRunning)
            {
                return;
            }
            
            Character character = session.Character;
            
            IEnumerable<ItemConfiguration> hpItems = bot.UsedHealItems.Where(x => x.UseForHp && character.HpPercentage < x.HpPercentage).OrderBy(x => x.HpPercentage);
            IEnumerable<ItemConfiguration> mpItems = bot.UsedHealItems.Where(x => x.UseForMp && character.MpPercentage < x.MpPercentage).OrderBy(x => x.MpPercentage);

            foreach(ItemConfiguration hpItem in hpItems)
            {
                InventoryItem item = character.GetInventoryItem(hpItem.Item);
                if (item != null)
                {
                    character.UseItem(item);
                    break;
                }
            }

            foreach (ItemConfiguration mpItem in mpItems)
            {
                InventoryItem item = character.GetInventoryItem(mpItem.Item);
                if (item != null)
                {
                    character.UseItem(item);
                    break;
                }
            }
        }
    }
}