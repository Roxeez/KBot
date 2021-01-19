using KBot.Game.Inventories;

namespace KBot.Event.Characters
{
    public class InventoryLoadedEvent : IEvent
    {
        public Inventory Inventory { get; set; }
    }
}