using System;
using JetBrains.Annotations;
using KBot.Common.Collection;
using KBot.Game.Battle;
using KBot.Game.Enum;

namespace KBot.Game.Entities
{
    /// <summary>
    /// Represent a living entity in the game (npc, monster, player)
    /// </summary>
    public abstract class LivingEntity : Entity
    {
        /// <summary>
        /// Level of the entity
        /// </summary>
        public int Level { get; set; }
        
        /// <summary>
        /// Current hp percentage of entity
        /// </summary>
        public int HpPercentage { get; set; }
        
        /// <summary>
        /// Current mp percentage of entity
        /// </summary>
        public int MpPercentage { get; set; }
        
        /// <summary>
        /// Speed of this entity
        /// </summary>
        public int Speed { get; set; }
        
        /// <summary>
        /// Contains all buffs of this entity
        /// </summary>
        [NotNull]
        public ObservableSet<Buff> Buffs { get; }

        protected LivingEntity(long id, EntityType type, string name) : base(id, type, name)
        {
            if (type == EntityType.MapObject)
            {
                throw new InvalidOperationException("Living entity can't be of type MapObject");
            }
            
            Buffs = new ObservableSet<Buff>();
        }
    }
}