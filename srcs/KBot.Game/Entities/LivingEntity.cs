using KBot.Game.Enum;

namespace KBot.Game.Entities
{
    public abstract class LivingEntity : Entity
    {
        public int Level { get; set; }
        
        public int HpPercentage { get; set; }
        public int MpPercentage { get; set; }
        
        public int Speed { get; set; }

        public bool IsAlive => HpPercentage > 0;
    }
}