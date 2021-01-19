using System.Collections;
using System.Collections.Generic;
using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Game.Inventories
{
    public sealed class Inventory : IEnumerable<InventoryItem>
    {
        private readonly Dictionary<int, InventoryItem> stacks;

        public InventoryType Type { get; }
        
        public InventoryItem this[int index]
        {
            get => stacks.GetValue(index);
            set => stacks[index] = value;
        }

        public Inventory(InventoryType type, Dictionary<int, InventoryItem> stacks)
        {
            Type = type;
            this.stacks = stacks;
        }

        public IEnumerator<InventoryItem> GetEnumerator()
        {
            return stacks.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}