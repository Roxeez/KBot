using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBot.Game.Inventories;
using PropertyChanged;

namespace KBot.Core.Configuration
{
    [AddINotifyPropertyChangedInterface]
    public class ItemConfiguration : IEquatable<ItemConfiguration>
    {
        public Item Item { get; set; }
        public bool UseForHp { get; set; }
        public bool UseForMp { get; set; }
        public int HpPercentage { get; set; }
        public int MpPercentage { get; set; }
        
        public bool Equals(ItemConfiguration other)
        {
            return other != null && other.Item.Equals(Item);
        }
    }
}
