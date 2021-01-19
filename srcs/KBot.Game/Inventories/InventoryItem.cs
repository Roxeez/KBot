using KBot.Game.Enum;

namespace KBot.Game.Inventories
{
    public class InventoryItem
    {
        public ItemStack Stack { get; }
        public InventoryType InventoryType { get; }
        public int Slot { get; }

        public InventoryItem(ItemStack stack, InventoryType inventoryType, int slot)
        {
            Stack = stack;
            InventoryType = inventoryType;
            Slot = slot;
        }

        public override string ToString()
        {
            return $"[{Slot + 1}] - {Stack}";
        }
    }
}