using System;
using KBot.Game.Enum;
using KBot.Game.Inventories;

namespace KBot.Game.Entities
{
    public class MapObject : Entity
    {
        /// <summary>
        /// ItemStack of this map object
        /// </summary>
        public ItemStack ItemStack { get; }
        
        /// <summary>
        /// Owner of this map object
        /// </summary>
        public Player Owner { get; set; }

        public MapObject(long id, ItemStack stack) : base(id, EntityType.MapObject, stack.Item.Name)
        {
            ItemStack = stack;
        }
    }
}