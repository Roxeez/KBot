using System.Linq;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Inventories;
using KBot.Game.Battle;

namespace KBot.Game.Extension
{
    public static class CharacterExtensions
    {
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

            character.Walk(new Position((short)x, (short)y));
        }

        public static void WalkTo(this Character character, Entity entity)
        {
            character.Walk(entity.Position);
        }

        public static Inventory GetInventory(this Character character, InventoryType inventoryType)
        {
            return character.Inventories.GetValue(inventoryType);
        }

        public static Skill GetBasicAttack(this Character character)
        {
            return character.Skills.First();
        }

        public static void UseItem(this Character character, Item item)
        {
            character.UseItemOn(item, character);   
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