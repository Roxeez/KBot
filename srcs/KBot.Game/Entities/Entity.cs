using System;
using KBot.Game.Enum;
using KBot.Game.Maps;
using PropertyChanged;

namespace KBot.Game.Entities
{
    [AddINotifyPropertyChangedInterface]
    public abstract class Entity : IEquatable<Entity>
    {
        public long Id { get; set; }
        public EntityType EntityType { get; set; }
        public string Name { get; set; }
        
        public Map Map { get; set; }
        public Position Position { get; set; }
        
        public bool Equals(Entity other)
        {
            return other != null && other.Id == Id && other.EntityType == EntityType;
        }
    }
}