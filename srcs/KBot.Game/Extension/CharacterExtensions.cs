using System;
using System.Linq;
using System.Threading;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Inventories;
using KBot.Game.Battle;
using KBot.Game.Pets;

namespace KBot.Game.Extension
{
    public static class CharacterExtensions
    {
        public static int GetSkillRange(this Character character, Skill skill)
        {
            return skill.Range + (character.HasBuff(578) ? 4 : 0);
        }

        public static void WalkTo(this Character character, Position position)
        {
            character.Walk(position);

            while (!character.IsInRange(position, 1))
            {
                if (character.CantMove)
                {
                    Log.Information("Character can't move (stunned)");
                    while (character.CantMove)
                    {
                        Thread.Sleep(1);
                    }
                    
                    character.Walk(position);
                }
                
                Thread.Sleep(1);
            }
        }

        public static InventoryItem GetInventoryItem(this Character character, Item item)
        {
            return character.GetInventory(item.InventoryType).FirstOrDefault(x => x.Stack.Item.Equals(item));
        }

        public static InventoryItem GetInventoryItem(this Character character, InventoryType type, int itemId)
        {
            return character.GetInventory(type).FirstOrDefault(x => x.Stack.Item.Id == itemId);
        }

        public static bool HasItem(this Character character, Item item)
        {
            return character.GetInventory(item.InventoryType)?.Any(x => x.Stack.Item.Equals(item)) == true;
        }

        public static void WalkInRange(this Character character, Position position, int range)
        {
            double distance = character.Position.GetDistance(position);
            if (distance <= range)
            {
                return;
            }

            double ratio = (distance - range) / distance;

            double x = character.Position.X + (ratio * (position.X - character.Position.X));
            double y = character.Position.Y + (ratio * (position.Y - character.Position.Y));
            
            var destination = new Position((short)x, (short)y);

            character.Walk(destination);
            
            while (!character.Position.Equals(destination))
            {
                if (character.CantMove)
                {
                    while (character.CantMove)
                    {
                        Thread.Sleep(1);
                    }
                    
                    character.Walk(position);
                }
                
                Thread.Sleep(1);
            }
        }

        public static bool IsInSkillRange(this Character character, Position position, Skill skill)
        {
            return character.GetDistance(position) <= character.GetSkillRange(skill) + 1;
        }

        public static bool IsInRange(this Character character, Position position, int range)
        {
            return character.GetDistance(position) <= range;
        }

        public static double GetDistance(this Character character, Position position)
        {
            return character.Position.GetDistance(position);
        }

        public static bool HasBuff(this Character character, int buffId)
        {
            return character.Buffs.Any(x => x.Id == buffId);
        }
        
        public static Inventory GetInventory(this Character character, InventoryType inventoryType)
        {
            return character.Inventories.GetValue(inventoryType);
        }

        public static Skill GetBasicAttack(this Character character)
        {
            return character.Skills.FirstOrDefault(x => x.CastId == 0);
        }

        public static void UseItemOn(this Character character, Item item, LivingEntity entity)
        {
            Inventory inventory = character.GetInventory(item.InventoryType);
            if (inventory == null)
            {
                Log.Warning($"Can't get inventory {item.InventoryType}");
                return;
            }

            InventoryItem stack = inventory.Find(item);
            if (stack == null)
            {
                Log.Warning($"Can't found {item.Id} in inventory {inventory.Type}");
                return;
            }
            
            character.UseItemOn(stack, entity);
        }
    }
}