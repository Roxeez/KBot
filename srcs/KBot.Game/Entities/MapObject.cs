namespace KBot.Game.Entities
{
    public class MapObject : Entity
    {
        public int ModelId { get; set; }
        public int Amount { get; set; }
        public Player Owner { get; set; }
    }
}