using KBot.Game.Enum;
using KBot.Game.Inventories;

namespace KBot.Game.Extension
{
    public static class ItemExtensions
    {
        public static bool IsPotion(this Item item)
        {
            return item.InventoryType == InventoryType.Main && item.Type == 5 && item.SubType == 0;
        }

        public static bool IsFood(this Item item)
        {
            return item.InventoryType == InventoryType.Etc && item.Type == 1 && item.SubType == 0;
        }
        
        public static bool IsSnack(this Item item)
        {
            return item.InventoryType == InventoryType.Etc && item.Type == 2 && item.SubType == 0;
        }
        
        public static bool IsPotion(this InventoryItem item)
        {
            return item.Stack.Item.IsPotion();
        }

        public static bool IsFood(this InventoryItem item)
        {
            return item.Stack.Item.IsFood();
        }
        
        public static bool IsSnack(this InventoryItem item)
        {
            return item.Stack.Item.IsSnack();
        }
    }
}