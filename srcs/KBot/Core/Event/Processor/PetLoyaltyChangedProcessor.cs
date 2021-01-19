using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;
using KBot.Game.Pets;

namespace KBot.Core.Event.Processor
{
    public class PetLoyaltyChangedProcessor : EventProcessor<PetLoyaltyChanged>
    {
        private readonly Bot bot;

        public PetLoyaltyChangedProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, PetLoyaltyChanged e)
        {
            if (!bot.UsePetFood)
            {
                return;
            }
            
            Pet pet = session.Character.Pet;
            if (pet == null)
            {
                return;
            }

            if (pet.Loyalty < 200)
            {
                InventoryItem item = session.Character.GetInventoryItem(InventoryType.Etc, 2077);
                if (item == null)
                {
                    return;
                }
                
                session.Character.UseItemOn(item, pet.Entity);
            }
        }
    }
}
