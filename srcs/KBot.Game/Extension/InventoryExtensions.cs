using System.Linq;
using KBot.Game.Inventories;

namespace KBot.Game.Extension
{
    public static class InventoryExtensions
    {
        public static InventoryItem Find(this Inventory inventory, Item item)
        {
            return inventory.FirstOrDefault(x => x.Stack.Item.Equals(item));
        }

        public static InventoryItem Find(this Inventory inventory, int modelId)
        {
            return inventory.FirstOrDefault(x => x.Stack.Item.Id == modelId);
        }
    }
}