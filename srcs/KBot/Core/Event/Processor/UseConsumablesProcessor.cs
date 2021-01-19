using System.Linq;
using System.Threading;
using KBot.Event;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;

namespace KBot.Core.Event.Processor
{
    public class UseConsumablesProcessor : EventProcessor<UseConsumablesEvent>
    {
        private readonly Bot bot;

        public UseConsumablesProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, UseConsumablesEvent e)
        {
            Character character = session.Character;
            Inventory inventory = character.GetInventory(InventoryType.Main);

            if (bot.UseAttackPotion)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 116);
                if (!hasBuff)
                {
                    InventoryItem attack = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1246 || x.Stack.Item.Id == 9020);
                    if (attack != null)
                    {
                        character.UseItem(attack);
                        Thread.Sleep(200);
                    }
                }
            }

            if (bot.UseDefencePotion)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 117);
                if (!hasBuff)
                {
                    InventoryItem defense = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1247 || x.Stack.Item.Id == 9021);
                    if (defense != null)
                    {
                        character.UseItem(defense);
                        Thread.Sleep(200);
                    }
                }
            }

            if (bot.UseEnergyPotion)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 118);
                if (!hasBuff)
                {
                    InventoryItem energy = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1248 || x.Stack.Item.Id == 9022);
                    if (energy != null)
                    {
                        character.UseItem(energy);
                        Thread.Sleep(200);
                    }
                }
            }

            if (bot.UseExperiencePotion)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 119);
                if (!hasBuff)
                {
                    InventoryItem experience = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1249 || x.Stack.Item.Id == 9023);
                    if (experience != null)
                    {
                        character.UseItem(experience);
                        Thread.Sleep(200);
                    }
                }
            }
            
            if (bot.UseAncelloanBlessing)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 121);
                if (!hasBuff)
                {
                    InventoryItem ancelloanBlessing = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1286 || x.Stack.Item.Id == 9041);
                    if (ancelloanBlessing != null)
                    {
                        character.UseItem(ancelloanBlessing);
                        Thread.Sleep(200);
                    }
                }
            }
            
            if (bot.UseFairyBoost)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 131);
                if (!hasBuff)
                {
                    InventoryItem fairyBooster = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1296 || x.Stack.Item.Id == 9074);
                    if (fairyBooster != null)
                    {
                        character.UseItem(fairyBooster);
                        Thread.Sleep(200);
                    }
                }
            }
            
            if (bot.UseMateBlessing)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 122);
                if (!hasBuff)
                {
                    InventoryItem mateBlessing = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1285 || x.Stack.Item.Id == 9043);
                    if (mateBlessing != null)
                    {
                        character.UseItem(mateBlessing);
                        Thread.Sleep(200);
                    }
                }
            }

            if (bot.UseSoulstoneBlessing)
            {
                bool hasBuff = character.Buffs.Any(x => x.Id == 122);
                if (!hasBuff)
                {
                    InventoryItem mateBlessing = inventory.FirstOrDefault(x => x.Stack.Item.Id == 1362 || x.Stack.Item.Id == 9075);
                    if (mateBlessing != null)
                    {
                        character.UseItem(mateBlessing);
                        Thread.Sleep(200);
                    }
                }
            }
        }
    }
}