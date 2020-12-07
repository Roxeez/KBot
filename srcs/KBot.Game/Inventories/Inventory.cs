using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Game.Inventories
{
    public sealed class Inventory : IEnumerable<InventoryItem>, INotifyCollectionChanged
    {
        private readonly Dictionary<int, InventoryItem> stacks;

        public InventoryType Type { get; }
        
        public InventoryItem this[int index]
        {
            get => stacks.GetValue(index);
            set
            {
                InventoryItem previous = stacks[index];

                stacks[index] = value;
                if (value == null)
                {
                    CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, previous));
                }
                else
                {
                    if (previous != null)
                    {
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, previous));
                    }
                    else
                    {
                        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
                    }
                }
            }
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

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}