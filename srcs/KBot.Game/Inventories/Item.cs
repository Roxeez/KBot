using System;
using System.Windows.Media.Imaging;
using KBot.Game.Enum;

namespace KBot.Game.Inventories
{
    public class Item : IEquatable<Item>
    {
        public int Id { get; }
        public string Name { get; }
        public InventoryType InventoryType { get; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public BitmapImage Icon { get; }
        
        public Item(int id, string name, InventoryType type, BitmapImage icon)
        {
            Id = id;
            Name = name;
            InventoryType = type;
            Icon = icon;
        }

        public bool Equals(Item other)
        {
            return other != null && other.Id == Id;
        }
    }
}