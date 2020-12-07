using System;
using JetBrains.Annotations;
using KBot.Game.Enum;
using KBot.Game.Maps;
using PropertyChanged;

namespace KBot.Game.Entities
{
    /// <summary>
    /// Represent any entity in the game (monster, npc, player, drops etc..)
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public abstract class Entity : IEquatable<Entity>
    {
        /// <summary>
        /// Unique ID of entity
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Type of entity
        /// </summary>
        public EntityType EntityType { get; }
        
        /// <summary>
        /// Name of entity
        /// </summary>
        [NotNull]
        public string Name { get; set; }
        
        /// <summary>
        /// Map object where this entity actually is located
        /// </summary>
        public Map Map { get; set; }
        
        /// <summary>
        /// Position of this entity on the map
        /// </summary>
        public Position Position { get; set; }

        protected Entity(long id, EntityType type, string name)
        {
            Id = id;
            EntityType = type;
            Name = name;
        }
        
        public bool Equals(Entity other)
        {
            return other != null && other.Id == Id && other.EntityType == EntityType;
        }
    }
}