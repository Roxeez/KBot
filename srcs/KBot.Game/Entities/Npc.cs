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

        public int BasicRange { get; set; }
        public int BasicCastTime { get; set; }
        public int BasicCooldown { get; set; }

        public Npc(long id, string name, int modelId) : base(id, EntityType.Npc, name)
        {
            ModelId = modelId;
        }
    }
}