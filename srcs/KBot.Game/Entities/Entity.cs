using KBot.Game.Enum;
using KBot.Game.Maps;
using PropertyChanged;

namespace KBot.Game.Entities
{
    [AddINotifyPropertyChangedInterface]
    public abstract class Entity
    {
        public long Id { get; set; }
        public EntityType EntityType { get; set; }
        public string Name { get; set; }
        
        public Map Map { get; set; }
        public Position Position { get; set; }
    }
}