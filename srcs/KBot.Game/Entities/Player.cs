using KBot.Game.Enum;
using PropertyChanged;

namespace KBot.Game.Entities
{
    
    /// <summary>
    /// Represent a player
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class Player : LivingEntity
    {
        /// <summary>
        /// Hero level of this player
        /// </summary>
        public int HeroLevel { get; set; }
        
        /// <summary>
        /// Gender of this player
        /// </summary>
        public Gender Gender { get; set; }
        
        /// <summary>
        /// Job of this player
        /// </summary>
        public Job Job { get; set; }

        public Player(long id, string name) : base(id, EntityType.Player, name)
        {

        }
    }
}