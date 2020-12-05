using KBot.Game.Enum;
using PropertyChanged;

namespace KBot.Game.Entities
{
    [AddINotifyPropertyChangedInterface]
    public class Monster : LivingEntity
    {
        public int ModelId { get; set; }

        public Monster()
        {
            EntityType = EntityType.Monster;
        }
    }
}