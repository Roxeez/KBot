using KBot.Game.Enum;
using PropertyChanged;

namespace KBot.Game.Entities
{
    /// <summary>
    /// Represent a NPC
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Npc : LivingEntity
    {
        /// <summary>
        /// Model ID used by this npc
        /// </summary>
        public int ModelId { get; }

        public Npc(long id, string name, int modelId) : base(id, EntityType.Monster, name)
        {
            ModelId = modelId;
        }
    }
}