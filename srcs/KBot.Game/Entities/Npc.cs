using KBot.Game.Enum;
using PropertyChanged;

namespace KBot.Game.Entities
{
    [AddINotifyPropertyChangedInterface]
    public class Npc : LivingEntity
    {
        public int ModelId { get; set; }

        public Npc()
        {
            EntityType = EntityType.Npc;
        }
    }
}