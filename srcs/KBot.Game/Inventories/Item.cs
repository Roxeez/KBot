using System;
using KBot.Game.Enum;

namespace KBot.Game.Inventories
{
    public class Item : IEquatable<Item>
    {
        public int Id { get; }
        public string Name { get; }
        public InventoryType InventoryType { get; }

        public Item(int id, string name, InventoryType type)
        {
            Id = id;
            Name = name;
            InventoryType = type;
        }

        public bool Equals(Item other)
        {
            return other != null && other.Id == Id;
        }
    }
}