using System;
using KBot.Game.Enum;
using PropertyChanged;

namespace KBot.Game.Inventories
{
    [AddINotifyPropertyChangedInterface]
    public class ItemStack : IEquatable<ItemStack>
    {
        public Item Item { get; }
        public int Amount { get; set; }

        public ItemStack(Item item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        public bool Equals(ItemStack other)
        {
            return other != null && other.Item.Equals(Item);
        }

        public override string ToString()
        {
            return $"{Item.Name} x {Amount}";
        }
    }
}