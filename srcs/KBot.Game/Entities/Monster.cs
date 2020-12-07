using KBot.Game.Enum;
using PropertyChanged;

namespace KBot.Game.Entities
{
    /// <summary>
    /// Represent a monster
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Monster : LivingEntity
    {
        /// <summary>
        /// Model ID used by this monster
        /// </summary>
        public int ModelId { get; }

        public Monster(long id, string name, int modelId) : base(id, EntityType.Monster, name)
        {
            ModelId = modelId;
        }
    }
}